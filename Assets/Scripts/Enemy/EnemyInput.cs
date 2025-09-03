using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(Animator))]
public class EnemyInput : MonoBehaviour
{
    bool isCharging;
    float currentVelocityX;
    float horizontalInput = -1f;
    float gravity = -50f;
    float jumpVelocity;
    float jumpTimer;
    GameObject player;
    Animator animator;
    Controller2D controller;
    Vector2 velocity;

    [SerializeField] private float jumpHeight = 4f; // Jump height in units
    [SerializeField] private float timeToJumpApex = 0.4f; // Time in seconds until the apex of the jump, after which the player falls back down.

    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float accelerationTimeGrounded = 0.1f;
    [SerializeField] private float offsetToPlayer = 0f;
    [SerializeField] private float inputTowardsPlayer = 1f;
    [SerializeField] private bool canJump = false;
    [SerializeField] private float jumpDelay = 2f;
    [SerializeField] private bool canCharge = false;
    [SerializeField] private float chargeSpeed = 10f;
    [SerializeField] private float chargeDistance = 5f;


    void Start()
    {
        // Get components
        controller = GetComponent<Controller2D>();
        animator = GetComponent<Animator>();
        player = FindFirstObjectByType<PlayerInput>().gameObject;

        // Setup jump variables to allow for more logical exposed settings.
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        jumpTimer = Random.Range(0, jumpDelay);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (controller.collisionInfo.below)
        {// Reset jump data if grunded
            canJump = true;
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }
        if (controller.collisionInfo.above || controller.collisionInfo.below)
        {// Reset velocity Y when touching the ground to prevent gravity accumulation
            velocity.y = 0;
        }

        jumpTimer += Time.deltaTime;
        if (jumpTimer >= jumpDelay && canJump)
        {
            jumpTimer -= jumpDelay;
            velocity.y = jumpVelocity;
        }

        if (canCharge)
        {
            isCharging = Mathf.Abs(this.transform.position.x - player.transform.position.x) < chargeDistance;
        }
        
        Debug.Log(isCharging);

        //Default inputs
        if (!isCharging || !canCharge)
        {
            horizontalInput = 0;
            if (this.transform.position.x < player.transform.position.x + offsetToPlayer) { horizontalInput += inputTowardsPlayer; }
            if (this.transform.position.x > player.transform.position.x + offsetToPlayer) { horizontalInput -= inputTowardsPlayer; }
        }
        horizontalInput = Mathf.Clamp(horizontalInput, -1, 1);

        float targetVelocityX = horizontalInput * moveSpeed;
        
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref currentVelocityX, accelerationTimeGrounded);
        if (isCharging && canCharge)
        {
            velocity.x = horizontalInput * chargeSpeed;
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime, true);
    }

    public void ResetVelocity(Vector2 oldLocalUp, Vector2 newLocalUp, bool flipLocalRight = false)
    {
        float angle = Vector2.Angle(oldLocalUp, newLocalUp);
        print(angle);
        velocity = velocity.Rotate(angle);
        float projectedVelocity = velocity.Project(newLocalUp.Rotate((flipLocalRight) ? 90 : -90));
        velocity -= newLocalUp.Rotate((flipLocalRight) ? 90 : -90) * projectedVelocity * 2;
    }
}
