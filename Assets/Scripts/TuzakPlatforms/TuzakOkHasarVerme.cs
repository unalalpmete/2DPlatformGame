using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TuzakOkHasarVerme : MonoBehaviour
{
    [SerializeField] private float hasarAraligi = 2f; // 2 saniye
    private bool okHasarVerebilir = true;

    private void OnTriggerStay2D(Collider2D other) // okun playera degmesi degil de player üzerinde kalma zamanýnda
    {
        if (other.gameObject.CompareTag("Player") && okHasarVerebilir == true)
        {
            StartCoroutine(HasarVer(other));
        }
    }
    private IEnumerator HasarVer(Collider2D player) // StartCoroutine(HasarVer(other)) çaðrýsý ile other deðeri player parametresine atanýr.
    {                                               // Yani player deðiþkeni OnTriggerStay2D’deki other Collider2D’ye referans olur.
        okHasarVerebilir = false;

        //player.GetComponent<PlayerHareketController>().GeriTepkiSayacBaslatma();
        player.GetComponent<PlayerHealthController>().CaniAzalt();
        SesManager.instance.SesEfektiCikar(3);

        yield return new WaitForSeconds(hasarAraligi);
        okHasarVerebilir = true;
    }

}
