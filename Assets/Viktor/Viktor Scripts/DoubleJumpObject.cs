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
    private ParticleSystem _particles;


    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _sphereCollider = GetComponent<SphereCollider>();
        _particles = GetComponent<ParticleSystem>();
    }

    public void ResetToken()
    {
        _particles.Play();
        _sphereCollider.enabled = true;
        _meshRenderer.material = defaultMaterial;
    }

    public void ActivateToken()
    {
        Instantiate(pickupFX, transform.position, transform.rotation);

        _particles.Stop();
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
