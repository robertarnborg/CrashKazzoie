using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpObject : MonoBehaviour
{
    [SerializeField] private DoubleJumpController controller = null;

    public void ResetToken()
    {
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void RemoveToken()
    {
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().GrantDoubleJump();
            controller.AddObject(transform.gameObject);
            RemoveToken();
        }
    }

}
