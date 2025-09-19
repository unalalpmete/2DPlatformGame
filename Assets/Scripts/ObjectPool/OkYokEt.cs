using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkYokEt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // false oldukca ok atilabiliyodu
        if (other.CompareTag("ok"))
        {
            other.gameObject.SetActive(false);
        }
    }

}
