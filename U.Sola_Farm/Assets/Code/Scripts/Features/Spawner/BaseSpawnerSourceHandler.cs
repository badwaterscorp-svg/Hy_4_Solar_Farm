using UnityEngine;

public abstract class BaseSpawnerSourceHandler : MonoBehaviour
{
    [SerializeField] protected float timeBetweenSpawns = 1f;
    
    private float _timer;
    private bool _isSpawning;

    protected abstract void Spawn();

    private void Update()
    {
        if (_isSpawning)
        {
            _timer += Time.deltaTime;
            if (_timer >= timeBetweenSpawns)
            {
                _timer = 0f;
                Spawn();
            }
        }
    }

    public void StartSpawning()
    {
        _isSpawning = true;
        _timer = timeBetweenSpawns;
    }

    public void StopSpawning()
    {
        _isSpawning = false;
        _timer = 0f;
    }
}
