using System.Collections;
using UnityEngine;

public class LaunchFirstCinematic : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private string sceneName = "StoryScene";

    private void Start()
    {
        StartCoroutine(OpenSceneCoroutine());
        LevelManager.Instance.LaunchFirstCinematic();
    }

    private IEnumerator OpenSceneCoroutine()
    {
        yield return new WaitForSeconds(delay);
        LevelManager.Instance.OpenLevel(sceneName);
    }
}
