using System.Collections;
using UnityEngine;

public class SpawnerAsteroid : MonoBehaviour
{
    [SerializeField] private Asteroid[] asteroids;
    [SerializeField] private Vector2 spawnPointStart;
    [SerializeField] private Vector2 spawnPointEnd;
    [SerializeField] private float delaySpawnAsteroidAfterLine = .5f;
    [SerializeField] private Transform parentAsteroids;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraToFollow;
    [SerializeField] private float delayStart = 1;

    [HideInInspector] public float DelaySpawn = 2;

    private bool _canSpawn = true;

    private void Start()
    {
        StartCoroutine(SpawnerCoroutine());
    }

    private IEnumerator SpawnerCoroutine()
    {
        yield return new WaitForSeconds(delayStart);

        while (true)
        {
            yield return new WaitForSeconds(DelaySpawn);

            if (!_canSpawn)
                continue;


            // Random spawn asteroid
            float posX = Random.Range(spawnPointStart.x, spawnPointEnd.x);
            float posY = Random.Range(spawnPointStart.y, spawnPointEnd.y);
            Vector2 randPos = new Vector2(posX, posY + cameraToFollow.position.y);

            // Direction to go
            Vector2 dirAsteroid = ((Vector2)player.transform.position - randPos).normalized;

            // Random Asteroid 
            int randIndexAst = Random.Range(0, asteroids.Length);
            Asteroid randAst = asteroids[randIndexAst];

            // Create line asteroid
            LineAsteroid line = Instantiate(randAst.lineAsteroid, randPos, Quaternion.identity);
            line.CreateLine(dirAsteroid);

            yield return new WaitForSeconds(delaySpawnAsteroidAfterLine);

            // Create asteroid
            Asteroid ast = Instantiate(randAst, randPos, Quaternion.identity);
            ast.SetDirection(dirAsteroid);
            ast.SetParent(parentAsteroids);
            ast.SetLine(line);
        }
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LimitSpawn"))
            _canSpawn = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LimitSpawn"))
            _canSpawn = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(spawnPointStart, spawnPointEnd);
    }
}
