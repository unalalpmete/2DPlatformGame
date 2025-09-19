using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrumcekController : MonoBehaviour
{
    public static OrumcekController instance;

    [SerializeField]
    // konum(orumcegin gitgel yapacagi noktalar) belirtecegimiz zaman Transform kullanýrýz
    Transform[] pozisyonlar;

    [SerializeField]
    Slider orumcekSlider;

    public int maxSaglik;
    public int gecerliSaglik;

    public float orumcekHizi;

    // orumcegin uc noktalara geldigindeki bekleme suresi
    public float beklemeSuresi;
    float beklemeSayac;

    // orumcegin animasyonlarina ulasabilmek icin
    Animator anim;

    int kacinciPozisyon;

    // orumcegin saldirmaya calisacagi nesne
    Transform hedefPlayer;

    public float takipMesafesi = 5f;

    BoxCollider2D orumcekCollider;

    bool atakYapabilirmi;

    Rigidbody2D rb;

    [SerializeField]
    GameObject iksirPrefab;

    private void Awake()
    {
        instance = this;

        // bu scripte ait olan animatore ve ozelliklerine ulasabiliriz.
        anim = GetComponent<Animator>();
        orumcekCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;
        orumcekSlider.maxValue = maxSaglik;
        orumcekSlider.value = gecerliSaglik;
        // oyun basinda orumcegin caninin sliderda full gozukmesi icin
        SliderGuncelle();

        atakYapabilirmi = true;

        // tagi Player olan nesneyi bul ve onun transformunu hedefPlayer transforma esitle
        hedefPlayer = GameObject.Find("Player").transform;

        foreach (Transform pos in pozisyonlar)
        {
            // oyun basladigi an orumcegin cocuk objesi olan pozisyon noktalarýný dýþarý cýkarýp
            // ayrý bir obje haline getirmeliyiz ki orumcek hareket ettiginde noktalar da hareket etmesin.
            // eger cýkarmazsak orumcek hareket ettiginde noktalar da orumcegin cocuk objeleri oldugu için hareket eder ve
            // orumcek asla bu git gel yapacagi pozisyon noktalarina ulasamaz.
            // bu dizideki her elemani bosa cikar(ebeveyinlerini bos yap)
            pos.parent = null;
        }
    }

    private void Update()
    {
        if (atakYapabilirmi == false)
            return;

       

        if (beklemeSayac > 0)
        {
            // orumcek bu noktada duruyor.
            beklemeSayac -= Time.deltaTime;
            anim.SetBool("hareketEtsinmi", false);
        }
        else
        {
            // karakter orumcegin sinirlarina girdiginde orumcegin karakteri takip etmesi
            if (hedefPlayer.position.x > pozisyonlar[0].position.x && hedefPlayer.position.x < pozisyonlar[1].position.x)
            {
                Vector3 yeniPos = hedefPlayer.position;
                yeniPos.y = transform.position.y;

                transform.position = Vector3.MoveTowards(transform.position, yeniPos, orumcekHizi * Time.deltaTime);
                anim.SetBool("hareketEtsinmi", true);

                // karakterin iki nokta arasinda sola saga giderken yonunu ayarladik
                // yonu harekete baslamadan once ayarladiks
                if (transform.position.x > hedefPlayer.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < hedefPlayer.position.x)
                {
                    transform.localScale = new Vector3(+1, 1, 1);
                }

            }
            else
            {
                // bekleme suresi bittiyse orumcegin hareket etmesi gerek
                anim.SetBool("hareketEtsinmi", true);

                // karakterin iki nokta arasinda sola saga giderken yonunu ayarladik
                // yonu harekete baslamadan once ayarladiks
                if (transform.position.x > pozisyonlar[kacinciPozisyon].position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (transform.position.x < pozisyonlar[kacinciPozisyon].position.x)
                {
                    transform.localScale = new Vector3(+1, 1, 1);
                }

                // hareketimizi saglamaliyiz
                // MoveTowards nesneyi bir noktadan bir noktaya gitmesini saglayan fonksiyondu
                // maxDistanceDelta(3.parametre) 1 adýmda ne kadar yol alinacagini belirtir.
                transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, orumcekHizi * Time.deltaTime);

                if (Vector3.Distance(transform.position, pozisyonlar[kacinciPozisyon].position) < 0.1f)
                {
                    beklemeSayac = beklemeSuresi;
                    // orumcegin bekleme islemi bittikten sonra diger pozisyona gitmesi gerek 
                    PozisyonuDegistir();
                }
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

    // takip edilecek mesafeyi Unitye çizdirebiliriz(yeni yontem)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        // 1.parametre merkez, 2. ise yaricap
        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }

    // orumcegin karaktere hasar vermesi

    private void OnTriggerEnter2D(Collider2D other)
    {
        //orumcekcollider layermaski PlayerLayer olan bir nesneye carparsa orumcek atak yapsin
        if (orumcekCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")) && atakYapabilirmi == true)
        {
            atakYapabilirmi = false;

            anim.SetTrigger("atakYapti");
            other.GetComponent<PlayerHareketController>().GeriTepkiSayacBaslatma();
            other.GetComponent<PlayerHealthController>().CaniAzalt();

            StartCoroutine(YenidenAtakYapsin());
        }
    }

    IEnumerator YenidenAtakYapsin()
    {
        yield return new WaitForSeconds(1f);
        
        if(gecerliSaglik > 0)
        {
            atakYapabilirmi = true;
        }
           
    }

    public IEnumerator GeriTepki()
    {
        // orumcek darbe aldiysa atak yapamamasi lazim
        atakYapabilirmi = false;
        rb.velocity = Vector2.zero; // hareketi engellemek
        yield return new WaitForSeconds(.1f);

        gecerliSaglik--;
        SesManager.instance.SesEfektiCikar(1);

        SliderGuncelle();

        if (gecerliSaglik <= 0)
        {
            atakYapabilirmi = false;
            gecerliSaglik = 0;

            Instantiate(iksirPrefab, transform.position, Quaternion.identity);

            anim.SetTrigger("canVerdi");
            orumcekCollider.enabled = false;
            Destroy(gameObject, 2f);
            orumcekSlider.gameObject.SetActive(false);
        }
        else
        {
            // orumcek hasar aldiginda canvermeyecekse geri tepki vermeli
            for (int i = 0; i < 5; i++)
            {
                // yonunun tersine tepki vermesi icin - dedik
                rb.velocity = new Vector2(-transform.localScale.x + i, rb.velocity.y);

                yield return new WaitForSeconds(0.05f);
            }

            anim.SetBool("hareketEtsinmi", false);

            yield return new WaitForSeconds(0.25f);

            rb.velocity = Vector2.zero;
            atakYapabilirmi = true;
        }

    }

    public void SliderGuncelle()
    {
        orumcekSlider.value = gecerliSaglik;
    }

}
