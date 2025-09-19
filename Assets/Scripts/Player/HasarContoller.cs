using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasarContoller : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    private void Start()
    {
        player = GetComponent<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.CaniAzalt();
            PlayerHareketController.instance.GeriTepkiSayacBaslatma();
        }
    }
}
