using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiliciPasifYap : MonoBehaviour
{

    public GameObject kilicVurusBox;

    public void KiliciKapat()
    {
        // kilicSprite objesindeki kilicAttack animasyonunun son saniyesine event atad�k
        // ve o eventte kilicVurusBox nesnesini kapatacag�m�z� ayarlad�k. 
        // bu sayede kilicVurusBox nesnesi sadece animasyon oynarken ac�k olacak ve
        // kutu sadece bu animasyonun cal�st�g� anlarda hasar alacak.
        kilicVurusBox.SetActive(false); 
    }

}
