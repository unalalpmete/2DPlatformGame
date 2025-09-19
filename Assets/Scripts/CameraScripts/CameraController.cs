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
    //private float parallaxMultiplier = 0.5f; // parallax katsayýsý

    

    private void Start()
    {
        halfYukseklik = Camera.main.orthographicSize;
        halfGenislik = halfYukseklik * Camera.main.aspect; //kamera görüntüsünün geniþliðinin yüksekliðine oranýný verir
        // sýnýrlamalarda bunlarý da hesaba katmalýyýz.
        // Oyun basladýgýnda kameranýn bizim ayarladýgýmýz yerden asagýda ve geride baslamasýný önlemis olcaz.

        sonPos = transform.position; // Kameranýn baþlangýç pozisyonu

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
        // kameranýn herhengi bir bulundugu noktadan(transform.position.x/transform.position.y)
        // ilk yapmýs oldugumuz sonPos.x arasýndaki farký hesaplýyoruz. 
        // Kameranýn önceki pozisyona göre ne kadar hareket ettiðini buldýk
        Vector2 aradakiFark = new Vector2(transform.position.x - sonPos.x, transform.position.y - sonPos.y);

        // transform.position demedik, deseydik kameranýn pozisyonu degisirdi.
        // Biz backgrounds pozisyonunun degismesini istiyoruz.
        // Arka planý, bu farkýn sadece belli bir oraný kadar hareket ettir(parallax olmasý için)
        backgrounds.position += new Vector3(
            aradakiFark.x,
            aradakiFark.y,
            0f);
        // Þu anki kamera pozisyonunu kaydeder.Bir sonraki karede karþýlaþtýrma yapabilmek için.
        sonPos = transform.position;
    }

}
