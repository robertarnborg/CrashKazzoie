using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isDead;

    private Rigidbody myRigidBody = null;


    [SerializeField] private float movementSpeed = 9999f;
    [SerializeField] private float airSpeed = 100f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float rotateSpeed = 20f;
    [SerializeField] private float rotateEngage = 0.2f;
    [SerializeField] private float rotateSmooth = 3f;
    [SerializeField] private float jumpHeight = 20f;
    [SerializeField] private float jumpCheckOffset = 1f;
    [SerializeField] private Vector3 jumpCheckSize = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private string[] tagsToJumpOn = null;

    [SerializeField] private Transform cam = null;
    [SerializeField] private DoubleJumpController doubleJumpController = null;
    #region SFX
    [Header("Movement Audio Sound FX")]
    [SerializeField] private SimplerSFX jumpSFX;
    [SerializeField] private SimplerSFX landSFX;
    [SerializeField] private Animator walkSFXAnim;
    #endregion

    private float rotateSmoothing = 1f;
    private float movSpeedStorage = 1f;
    private bool isGrounded = false;
    private bool doubleJump = false;
    private bool hasHitDoubleJump = false;
    private Collider myCollider = null;
    private Animator myAnimator = null;


    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead)
        {
            walkSFXAnim.SetBool("isWalking", false);
            return;
        }

        MoveAndRotate();
        isGrounded = GroundChecker();
        Jump();
        if (hasHitDoubleJump && isGrounded)
        {
            hasHitDoubleJump = false;
            doubleJump = false;
            doubleJumpController.ResetDoubleJumpObjects();
        }
    }

    private void MoveAndRotate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude <= Mathf.Epsilon)
        {
            walkSFXAnim.SetBool("isWalking", false);
            myAnimator.SetBool("Running", false);
            return;
        }

        direction = Quaternion.Euler(0, cam.eulerAngles.y, 0) * direction;

        /*if (myRigidBody.velocity.magnitude > maxSpeed && isGrounded)
        {
            //float tempY = myRigidBody.velocity.y;
            myRigidBody.velocity = myRigidBody.velocity.normalized * maxSpeed;
            //myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, tempY, myRigidBody.velocity.z);
        }*/

        float currentSpeed = Vector3.Dot(myRigidBody.velocity, direction); 

        if(currentSpeed > maxSpeed)
        {
            Vector3 clampedSpeed = myRigidBody.velocity.normalized * maxSpeed;
            myRigidBody.velocity = new Vector3(clampedSpeed.x, myRigidBody.velocity.y, clampedSpeed.z);
            return;
        }


        /*else if (myRigidBody.velocity.y > fallSpeed)
        {
            Debug.Log("clamped");
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, fallSpeed, myRigidBody.velocity.z);
        }*/

        if (isGrounded)
        {
            movSpeedStorage = movementSpeed;
            if (Mathf.Abs(currentSpeed) > 0.1f)
            {
                walkSFXAnim.SetBool("isWalking", true);
                myAnimator.SetBool("Running", true);
            }
        }
        else if (!isGrounded)
        {
            movSpeedStorage = airSpeed;

            walkSFXAnim.SetBool("isWalking", false);
            myAnimator.SetBool("Running", false);
        }

        myRigidBody.AddForce(direction * movSpeedStorage * Time.deltaTime);

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
            myAnimator.SetBool("Jumping", false);
            return;
        }
        if (isGrounded)
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, jumpHeight, myRigidBody.velocity.z);

            jumpSFX.audioSource.pitch = 1.0f;
            jumpSFX.PlayRandomSfx();
            myAnimator.SetBool("Jumping", true);
        }
        else if (doubleJump)
        {
            myRigidBody.velocity = new Vector3(myRigidBody.velocity.x, jumpHeight, myRigidBody.velocity.z);
            doubleJump = false;

            jumpSFX.audioSource.pitch = Random.Range(1.1f, 1.2f);
            jumpSFX.PlayRandomSfx();
            myAnimator.SetBool("Jumping", true);
        }
    }

    private bool GroundChecker()
    {
        myAnimator.SetBool("Landing", false);
        Vector3 checkBoxOrigin = new Vector3(transform.position.x, transform.position.y - jumpCheckOffset, transform.position.z);
        Collider[] colliders = Physics.OverlapBox(checkBoxOrigin, transform.localScale / 2.1f, Quaternion.Euler(Vector3.down));
        if (colliders.Length <= 0)
        {
            return false;
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int s = 0; s < tagsToJumpOn.Length; s++)
            {
                if (colliders[i].CompareTag(tagsToJumpOn[s]))
                {
                    if (!isGrounded)
                    {
                        myAnimator.SetBool("Landing", true);
                        landSFX.PlayRandomSfx();
                    }
                    return true;
                }
            }
        }
        return false;
    }

    public void GrantDoubleJump()
    {
        doubleJump = true;
        hasHitDoubleJump = true;
    }
}
