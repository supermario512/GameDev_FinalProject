using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    //public Transform cameraTransform;
    public float initialHealth = 100f;
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public bool isInvincible = false;
    public float invincibilityTimer = 0f;
    private bool lastStand = true;
    private float jumpHeight = 4f;
    private float maxJumpHeight = 18f;  // Maximum possible jump height with holding
    private float jumpHoldTime = 0.4f;  // How long player can hold to reach max height
    private float gravity = 9.81f;
    private float jumpTimeCounter;     // To track how long jump is held
    private bool isJumping;            // To track if player is currently jumping
    private bool hasKey = false;
    private Vector3 respawnPoint;

    //public AudioSource wallSound;
    public WeaponScript weapon;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        respawnPoint = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            Debug.Log("You're Invincible.");
            invincibilityTimer -= Time.deltaTime;

            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                Debug.Log("You're not invincible anymore.");
            }
        }

        isGrounded = controller.isGrounded;
        
        // Reset jump state when player lands
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep player grounded
            isJumping = false;
        }
       
        Vector3 finalMovement = Vector3.zero;
     
        // Movement
        if((Input.GetKey(KeyCode.A)  || Input.GetKey(KeyCode.LeftArrow)))
        {
            finalMovement -= Vector3.right;
        }
        
        if((Input.GetKey(KeyCode.D)  || Input.GetKey(KeyCode.RightArrow)))
        {
            finalMovement += Vector3.right;
        }

        finalMovement.Normalize();
        if (finalMovement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(finalMovement);
        }

        // second variable is speed value
        controller.Move(finalMovement * 10f * Time.deltaTime);

        // Handle jumping
        HandleJump();

        // Apply Gravity
        if (!isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        
        controller.Move(velocity * Time.deltaTime);

        // Check for pit fall without starting a new coroutine every frame
        if (transform.position.y < -30f)
        {
            Debug.Log("Player fell into a pit.");
            transform.position = respawnPoint;
            velocity = Vector3.zero; // Reset velocity when respawning
        }
    }

    void HandleJump()
    {
        // Start jump when pressing space
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Initial jump with minimum height
            float initialJumpVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity);
            velocity.y = initialJumpVelocity;
            isJumping = true;
            jumpTimeCounter = jumpHoldTime; // Set the counter to max hold time
        }

        // Continue boosting jump while holding space
        if (Input.GetKey(KeyCode.Space) && isJumping && jumpTimeCounter > 0)
        {
            // Calculate additional velocity to reach max height over time
            float maxJumpVelocity = Mathf.Sqrt(maxJumpHeight * 2f * gravity);
            float targetVelocity = Mathf.Lerp(velocity.y, maxJumpVelocity, 1 - (jumpTimeCounter / jumpHoldTime));
            
            // Apply only the difference to avoid sharp velocity changes
            velocity.y += (targetVelocity - velocity.y) * Time.deltaTime / jumpHoldTime;
            
            jumpTimeCounter -= Time.deltaTime;
        }

        // Release space button or time runs out
        if (Input.GetKeyUp(KeyCode.Space) || jumpTimeCounter <= 0)
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Obtain Weapon
        if(other.CompareTag("Weapon"))
        {
            weapon = other.GetComponent<WeaponScript>();
        }

        // Collect Key
        if(other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Finish"))
        {
            Debug.Log("You're a Winner!");
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            currentHealth -= dmg;
            isInvincible = true;
            invincibilityTimer = 2f;
        }
        if (currentHealth <= 0)
        {
            if (lastStand)
            {
                Debug.Log("You can only resist death once. Get some health sooner rather than later.");
                currentHealth = 1;
                lastStand = false;
                isInvincible = true;  // Temporary invincibility to prevent instant death
                invincibilityTimer = 3f; // Last Stand invincibility duration
            }
            else
            {
                currentHealth = 0;
                Debug.Log("You died.");
            }
        }
        /*
        if (currentHealth <= 0 && lastStand == true)
        {
            Debug.Log("You can only resist death once. Get some health sooner rather than later.");
            currentHealth = 1;
            lastStand = false;
        }
        else if (currentHealth <= 0 && lastStand == false)
        {
            currentHealth = 0;
            Debug.Log("You died.");
        }
        */
    }
}