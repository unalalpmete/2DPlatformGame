using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Transform altPoint;
    Animator anim;
    Vector3 hareketYonu = Vector3.up; // degdigimizde block yukarý hareket edecek
    Vector3 orijinalPos;
    Vector3 animPos;

    public LayerMask playerLayer;

    bool animasyonBaslasinmi;
    bool hareketEtsinmi = true;

    public GameObject coinPrefab;
    Vector3 coinPos;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        orijinalPos = transform.position;
        animPos = transform.position;
        animPos.y += 0.15f;

        coinPos = transform.position;
        coinPos.y += 1f;
    }

    private void Update()
    {
        CarpismayiKontrolEt(); // karakterimiz ýþýna degdi mi diye her framede kontrol edecek
        AnimasyonuBaslat();
    }

    void CarpismayiKontrolEt()
    {
        if (hareketEtsinmi == true)
        {
            // altPointten ýþýný gönderdik. nesnenin layeri playerlayer(parlayanblock) ise ýþýný gönderecek.
            RaycastHit2D hit = Physics2D.Raycast(altPoint.position, Vector2.down, .1f, playerLayer);
            //Debug.DrawRay(altPoint.position,Vector2.down,Color.red);

            if (hit && hit.collider.gameObject.CompareTag("Player"))
            {
                anim.Play("mat");
                animasyonBaslasinmi = true;
                hareketEtsinmi = false;

                //parlayanbloklara carpýnca ustlerinde coin oluþturma iþlemi
                Instantiate(coinPrefab, coinPos, Quaternion.identity);
            }
        }
        
    }

    void AnimasyonuBaslat()
    {
        if (animasyonBaslasinmi == true)
        {
            transform.Translate(hareketYonu * Time.smoothDeltaTime);

            if (transform.position.y >= animPos.y)
            {
                hareketYonu = Vector3.down; // carpýsmadan dolayý block yukarý cýktýgýnda bu satýr gerceklesecek

            }
            else if (transform.position.y <= orijinalPos.y)
            {
                animasyonBaslasinmi = false;
            }

        }
       
    }

}
