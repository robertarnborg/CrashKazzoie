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
    [SerializeField] private bool targetPlayer = false;
    [SerializeField] private float targetRange = 20f;

    [SerializeField] private GameObject cannonBallPrefab = null;
    [SerializeField] private ParticleSystem particlesSmoke = null;
    [SerializeField] private ParticleSystem particlesFire = null;
    [SerializeField] private GameObject pipe = null;

    private float timer = 0f;
    private bool delay = true;
    private Animator myAnimator = null;
    private Transform player = null;

    private SimplerSFX fireCannonSFX;

    private void Awake()
    {
        if (targetPlayer)
        {
            player = FindObjectOfType<PlayerMovement>().transform;
        }
        fireCannonSFX = GetComponent<SimplerSFX>();
        myAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        myAnimator.SetBool("Shooting", false);
        if (delay)
        {
            Timer();
            if (timer > startDelay)
            {
                timer -= startDelay;
                delay = false;
            }
            return;
        }
        if (targetPlayer)
        {
            Vector3 direction = player.position - transform.position;
            if (direction.magnitude > targetRange)
            {
                return;
            }
            direction.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
            Shoot();
        }
        else
        {
            Shoot();
        }
    }

    private void Timer()
    {
        timer += Time.fixedDeltaTime;
    }

    private void Shoot()
    {
        Timer();
        if (timer < shootingTimer)
        {
            return;
        }
        timer -= shootingTimer;
        myAnimator.SetBool("Shooting", true);
        GameObject cannonBall = Instantiate(cannonBallPrefab, transform.position + (transform.forward * spawnOffset), Quaternion.identity) as GameObject;
        cannonBall.GetComponent<CannonBall>().SetCannonBall(ballLifetime, ballSpeed, transform.forward, pipe);
        particlesSmoke.Play();
        particlesFire.Play();

        fireCannonSFX.PlayRandomSfx();
    }
}
