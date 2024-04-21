using TMPro;
using UnityEngine;

public class EpochUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Simulation _simulation;

    private void Awake()
    {
        _simulation.OnNewEpoch.AddListener(HandleNewEpoch);
    }

    private void OnDestroy()
    {
        _simulation.OnNewEpoch.RemoveListener(HandleNewEpoch);
    }

    private void HandleNewEpoch(int n)
    {
        _text.text = $"Epoch {n}";
    }
}
