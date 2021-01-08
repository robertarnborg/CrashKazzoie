using UnityEngine;

public class HazardGruesomeDeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerDeath>().GruesomeDeath();
        }
    }
}
