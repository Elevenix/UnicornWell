using UnityEngine;

public class DifficultyAsteroids : MonoBehaviour
{
    [SerializeField] private SpawnerAsteroid spawnerAsteroids;
    [SerializeField] private float minDelaySpawn = 2f;
    [SerializeField] private float maxDelaySpawn = .5f;
    [SerializeField] private float divisor = 100f;

    private Transform _player;
    private float _actualDeslaySpawn;

    private void Start()
    {
        _player = GameManager.Instance.GetPlayer();
        _actualDeslaySpawn = minDelaySpawn;
    }

    private void FixedUpdate()
    {
        ModifySpeedSpawnAsteroids();
    }

    private void ModifySpeedSpawnAsteroids()
    {
        if (!_player)
            return;

        float res = _player.position.y / divisor;
        _actualDeslaySpawn = minDelaySpawn - res;
        _actualDeslaySpawn = Mathf.Clamp(_actualDeslaySpawn, maxDelaySpawn, minDelaySpawn);

        // Affect value
        spawnerAsteroids.DelaySpawn = _actualDeslaySpawn;
    }
}
