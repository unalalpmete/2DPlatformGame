using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iskeletAttackController : MonoBehaviour
{
    [SerializeField]
    Transform attackPos;

    [SerializeField]
    float attackYaricap;

    [SerializeField]
    LayerMask playerLayer;

    public void AtakYap()
    {
        // bir daire �izer ve bu dairenin i�inde kalan ilk Collider2D nesnesini d�nd�r�r.
        // Bu kod d��man�n yak�n sald�r� (melee attack) yapabilmesi i�in kullan�l�r.
        // cizilen daire icine giren colliderlar arasinda layeri playerLayer olan collideri bul.
        Collider2D playerCollider = Physics2D.OverlapCircle(attackPos.position, attackYaricap, playerLayer);
        // istenen collider varsa ve player olmediyse
        if (playerCollider != null && playerCollider.GetComponent<PlayerHareketController>().playerCanverdimi == false)
        {
            playerCollider.GetComponent<PlayerHareketController>().GeriTepkiSayacBaslatma();
            playerCollider.GetComponent<PlayerHealthController>().CaniAzalt();
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackYaricap);
    }


}
