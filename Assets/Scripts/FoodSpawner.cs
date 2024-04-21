using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private Food _prefab;
    [SerializeField] private float _timeDelta;
    [SerializeField] private int _maxCount;
    [SerializeField] private int _extentsX;
    [SerializeField] private int _extentsZ;

    private Food[] _spawned;

    private void Awake()
    {
        _spawned = new Food[_maxCount];

        InvokeRepeating(nameof(SpawnFoods), 0f, _timeDelta);
    }

    private void SpawnFoods()
    {
        for (int i = 0; i < _maxCount; i++)
        {
            if (_spawned[i] != null) continue;

            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-_extentsX, _extentsX), 0, Random.Range(-_extentsZ, _extentsZ));
            _spawned[i] = Instantiate(_prefab, spawnPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(2 * _extentsX, 0.1f, 2 * _extentsZ));
    }
}
