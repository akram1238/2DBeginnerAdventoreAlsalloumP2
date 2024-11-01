using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectuble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayControler controller = other.GetComponent<PlayControler>();
        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                Destroy(gameObject);

            }
        }
    }
}
        
    

