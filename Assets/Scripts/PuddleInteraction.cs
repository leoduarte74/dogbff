using UnityEngine;

public class CharcoInteraccion : MonoBehaviour
{
    // Variable para saber si el cachorro está dentro del área de interacción.
    private bool isPuppyInside = false;

    // Se llama cuando algo entra en el Trigger.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Usa el Tag "Puppy" para verificar si es nuestro personaje.
        if (other.CompareTag("Puppy"))
        {
            isPuppyInside = true;
            Debug.Log("Cachorro entró en rango. Presiona 'E'.");
            // Nota: Aquí podrías mostrar un ícono de "Presiona E" sobre el charco.
        }
    }

    // Se llama cuando algo sale del Trigger.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Puppy"))
        {
            isPuppyInside = false;
            Debug.Log("Cachorro salió del rango.");
        }
    }

    // Se llama en cada frame para detectar la pulsación de la tecla.
    void Update()
    {
        // Si el cachorro está dentro Y presionamos la tecla 'E'
        if (isPuppyInside && Input.GetKeyDown(KeyCode.E))
        {
            // ¡La interacción narrativa!
            Debug.Log("Cachorro interactuando: *Chapoteo triste*.");

            // TODO: Aquí luego añadiremos la llamada a tu sistema de Pensamientos (T)
        }
    }
}