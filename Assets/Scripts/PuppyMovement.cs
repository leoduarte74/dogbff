using UnityEngine;

public class PuppyMovement : MonoBehaviour
{
    // Esta variable será visible en el Inspector. Recuerda que la ajustaste a 0.3f.
    public float movementSpeed = 0.3f;

    // Referencia al Rigidbody 2D para moverlo correctamente y detectar colisiones.
    private Rigidbody2D rb;

    // Referencia al SpriteRenderer para poder voltear el sprite.
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Obtener los componentes al inicio del juego.
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D no encontrado en el objeto del cachorro. ¡El movimiento y colisiones fallarán!");
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer no encontrado en el objeto del cachorro.");
        }
    }

    // FixedUpdate se usa para todo lo relacionado con la física y Rigidbody.
    void FixedUpdate()
    {
        // 1. Obtener la entrada del jugador (WASD)
        // GetAxisRaw proporciona valores de -1, 0, o 1.
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // 2. Calcular el vector de movimiento normalizado (para evitar ir más rápido en diagonal)
        Vector2 movement = new Vector2(inputX, inputY).normalized;

        // 3. Calcular la nueva posición, usando Time.fixedDeltaTime para física consistente.
        // **ESTO RESUELVE LA COLISIÓN**
        Vector2 newPosition = rb.position + movement * movementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        // 4. Lógica de Giro del Sprite (FlipX)
        // La lógica de FlipX puede ir en Update o FixedUpdate, pero aquí funciona bien:
        if (inputX > 0)
        {
            // Moviéndose a la derecha: el sprite mira a la derecha (no volteado)
            spriteRenderer.flipX = false;
        }
        else if (inputX < 0)
        {
            // Moviéndose a la izquierda: voltear el sprite en el eje X
            spriteRenderer.flipX = true;
        }
    }
}