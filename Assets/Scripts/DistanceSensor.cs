using UnityEngine;

public class DistanceSensor : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _count;
    [SerializeField] private float _angleDelta;
    [field: SerializeField] public float MaxDistance { get; private set; }

    public float[] OutputData { get; private set; }

    private void Awake()
    {
        OutputData = new float[_count];
    }

    public void Evaluate()
    {
        float startAngle = -_angleDelta * 0.5f;

        for (int i = 0; i < _count; i++)
        {
            float angle = startAngle + i * _angleDelta / _count;
            Vector3 direction = Quaternion.AngleAxis(angle, transform.up) * transform.forward;

            OutputData[i] = MaxDistance;
            if(Physics.Raycast(transform.position, direction, out RaycastHit hit, MaxDistance, _layerMask))
            {
                OutputData[i] = hit.distance;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        float startAngle = -_angleDelta * 0.5f;

        for (int i = 0; i < _count; i++)
        {
            float angle = startAngle + i * _angleDelta / (_count-1);
            Vector3 direction = Quaternion.AngleAxis(angle, transform.up) * transform.forward;

            Gizmos.DrawRay(transform.position, direction);
        }
    }
}
