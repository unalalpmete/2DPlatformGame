using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MizrakController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boa") && other.GetComponent<BoaController>().oldumu == false)
        {
            Destroy(gameObject);
            other.GetComponent<BoaController>().BoaOldu();
        }
    }
}
