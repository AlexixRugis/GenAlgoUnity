using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private float _energy;

    private void OnTriggerEnter(Collider collider)
    {
        Agent agent = collider.GetComponent<Agent>();
        if (agent != null )
        {
            agent.AddEnergy(_energy);
            Destroy(gameObject);
        }
    }
}
