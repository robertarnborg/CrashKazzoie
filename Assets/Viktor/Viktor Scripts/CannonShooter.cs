using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooter : MonoBehaviour
{
    [SerializeField] private float shootingTimer = 3f;
    [SerializeField] private float startDelay = 0f;
    [SerializeField] private float ballSpeed = 5f;
    [SerializeField] private float ballLifetime = 5f;
    [SerializeField] private float spawnOffset = 2f;

    [SerializeField] private GameObject cannonBallPrefab = null;

    private float timer = 0f;
    private bool delay = true;
    private Vector3 spawnPosition;

    private SimplerSFX fireCannonSFX;

    private void Awake()
    {
        spawnPosition = transform.position + (transform.forward * spawnOffset);
        //create direction
        fireCannonSFX = GetComponent<SimplerSFX>();
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (delay)
        {
            if (timer > startDelay)
            {
                timer -= startDelay;
                delay = false;
            }
            return;
        }
        if (timer > shootingTimer)
        {
            timer -= shootingTimer;
            GameObject cannonBall = Instantiate(cannonBallPrefab, spawnPosition, Quaternion.identity) as GameObject;
            cannonBall.GetComponent<CannonBall>().SetCannonBall(ballLifetime, ballSpeed, transform.forward);

            fireCannonSFX.PlayRandomSfx();
        }
    }

}
