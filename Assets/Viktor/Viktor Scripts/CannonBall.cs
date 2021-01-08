using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab = null;

    private float lifeTime = 5f;
    private float speed = 5f;
    private Vector3 direction;
    private float timer = 0f;

    private void Update()
    {
        transform.position += direction * Time.deltaTime * speed;
        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            KillCannonBall();
        }
    }

    public void SetCannonBall(float lifeTime, float speed, Vector3 direction)
    {
        this.lifeTime = lifeTime;
        this.speed = speed;
        this.direction = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerDeath>().GruesomeDeath();
            KillCannonBall();
        }
    }

    private void KillCannonBall()
    {
        if (explosionPrefab)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        }
        else
        {
            Debug.Log("Cannonball doesnt have an explosionPrefab");
        }
        Destroy(gameObject);
    }

}
