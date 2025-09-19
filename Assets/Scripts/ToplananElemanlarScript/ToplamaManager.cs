using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToplamaManager : MonoBehaviour
{

    [SerializeField]
    bool isCoin;
    [SerializeField]
    bool isIksir;
    bool toplandimi;

    [SerializeField]
    GameObject patlamaEfekti;

    // triggerý acýk olan nesneye dokundugumuz anda calýsan fonksiyon budur.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && toplandimi == false)
        {
            if (isCoin)
            {
                toplandimi = true;
                SesManager.instance.KarisikSesEfektiCikar(6); // mucevher 6da
                Destroy(gameObject); // toplanan coini yok et
                Instantiate(patlamaEfekti, transform.position, Quaternion.identity);
                // bu scriptin kontrol ettigi coinler her playera degdiginde
                // gamemanager scriptindeki toplanancoinadete ulaþarak onu arttýrdýk.
                GameManager.instance.toplananCoinAdet++;
                UIManager.instance.CoinAdetGuncelle();
            }
            
            if(isIksir)
            {
                toplandimi = true;
                SesManager.instance.SesEfektiCikar(8);
                PlayerHealthController.instance.CaniArtir();

                Destroy(gameObject); // toplanan coini yok et
                Instantiate(patlamaEfekti, transform.position, Quaternion.identity);
                // bu scriptin kontrol ettigi coinler her playera degdiginde
                // gamemanager scriptindeki toplanancoinadete ulaþarak onu arttýrdýk.
                
            }
        }
    }

}
