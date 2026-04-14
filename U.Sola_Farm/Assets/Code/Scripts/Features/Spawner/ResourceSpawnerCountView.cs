using TMPro;
using UnityEngine;

public class ResourceSpawnerCountView : MonoBehaviour
{
    [SerializeField] TMP_Text _countText;
    [SerializeField] private BaseSpawnerResource _spawner;

    private void OnEnable()
    {
        _spawner.OnSpawn += UpdateCount;
        _spawner.OnDespawn += UpdateCount;
    }

    private void UpdateCount()
    {
        _countText.text = $"{_spawner.SpawnedCount}/{_spawner.MaxCollection}";
    }

    private void OnDisable()
    {
        _spawner.OnSpawn -= UpdateCount;
        _spawner.OnDespawn -= UpdateCount;
    }
}
