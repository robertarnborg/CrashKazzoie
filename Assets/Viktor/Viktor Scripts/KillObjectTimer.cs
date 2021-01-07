using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObjectTimer : MonoBehaviour
{
    [SerializeField] private float killTimer = 2f;

    private float timer = 0f;

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > killTimer)
        {
            Destroy(gameObject);
        }
    }
}
