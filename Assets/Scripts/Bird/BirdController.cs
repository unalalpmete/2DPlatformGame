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
            // bekleme sayac� 0dan buyukse bekleme an�ndad�r ve ucmayacakt�r. yani ucsunMu = false olmal� 
            anim.SetBool("ucsunMu", false);
        }
        else
        {
            // kus farkl� noktalar aras�nda ucarken kusun y�n�n� dogru ve uygun ayarlama //
            // iki nokta aras�ndaki vektor� bulduk
            kusYonu = new Vector2(pozisyonlar[kacinciPozisyon].position.x - transform.position.x, pozisyonlar[kacinciPozisyon].position.y - transform.position.y);
            // Atan2 ac�n�n radyan�n� bulur. Mathf.Rad2Deg ile radyan� carparsak a��n�n kendisini buluruz.
            float angle = Mathf.Atan2(kusYonu.y, kusYonu.x)*Mathf.Rad2Deg;

            //E�er objenin X konumu, hedef pozisyonun X konumundan b�y�k veya e�itse yani hedefin sa��na ge�mi�se localScale de�i�tiriliyor.
            //localScale'in Y de�eri 1 iken -1 oluyor.
            //Bu, objeyi Y ekseninde ters �evirir(dikey olarak ayna efekti).
            if (transform.position.x >= pozisyonlar[kacinciPozisyon].position.x)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            // kusumuzun y�n�n�(rotasyon) ayarlad�k
            transform.rotation = Quaternion.Euler(0, 0, angle);


            anim.SetBool("ucsunMu", true);
            // movetowards ile ku�u istedgimiz pozisyona hareket ettiriyoruz.
            transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, birdSpeed * Time.deltaTime);

            // iki nokta aras� mesafeyi �l�er
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
