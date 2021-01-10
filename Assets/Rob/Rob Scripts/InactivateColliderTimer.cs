using UnityEngine;

public class InactivateColliderTimer : MonoBehaviour
{
    [SerializeField] private float killTimer = 0.2f;

    private float timer = 0f;
    private Collider coll;

    private void Awake()
    {
        coll = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > killTimer)
        {
            coll.enabled = false;
        }
    }
}
