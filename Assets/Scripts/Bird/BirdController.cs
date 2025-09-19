using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;

    public float birdSpeed;
    public float beklemeSuresi;
    float beklemeSayac;
    int kacinciPozisyon;
    Animator anim;
    Vector2 kusYonu;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null;
        }
    }

    private void Start()
    {
        kacinciPozisyon = 0;
        transform.position = pozisyonlar[kacinciPozisyon].position;
    }

    private void Update()
    {
        if (beklemeSayac > 0)
        {
            beklemeSayac -= Time.deltaTime;
            // bekleme sayacý 0dan buyukse bekleme anýndadýr ve ucmayacaktýr. yani ucsunMu = false olmalý 
            anim.SetBool("ucsunMu", false);
        }
        else
        {
            // kus farklý noktalar arasýnda ucarken kusun yönünü dogru ve uygun ayarlama //
            // iki nokta arasýndaki vektorü bulduk
            kusYonu = new Vector2(pozisyonlar[kacinciPozisyon].position.x - transform.position.x, pozisyonlar[kacinciPozisyon].position.y - transform.position.y);
            // Atan2 acýnýn radyanýný bulur. Mathf.Rad2Deg ile radyaný carparsak açýnýn kendisini buluruz.
            float angle = Mathf.Atan2(kusYonu.y, kusYonu.x)*Mathf.Rad2Deg;

            //Eðer objenin X konumu, hedef pozisyonun X konumundan büyük veya eþitse yani hedefin saðýna geçmiþse localScale deðiþtiriliyor.
            //localScale'in Y deðeri 1 iken -1 oluyor.
            //Bu, objeyi Y ekseninde ters çevirir(dikey olarak ayna efekti).
            if (transform.position.x >= pozisyonlar[kacinciPozisyon].position.x)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            // kusumuzun yönünü(rotasyon) ayarladýk
            transform.rotation = Quaternion.Euler(0, 0, angle);


            anim.SetBool("ucsunMu", true);
            // movetowards ile kuþu istedgimiz pozisyona hareket ettiriyoruz.
            transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, birdSpeed * Time.deltaTime);

            // iki nokta arasý mesafeyi ölçer
            if (Vector3.Distance(transform.position, pozisyonlar[kacinciPozisyon].position) < 0.1f)
            {
                PozisyonuDegistir();
                beklemeSayac = beklemeSuresi;
            }

        }
    }

    void PozisyonuDegistir()
    {
        kacinciPozisyon++;
        if (kacinciPozisyon >= pozisyonlar.Length)
        {
            kacinciPozisyon = 0;
        }
    }

}
