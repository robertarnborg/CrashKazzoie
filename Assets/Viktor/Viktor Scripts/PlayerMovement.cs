using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody myRigidBody = null;

    [SerializeField] private float movementSpeed = 9999f;
    [SerializeField] private float movementMultiplier = 10f;
    [SerializeField] private float maxSpeed = 5f;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (myRigidBody.velocity.magnitude > maxSpeed)
        {
            myRigidBody.velocity = myRigidBody.velocity.normalized * maxSpeed;
        }
        if (direction.magnitude >= 0.1f)
        {
            myRigidBody.AddForce(direction * movementSpeed * movementMultiplier * Time.deltaTime);
        }
    }

}
