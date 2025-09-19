using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    // bu scripte diðer scriptlerden eriþmek istedigimiz için static yaptýk.
    public static PlayerHealthController instance; // singleton patterni

    public int maxSaglik, gecerliSaglik;

    private void Awake()
    {
        instance = this; // playerhealthcontrollere her yerden ulasabilmek için böyle yaptýk.
        // PlayerHealthController.instance yazarak her yerden bu scripte ulasabiliriz.
    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;

        // UIManager nesnesi var ise
        if (UIManager.instance != null)
        {
            UIManager.instance.PSlideriGuncelle(gecerliSaglik, maxSaglik);
        }

        
    }

    public void CaniAzalt()
    {
        gecerliSaglik--;
        SesManager.instance.SesEfektiCikar(3); // karakter darbe alma 3te
        UIManager.instance.PSlideriGuncelle(gecerliSaglik, maxSaglik);

        if (gecerliSaglik<=0)
        {
            gecerliSaglik = 0;

            PlayerHareketController.instance.PlayerCanVerdi();
        }
    }

    public void CaniArtir()
    {
        gecerliSaglik++;
        if (gecerliSaglik >= maxSaglik)
        {
            gecerliSaglik = maxSaglik;
        }

        UIManager.instance.PSlideriGuncelle(gecerliSaglik, maxSaglik);

    }

}
