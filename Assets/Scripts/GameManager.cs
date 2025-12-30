using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float delayBeforeRespawn = 3;
    [SerializeField] private SpawnerAsteroid spawnerAsteroids;
    [SerializeField] private Score score;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public Transform GetPlayer()
    {
        return player;
    }

    public void PlayerDie()
    {
        StartCoroutine(DelayLoseCoroutine());
        spawnerAsteroids.StopSpawn();
        CameraShake.Instance.SetCanShake(false);
    }

    public void WinGame()
    {
        LevelManager.Instance.WinGame();
    }

    private IEnumerator DelayLoseCoroutine()
    {
        // set best score
        score.SendEndScore();
        yield return new WaitForSeconds(delayBeforeRespawn);
        LevelManager.Instance.LoseGame();
    }
}
