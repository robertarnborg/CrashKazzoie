using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpObject : MonoBehaviour
{
    [SerializeField] private DoubleJumpController controller = null;
    public GameObject pickupFX;
    public Material defaultMaterial;
    public Material onActivateTokenMaterial;
    private MeshRenderer _meshRenderer;
    private SphereCollider _sphereCollider;


    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    public void ResetToken()
    {
        _sphereCollider.enabled = true;
        _meshRenderer.material = defaultMaterial;
    }

    public void ActivateToken()
    {
        Instantiate(pickupFX, transform.position, transform.rotation);

        _sphereCollider.enabled = false;
        _meshRenderer.material = onActivateTokenMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().GrantDoubleJump();
            controller.AddObject(transform.gameObject);
            ActivateToken();
        }
    }

}
