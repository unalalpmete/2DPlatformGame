using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // sahneyi tekrar y�kleyecegmiz i�in ekldik

public class PlayerHareketController : MonoBehaviour
{
    // bu scripte di�er scriptlerden eri�mek istedigimiz i�in static yapt�k.
    public static PlayerHareketController instance;

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    GameObject normalPlayer, kilicPlayer, mizrakPlayer, okPlayer;

    [SerializeField]
    Transform zeminKontrolNoktasi;
    [SerializeField]
    Animator normalAnim, kilicAnim, mizrakAnim, okAnim;

    [SerializeField]
    SpriteRenderer normalSprite, kilicSprite, mizrakSprite, okSprite;

    [SerializeField]
    GameObject kilicVurusBoxObje;

    [SerializeField]
    GameObject atilacakOk;

    [SerializeField]
    Transform okCikisNoktasi;

    bool okAtabilirmi;

    public float hareketHizi;
    public float ziplamaGucu;

    bool isGrounded; // karakter zeminde mi ?
    public LayerMask zeminmaske; // inspector �zerinden layerimizi se�eriz ve
                                 // sadece o layere ait objelerler i�lem yapmas�n� saglar�z(z�plama)
    bool ikinciKezZiplasinmi;

    [SerializeField]
    float geriTepkiSuresi, geriTepkiGucu;

    float geriTepkiSayaci;

    bool yonSagdami;

    public bool playerCanverdimi; // �l� olup olmad�g�n� kontrol edecek

    bool kiliciVurdumu;

    [SerializeField]
    GameObject atilacakMizrak;

    [SerializeField]
    Transform mizrakCikisNoktasi;

    [SerializeField]
    GameObject normalKamera, kilicKamera, okKamera, mizrakKamera;

    

    private void Awake() // awake ilk �al�san fonksiyondur
    {
        kiliciVurdumu = false;

        instance = this;
        // PlayerHareketController.instance yazarak her yerden bu scripte ulasabiliriz.

        okAtabilirmi = true;

        rb = GetComponent<Rigidbody2D>();

        playerCanverdimi = false;

        // baslang�cta false yap�yoruz. fareye t�klad�g�m�zda true yap�p
        // vurus islemini sadece fareye t�klad�g�m�zda yapacak hale getirecez.
        kilicVurusBoxObje.SetActive(false);

        

    }

    private void Update()
    {
        if (playerCanverdimi == true)
        {
            return; // eger karakter �ld�yse update i�indeki di�er fonklar� gerceklestirmeden updateden ��k demektir.
        }

        if (geriTepkiSayaci <= 0)
        {
            HareketEt();
            Zipla();
            YonuDegistir();

            if (normalPlayer.activeSelf)
            {
                normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, 1f);
            }

            if (kilicPlayer.activeSelf)
            {
                kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, 1f);
            }

            if (mizrakPlayer.activeSelf) // mizrakPlayer aciksa
            {
                mizrakSprite.color = new Color(mizrakSprite.color.r, mizrakSprite.color.g, mizrakSprite.color.b, 1f);
            }

            if (okPlayer.activeSelf)
            {
                okSprite.color = new Color(okSprite.color.r, okSprite.color.g, okSprite.color.b, 1f);
            }

            // Kilic vurma animasyonu
            // kilicplayer aktifse
            if (Input.GetKeyDown(KeyCode.E) && kilicPlayer.activeSelf)
            {
                kiliciVurdumu = true;
                kilicVurusBoxObje.SetActive(true);
                SesManager.instance.SesEfektiCikar(4); // kilic sesi 4te
            }
            else
            {
                kiliciVurdumu = false;  
            }

            // mizrak vurma animasyonu
            // mizrakPlayer aktifse
            if (Input.GetKeyDown(KeyCode.E) && mizrakPlayer.activeSelf)
            {
                mizrakAnim.SetTrigger("mizrakAtti");
                Invoke("MizragiFirlat", .5f);
                SesManager.instance.SesEfektiCikar(5); // mizrak sesi 5te
            }

