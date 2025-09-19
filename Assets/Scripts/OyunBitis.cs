using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyunBitis : MonoBehaviour
{
    [SerializeField] GameObject bitispanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bitispanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}
