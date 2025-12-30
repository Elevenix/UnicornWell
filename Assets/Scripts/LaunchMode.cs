using UnityEngine;

public class LaunchMode : MonoBehaviour
{
    [SerializeField] private string nameLevel = "StoryScene";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            LevelManager.Instance.OpenLevel(nameLevel);
    }
}
