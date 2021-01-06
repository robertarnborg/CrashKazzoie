using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody myRigidBody = null;

    [SerializeField] private float movementSpeed = 9999f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float rotateSpeed = 20f;
    [SerializeField] private float rotateEngage = 0.2f;
    [SerializeField] private float rotateSmooth = 3f;
    [SerializeField] private float jumpHeight = 20f;

    [SerializeField] private Transform cam = null;

    private float rotateSmoothing = 1f;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveAndRotate();
        Jump();
    }

    private void MoveAndRotate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude <= Mathf.Epsilon)
        {
            return;
        }

        direction = Quaternion.Euler(0, cam.eulerAngles.y, 0) * direction;

        if (myRigidBody.velocity.magnitude > maxSpeed)
        {
            float tempY = myRigidBody.velocity.y;
            myRigidBody.velocity = myRigidBody.velocity.normalized * maxSpeed;
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, tempY, myRigidBody.velocity.z);
        }

        myRigidBody.AddForce(direction * movementSpeed * Time.deltaTime);

        if (transform.forward != direction)
        {

            float angleCheck = Vector3.Dot(transform.forward, direction);
            if (angleCheck > rotateEngage)
            {
                rotateSmoothing = Mathf.Lerp(1f, rotateSmooth, angleCheck);
            }
            else
            {
                rotateSmoothing = 1f;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(myRigidBody.velocity.x, 0, myRigidBody.velocity.z)), Time.deltaTime * rotateSpeed * rotateSmoothing);
        }
    }

    private void Jump()
    {
        if (!Input.GetButtonDown("Jump"))
        {
            return;
        }
        Debug.Log("Hoppsan");
        myRigidBody.AddForce(transform.up * jumpHeight);
    }
}
