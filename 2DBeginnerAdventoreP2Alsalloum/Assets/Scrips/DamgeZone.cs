using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayControler controller = other.GetComponent<PlayControler>();
        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
