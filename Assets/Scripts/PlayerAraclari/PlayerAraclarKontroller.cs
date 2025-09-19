using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAraclarKontroller : MonoBehaviour
{
    [SerializeField]
    bool kilicmi, mizrakmi, yaymi;

    [SerializeField]
    Animator okAnim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other != null && kilicmi == true)
            {
                other.GetComponent<PlayerHareketController>().HerseyiKapatKiliciAc();
                
            }

            if (other != null && mizrakmi == true)
            {
                other.GetComponent<PlayerHareketController>().HerseyiKapatMizrakiAc();
            }

            if (other != null && yaymi == true)
            {
                other.GetComponent<PlayerHareketController>().HerseyiKapatOkuAc();
                
            }

            Destroy(gameObject);
        }
    }
}
