using UnityEngine;

public class PuppyMovement : MonoBehaviour
{
    // Variables públicas (ajustables en el Inspector)
    public float movementSpeed = 0.3f; // Tu velocidad lenta para la atmósfera

    // Referencias a Componentes
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator; // ¡La nueva referencia!

    void Start()
    {
        // Obtener todos los componentes necesarios
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator

        // Verificaciones de seguridad (opcional, pero buena práctica)
        if (rb == null || spriteRenderer == null || animator == null)
        {
            Debug.LogError("Faltan componentes (Rigidbody/SpriteRenderer/Animator) en el cachorro.");
        }
    }

    // Usamos FixedUpdate para mover el Rigidbody (colisiones más estables)
    void FixedUpdate()
    {
        // 1. Obtener la entrada del jugador (WASD)
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(inputX, inputY).normalized;

        // 2. Mover la posición del Rigidbody
        Vector2 newPosition = rb.position + movement * movementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        // 3. LÓGICA DE ANIMACIÓN (Actualizar el Animador)
        // Calculamos la magnitud del vector de movimiento.
        // Es 0 cuando no hay entrada, y 1 cuando hay entrada.
        float currentInputSpeed = movement.magnitude;

        // Le pasamos este valor al parámetro 'Speed' del Animador
        // Si es 0 (no hay input) -> Idle. Si es > 0.1 (hay input) -> Walk.
        animator.SetFloat("Speed", currentInputSpeed);

        // 4. Lógica de Giro del Sprite (FlipX)
        if (inputX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (inputX < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}