using UnityEngine;
using System.Collections;

public class StairsCollider : MonoBehaviour


{
    void Update()
    {
        
    }

    void OnCollisionEnter2D (Collision2D collided)
    {
        if (collided.gameObject.CompareTag("Puppy"))
        {
            Debug.Log("The dog has touched the stairs");
        }

    }

}
