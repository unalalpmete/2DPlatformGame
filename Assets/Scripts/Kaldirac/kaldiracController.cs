using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaldiracController : MonoBehaviour
{
    [SerializeField]
    GameObject acilacakEngel;

    [SerializeField]
    Animator engelAnim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ok"))
        {
            GetComponent<Animator>().SetTrigger("kaldiracAcilsin");
            GetComponent<BoxCollider2D>().enabled = false;
            Invoke("engeliAc", 0.5f);
        }

    }

    void engeliAc()
    {
        engelAnim.SetTrigger("engelHareket");
    }

}
