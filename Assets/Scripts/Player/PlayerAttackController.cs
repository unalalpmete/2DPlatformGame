using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D kilicVurusBox;

    [SerializeField]
    GameObject parlamaEfekti;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // kilicVurusBox layerý DusmanLayer olan nesneye degerse saldiriyi gerceklestir.
        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("DusmanLayer")))
        {
            if (other.CompareTag("Orumcek"))
            {
                if (parlamaEfekti)// parlamaEfekti bos degilse calýstýr demek
                {
                    Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
                }
                StartCoroutine(other.GetComponent<OrumcekController>().GeriTepki());
            }
        }

        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("DusmanLayer")))
        {
            if (other.CompareTag("Bat"))
            {
                if (parlamaEfekti)// parlamaEfekti bos degilse calýstýr demek
                {
                    Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
                }
                other.GetComponent<BatController>().CaniAzalt();
            }
        }

        if (kilicVurusBox.IsTouchingLayers(LayerMask.GetMask("IskeletLayer")))
        {
            if (other.CompareTag("iskelet"))
            {
                if (parlamaEfekti)// parlamaEfekti bos degilse calýstýr demek
                {
                    Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
                }
                other.GetComponent<iskeletHealthController>().CaniAzalt();
            }
        }

    }

}
