using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;

public class Simulation : MonoBehaviour
{
    [SerializeField] private Agent _prefab;
    [SerializeField] private int _agentCount;
    [SerializeField] private int _extentsX;
    [SerializeField] private int _extentsZ;
    [SerializeField] private float _epochTime;
    [SerializeField] private float _mutationProbability;
    [SerializeField] private float _mutationDelta;

    public class NewEpochEvent : UnityEvent<int> { }
    public NewEpochEvent OnNewEpoch = new NewEpochEvent();

    private int _currentEpoch;

    private Genome _best1;
    private Genome _best2;

    private Agent[] _spawned;

    private void Start()
    {
        _spawned = new Agent[_agentCount];

        StartCoroutine(SimulationRoutine());
    }

    private IEnumerator SimulationRoutine()
    {
        _currentEpoch = 1;
        while (true)
        {
            OnNewEpoch?.Invoke(_currentEpoch);
            SpawnNewAgents();

            yield return new WaitForSeconds(_epochTime);

            UpdateBests();
            DestroyAgents();

            _currentEpoch++;
            yield return new WaitForSeconds(2f);
        }
    }

    private void UpdateBests()
    {
        SortedDictionary<float, Genome> genomes = new SortedDictionary<float, Genome>();
        for (int i = 0; i < _spawned.Length; i++)
        {
            genomes[_spawned[i].Bonus] = _spawned[i].Genome;
        }

        List<Genome> sortedGenomes = new List<Genome>();
        foreach (var g in genomes) sortedGenomes.Add(g.Value);

        _best1 = sortedGenomes[sortedGenomes.Count - 1];
        _best2 = sortedGenomes[sortedGenomes.Count - 2];
    }

    private void SpawnNewAgents()
    {
        for (int i = 0; i < _agentCount; i++)
        {
            if (_spawned[i] != null) Destroy(_spawned[i].gameObject);

            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-_extentsX, _extentsX), 0, Random.Range(-_extentsZ, _extentsZ));
            _spawned[i] = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            if (_currentEpoch == 1)
            {
                GenerateBlankGenome(_spawned[i].Genome);
                InitAgent(_spawned[i], _spawned[i].Genome, _spawned[i].Genome);
            }
            else
            {
                InitAgent(_spawned[i], _best1, _best2);
            }
        }
    }

    private void DestroyAgents()
    {
        for (int i = 0; i < _spawned.Length; i++)
        {
            Destroy(_spawned[i].gameObject);
        }
    }

    private void GenerateBlankGenome(Genome g)
    {
        for (int i = 0; i < g.Count; i++)
        {
            for (int j = 0; j < g[i].Length; j++)
            {
                g[i][j] = Random.Range(-1f, 1f);
            }
        }
    }

    private void InitAgent(Agent agent, Genome g1, Genome g2)
    {
        for (int i = 0; i < agent.Genome.Count; i++)
        {
            for (int j = 0; j < agent.Genome[i].Length; j++)
            {
                float newVal = Mathf.Lerp(g1[i][j], g2[i][j], Random.Range(0f,1f));
                if (Random.Range(0f,1f) < _mutationProbability)
                {
                    newVal += Random.Range(-_mutationDelta, _mutationDelta);
                }
                agent.Genome[i][j] = newVal;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(2 * _extentsX, 0.1f, 2 * _extentsZ));
    }

}
