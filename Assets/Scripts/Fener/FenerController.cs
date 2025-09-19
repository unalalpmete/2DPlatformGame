using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenerController : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer fenerSprRenderer;

    [SerializeField]
    Sprite fenerOnSprite, fenerOffSprite;

    private void Awake()
    {
        fenerSprRenderer.sprite = fenerOffSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fenerSprRenderer.sprite = fenerOnSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("FeneriKapat", 0.5f); // invoke içindeki fonksiyonu geç çalýstýrýr.
        }
    }

    void FeneriKapat()
    {
        fenerSprRenderer.sprite = fenerOffSprite;
    }

}
