using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkController : MonoBehaviour
{
    [SerializeField]
    GameObject parlamaEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("IskeletLayer")))
        {
            if (other.CompareTag("iskelet"))
            {
                gameObject.SetActive(false);
                Instantiate(parlamaEffect, other.transform.position, other.transform.rotation);
                other.GetComponent<iskeletHealthController>().CaniAzalt();
            }
        }
    }

}
