using System;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.SceneManagement;

public class PuppyMovement : MonoBehaviour
{
    // Esta variable será visible en el Inspector, ajusta la velocidad aqui.
    public float movementSpeed;
    // Referencia al SpriteRenderer para poder voltear el sprite
    private SpriteRenderer spriteRenderer;
    public int counter = 0;

    void Start()
    {
        // Obtener el componente SpriteRenderer al inicio del juego
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer no encontrado en el objeto del cachorro.");
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Scene2" && counter == 0)
        {
            Debug.Log("Nivel 2 - Velocidad mas rapida del cachorro.");
            movementSpeed = 3f; // Aumentar la velocidad en el Nivel 2
            counter++; // sumar 1 para que no se vuelva a controlar
        } else if ((SceneManager.GetActiveScene().name == "Scene1" && counter == 0))
        {
            Debug.Log("Nivel 1 - Velocidad lenta del cachorro");
            movementSpeed = 1.5f; // Velocidad lenta nivel 1
            counter++;
        }
        // 1. Obtener la entrada del jugador (WASD)
        // GetAxisRaw proporciona valores de -1, 0, o 1 (sin suavizado, perfecto para pixel art)
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // 2. Calcular el vector de movimiento
        Vector2 movement = new Vector2(inputX, inputY).normalized;

        // 3. Aplicar el movimiento. Usamos Time.deltaTime para movimiento fluido e independiente del frame rate.
        // Transform.Translate mueve el objeto Kinematic sin problemas.
        transform.Translate(movement * movementSpeed * Time.deltaTime);

        // 4. Lógica de Giro del Sprite (para que mire hacia donde camina)
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