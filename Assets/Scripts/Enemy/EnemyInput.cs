using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class EnemyInput : MonoBehaviour
{
    float currentVelocityX;
    float gravity = -50f;
    float jumpVelocity;
    float jumpTimer;
    GameObject player;
    Controller2D controller;
    Vector2 velocity;

    [SerializeField] private float jumpHeight = 4; // Jump height in units
    [SerializeField] private float jumpForgivenessTime = 0.2f; // Time in seconds within you can use your inital to jump after falling off a platform.
    [SerializeField] private float timeToJumpApex = 0.4f; // Time in seconds until the apex of the jump, after which the player falls back down.

    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float accelerationTimeGrounded = 0.1f;
    [SerializeField] private float offsetToPlayer = 0f;
    [SerializeField] private float inputTowardsPlayer = 1f;
    [SerializeField] private bool canJump = false;
    [SerializeField] private float jumpDelay = 2f;


    void Start()
    {
        // Get components
        controller = GetComponent<Controller2D>();
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
        }
        if (controller.collisionInfo.above || controller.collisionInfo.below)
        {// Reset velocity Y when touching the ground to prevent gravity accumulation
            velocity.y = 0;
        }
        float horizontalInput = 0;

        jumpTimer += Time.deltaTime;
        if(jumpTimer >= jumpDelay && canJump)
        {
            jumpTimer -= jumpDelay;
            velocity.y = jumpVelocity;
        }
        

        if (this.transform.position.x < player.transform.position.x + offsetToPlayer) { horizontalInput += inputTowardsPlayer; }
        if (this.transform.position.x > player.transform.position.x + offsetToPlayer) { horizontalInput -= inputTowardsPlayer; }

        float targetVelocityX = horizontalInput * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref currentVelocityX, accelerationTimeGrounded);

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
