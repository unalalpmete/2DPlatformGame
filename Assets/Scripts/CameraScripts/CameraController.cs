using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    

    float halfYukseklik, halfGenislik;

    // background kamera ile birlikte hareket etmesi
    Vector2 sonPos;
    [SerializeField]
    Transform backgrounds;
    [SerializeField]
    //private float parallaxMultiplier = 0.5f; // parallax katsay�s�

    

    private void Start()
    {
        halfYukseklik = Camera.main.orthographicSize;
        halfGenislik = halfYukseklik * Camera.main.aspect; //kamera g�r�nt�s�n�n geni�li�inin y�ksekli�ine oran�n� verir
        // s�n�rlamalarda bunlar� da hesaba katmal�y�z.
        // Oyun baslad�g�nda kameran�n bizim ayarlad�g�m�z yerden asag�da ve geride baslamas�n� �nlemis olcaz.

        sonPos = transform.position; // Kameran�n ba�lang�� pozisyonu

    }

    private void Update()
    {
        
        if (backgrounds != null)
        {
            BackGroundHareket();    
        }
        
    }

    void BackGroundHareket()
    {
        // kameran�n herhengi bir bulundugu noktadan(transform.position.x/transform.position.y)
        // ilk yapm�s oldugumuz sonPos.x aras�ndaki fark� hesapl�yoruz. 
        // Kameran�n �nceki pozisyona g�re ne kadar hareket etti�ini buld�k
        Vector2 aradakiFark = new Vector2(transform.position.x - sonPos.x, transform.position.y - sonPos.y);

        // transform.position demedik, deseydik kameran�n pozisyonu degisirdi.
        // Biz backgrounds pozisyonunun degismesini istiyoruz.
        // Arka plan�, bu fark�n sadece belli bir oran� kadar hareket ettir(parallax olmas� i�in)
        backgrounds.position += new Vector3(
            aradakiFark.x,
            aradakiFark.y,
            0f);
        // �u anki kamera pozisyonunu kaydeder.Bir sonraki karede kar��la�t�rma yapabilmek i�in.
        sonPos = transform.position;
    }

}
