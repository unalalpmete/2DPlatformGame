using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiliciPasifYap : MonoBehaviour
{

    public GameObject kilicVurusBox;

    public void KiliciKapat()
    {
        // kilicSprite objesindeki kilicAttack animasyonunun son saniyesine event atadýk
        // ve o eventte kilicVurusBox nesnesini kapatacagýmýzý ayarladýk. 
        // bu sayede kilicVurusBox nesnesi sadece animasyon oynarken acýk olacak ve
        // kutu sadece bu animasyonun calýstýgý anlarda hasar alacak.
        kilicVurusBox.SetActive(false); 
    }

}
