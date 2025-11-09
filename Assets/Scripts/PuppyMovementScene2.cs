using UnityEngine;
using UnityEngine.SceneManagement; // Included if you need scene management later

public class PuppyMovementScene2 : MonoBehaviour
{
    // Public variables (set in Inspector)
    private float movementSpeed = 3f; // --Puppy's movement speed--

    // Component references
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private TextManagement textManagement;
    private GameObject dialog_box;

    void Start()
    {
        // --Get all necessary components--
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // --Safety checks--
        if (rb == null || spriteRenderer == null || animator == null)
        {
            Debug.LogError("Missing components (Rigidbody/SpriteRenderer/Animator) on puppy.");
        }
        textManagement = FindFirstObjectByType<TextManagement>();
        TextBooleanManager.text_active = true;
        dialog_box = GameObject.Find("DialogueBox_0");
        dialog_box.SetActive(true);
        // --BLOCK MOVEMENT IF DIALOGUE IS ACTIVE--
        if (TextBooleanManager.text_active)
        {
            // --Stop the puppy completely--
            rb.linearVelocity = Vector2.zero; // CORRECTED PROPERTY NAME
            animator.SetFloat("Speed", 0f);
            textManagement.ShowDialog("I hate when he goes to work!, I'll have to find something to do...");
            return;
        }
        
        
    }

    // --Used for physics and stable movement--
    void FixedUpdate()
    {
        // --BLOCK MOVEMENT IF DIALOGUE IS ACTIVE--
       if (TextBooleanManager.text_active)
       {
            // --Stop the puppy completely--
            rb.linearVelocity = Vector2.zero; // CORRECTED PROPERTY NAME
            animator.SetFloat("Speed", 0f);
            return;
       }

        // --Get player input (WASD)--
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(inputX, inputY).normalized;

        // --Calculate new position using Rigidbody for solid collision--
        Vector2 newPosition = rb.position + movement * movementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        // --ANIMATION LOGIC: Update Animator 'Speed' parameter--
        float currentInputSpeed = movement.magnitude;
        animator.SetFloat("Speed", currentInputSpeed);

        // --Sprite Flip Logic (to face direction of movement)--
        if (inputX > 0)
        {
            spriteRenderer.flipX = false; // --Facing right--
        }
        else if (inputX < 0)
        {
            spriteRenderer.flipX = true; // --Facing left--
        }
    }
}