            // ok atma k�sm�
            if (Input.GetKeyDown(KeyCode.E) && okPlayer.activeSelf && okAtabilirmi == true)
            {
                okAnim.SetTrigger("okAtti");
                //Invoke("OkuFirlat", .4f);
                StartCoroutine(OkuAzSonraAtRoutine());
                SesManager.instance.SesEfektiCikar(7); // ok sesi 7de
            }

            


            /*if (okPlayer.activeSelf)
            {
                if (GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("TirmanmaLayer")))
                {
                    float h = Input.GetAxis("Vertical");
                    Vector2 tirmanisVector = new Vector2(rb.velocity.x, h * tirmanisHizi);
                    rb.velocity = tirmanisVector;
                    rb.gravityScale = 0f;
                    okAnim.SetBool("tirmansinmi", true);
                    okAnim.SetFloat("yukariHareketHizi", Mathf.Abs(rb.velocity.y));
                }
                else
                {
                    okAnim.SetBool("tirmansinmi", false);
                    rb.gravityScale = 1f;
                }
            }*/

        }
        else
        {
            // eger sayac 0dan b�y�kse bu sayac� geriye dogru sayd�r�yoruz yani azalt�yoruz.
            geriTepkiSayaci = geriTepkiSayaci - Time.deltaTime;

            if (yonSagdami == true)
            {
                // karakterimiz saga dogru hareket ediyosa tepkimiz sola(geriye) dogru olacag� i�in -geriTepkiGucu
                rb.velocity = new Vector2(-geriTepkiGucu, rb.velocity.y);
            }
            else
            {
                // karakterimiz sola dogru hareket ediyosa tepkimiz saga(ileriye) dogru olacag� i�in geriTepkiGucu
                rb.velocity = new Vector2(geriTepkiGucu, rb.velocity.y);
            }

        }

        if (normalPlayer.activeSelf == true)
        {
            // anim kontrol etme k�sm�
            normalAnim.SetBool("isGrounded", isGrounded);
            // sa�a giderken pozitif sola giderken negatif deger al�yor.
            // Biz idledan runa gecerken 0.1den buyuk olma sart� istedik ama sola dogru kosarken velocity - deger al�yor
            // ve animasyon oynam�yor. Bu yuzden mutlak i�ine al�p degerin hep pozitif ve animasyonun oynamas�n� saglad�k.
            normalAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
            // A veya D bast�g�m�zda karkterin h�z� 0.1den fazla olcak
            // yani x eks degeri kontrol edecek
        }

        if (kilicPlayer.activeSelf == true)
        {
            kilicAnim.SetBool("isGrounded", isGrounded);
            kilicAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));

        }

        if (kilicPlayer.activeSelf && kiliciVurdumu == true)
        {
            kilicAnim.SetTrigger("kiliciVurdu");
        }


        if (mizrakPlayer.activeSelf == true)
        {
            mizrakAnim.SetBool("zemindemi", isGrounded);
            mizrakAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
        }

        if (okPlayer.activeSelf == true)
        {
            okAnim.SetBool("zemindemi", isGrounded);
            okAnim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
        }

    }

    // mizragi firlatma animasyonu
    void MizragiFirlat()
    {
        GameObject mizrak = Instantiate(atilacakMizrak, mizrakCikisNoktasi.position, mizrakCikisNoktasi.rotation);
        // mizragin iki yone de atilabilmesi icin mizragin localScale ile karakterin localScale ayni yaptik
        mizrak.transform.localScale = transform.localScale;
        mizrak.GetComponent<Rigidbody2D>().velocity = mizrakCikisNoktasi.right * transform.localScale.x * 7f;

        Invoke("HerseyiKapatNormaliAc", .1f); 

    }

    // okufirlat
    IEnumerator OkuAzSonraAtRoutine()
    {
        okAtabilirmi = false;
        yield return new WaitForSeconds(.5f);
        OkPoolManager.instance.OkuFirlat(okCikisNoktasi, this.transform);
        okAtabilirmi = true;
    }

    

    void HareketEt()
    {
        float h = Input.GetAxis("Horizontal"); // h -1 ise sola(A), +1 ise sa�a(D) hareket eder
        rb.velocity = new Vector2(h*hareketHizi,rb.velocity.y);

    }

    void YonuDegistir()
    {
        // KARAKTER�N Y�N�N� DE���T�RME
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            yonSagdami = false;
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            yonSagdami = true;
        }

        /*if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }*/
    }

    void Zipla()
    {
        // �ember �eklinde ���n g�nderir
        // ���n layer� zeminmaskeye(Zemin) esit olan katmana gittiyse isGrounded = true olur
        // Karakterin alt�ndaki noktada, k���k bir daire i�inde bir zeminle temas var m�?
        isGrounded = Physics2D.OverlapCircle(zeminKontrolNoktasi.position, .2f, zeminmaske);

        // bas�l� tutlsa bile sadece bir kez �al�s�r
        if (Input.GetButtonDown("Jump") && (isGrounded == true || ikinciKezZiplasinmi == true))
        {
            if (isGrounded == true)
            {
                ikinciKezZiplasinmi = true;
            }
            else
            {
                ikinciKezZiplasinmi = false;
            }

            rb.velocity = new Vector2(rb.velocity.x,ziplamaGucu);

        }
    }

    public void GeriTepkiSayacBaslatma()
    {
        geriTepkiSayaci = geriTepkiSuresi;

        if (normalPlayer.activeSelf)
        {
            normalSprite.color = new Color(normalSprite.color.r, normalSprite.color.g, normalSprite.color.b, .5f);
        }

        if (kilicPlayer.activeSelf)
        {
            kilicSprite.color = new Color(kilicSprite.color.r, kilicSprite.color.g, kilicSprite.color.b, .5f);
        }

        if (mizrakPlayer.activeSelf)
        {
            mizrakSprite.color = new Color(mizrakSprite.color.r, mizrakSprite.color.g, mizrakSprite.color.b, .5f);
        }

        if (okPlayer.activeSelf)
        {
            okSprite.color = new Color(okSprite.color.r, okSprite.color.g, okSprite.color.b, .5f);
        }

        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void PlayerCanVerdi()
    {
        rb.velocity = Vector2.zero; // h�z� s�f�rlad�k
        playerCanverdimi = true;

        if (normalPlayer.activeSelf == true)
        {
            normalAnim.SetTrigger("canVerdi");
        }

        if (kilicPlayer.activeSelf == true)
        {
            kilicAnim.SetTrigger("canVerdi");
        }

        if (mizrakPlayer.activeSelf == true)
        {
            mizrakAnim.SetTrigger("canVerdi");
        }

        if (okPlayer.activeSelf == true)
        {
            okAnim.SetTrigger("canVerdi");
        }

        StartCoroutine(PlayerYokEtSahneYenile());

    }

    IEnumerator PlayerYokEtSahneYenile()
    {
        yield return new WaitForSeconds(2f);
        // player� yok edersek bu kod sayfas� playere ait oldugu i�in bu sat�rdan sonras�n� gerceklestiremeyecek
        // yani objeyi yok etmek yerine playerspritenin spriterendererini yok etmeliyiz
        //Destroy(gameObject);
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);

        // aktif olan sahnenin index numaras�n� al ve buraya ata(y�kle) demektir
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void HerseyiKapatKiliciAc()
    {
        TumKameralariKapat();
        kilicKamera.SetActive(true);

        normalPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
        kilicPlayer.SetActive(true);
        okPlayer.SetActive(false);
    }

    public void HerseyiKapatMizrakiAc()
    {
        TumKameralariKapat();
        mizrakKamera.SetActive(true);

        normalPlayer.SetActive(false);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(true);
        okPlayer.SetActive(false);
    }

    public void HerseyiKapatNormaliAc()
    {
        TumKameralariKapat();
        normalKamera.SetActive(true);

        normalPlayer.SetActive(true);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
        okPlayer.SetActive(false);
    }

    public void HerseyiKapatOkuAc()
    {
        TumKameralariKapat();
        okKamera.SetActive(true);

        normalPlayer.SetActive(false);
        kilicPlayer.SetActive(false);
        mizrakPlayer.SetActive(false);
        okPlayer.SetActive(true);
    }

    void TumKameralariKapat()
    {
        normalKamera.SetActive(false);
        kilicKamera.SetActive(false);
        mizrakKamera.SetActive(false);
        okKamera.SetActive(false);
    }

}
