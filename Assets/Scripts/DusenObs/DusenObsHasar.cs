using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusenObsHasar : MonoBehaviour
{
    
    [SerializeField]
    LayerMask zeminLayer;

    [SerializeField]
    LayerMask playerLayer;


    private void Update()
    {
        if (gameObject.GetComponent<Collider2D>().IsTouchingLayers(zeminLayer) || gameObject.GetComponent<Collider2D>().IsTouchingLayers(playerLayer))
        {
            Invoke("ObsKaldir", 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.GetComponent<Collider2D>().IsTouchingLayers(playerLayer))
            {
                PlayerHealthController.instance.CaniAzalt();

            }
        }
    }

    void ObsKaldir()
    {
        gameObject.SetActive(false);
    }

}
