using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private Shaker dieShaker;
    [SerializeField] private ParticleSystem deathParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform") || collision.CompareTag("Death"))
        {
            CameraShake.Instance.Shake(dieShaker);
            GameManager.Instance.PlayerDie();
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
