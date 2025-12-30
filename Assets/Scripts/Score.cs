using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTMP;
    [SerializeField] private float distanceStoryMode = 60f;
    [SerializeField] private bool isStoryMode = false;

    private float _actualScore = 0;
    private Transform _player;

    private void Start()
    {
        _player = GameManager.Instance.GetPlayer();

        if(isStoryMode)
            scoreTMP.text = distanceStoryMode.ToString("F1") + " m";
    }

    private void Update()
    {
        if (isStoryMode)
            ScoreStoryMode();
        else
            ScoreInfiniteMode();
    }

    private void ScoreStoryMode()
    {
        if (_player.position.y > _actualScore)
        {
            _actualScore = _player.position.y;
            float valueSM = distanceStoryMode - _actualScore;
            scoreTMP.text = valueSM.ToString("F1") + " m";
        }
    }

    private void ScoreInfiniteMode()
    {
        if (_player.position.y > _actualScore)
        {
            _actualScore = _player.position.y;
            scoreTMP.text = _actualScore.ToString("F1") + " m";
        }
    }

    public void SendEndScore()
    {
        string scoreText = _actualScore.ToString("F1") + " m";
        float bestScore = PlayerPrefs.GetFloat("BestScore");
        string bestScoreText = bestScore.ToString("F1") + " m";
        if(_actualScore > bestScore)
        {
            PlayerPrefs.SetFloat("BestScore", _actualScore);
            bestScoreText = _actualScore.ToString("F1") + " m";
        }
        LevelManager.Instance.SetScores(isStoryMode,scoreText, bestScoreText);
    }
}
