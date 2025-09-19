using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IskeletHareketController : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;

    int kacinciPozisyon;
    public float iskeletHizi = 4f;
    public float beklemeSuresi = 1f;
    float beklemeSayaci;
    bool sinirIcindemi;

    Transform playerHedef;
    Rigidbody2D rb;
    Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sinirIcindemi = false;
    }

    private void Start()
    {
        kacinciPozisyon = 0;

        beklemeSayaci = beklemeSuresi;
        // playerHedefin transformunu Playerimizinkine esitledik.
        playerHedef = GameObject.Find("Player").transform;

        // oyun basladiginda her pozisyonun ebeveynini null yapiyoz
        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null;
        }

    }

    private void Update()
    {
        if (playerHedef.GetComponent<PlayerHareketController>().playerCanverdimi == true || GetComponent<iskeletHealthController>().iskeletOldumu == true) 
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("atakYapti", false);
            return;
        }

        float mesafe = Vector2.Distance(playerHedef.position, transform.position);

        if (mesafe > 6)
        {
            sinirIcindemi = false;
        }
        else
        {
            sinirIcindemi = true;   
        }

        // iskeletin playere saldirmayacagi durumda yapacagi islem
        if (sinirIcindemi == false)
        {
            if (Mathf.Abs(transform.position.x - pozisyonlar[kacinciPozisyon].position.x) >= 0.2f)
            {
                if (transform.position.x < pozisyonlar[kacinciPozisyon].position.x)
                {
                    rb.velocity = new Vector2(iskeletHizi, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-iskeletHizi, rb.velocity.y);
                }

                // Mathf.Sign() bir seyin isaretini al demek
                // karakterin saga sola gidecegi yonu belirlemesini kolaylastirir
                transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            }
            else // diger posa yani bekleme noktasina geldiginde
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                beklemeSayaci -= Time.deltaTime;
                if (beklemeSayaci <= 0)
                {
                    beklemeSayaci = beklemeSuresi; // bu sayaci baslatma islemi
                    kacinciPozisyon++; // diger posa gitmesi icin bunu arttiriyoruz
                    if (kacinciPozisyon >= pozisyonlar.Length)
                    {
                        kacinciPozisyon = 0; // basa dondurduk
                    }
                }
            }

        }
        else // sinirin icindeyse
        {
            Vector2 yonVectoru = transform.position - playerHedef.position;

            if (yonVectoru.magnitude > 2f && playerHedef != null)
            {
                if (yonVectoru.x > 0) // sola dogru gidecek
                {
                    rb.velocity = new Vector2(-iskeletHizi, rb.velocity.y);
                }
                else // saga dogru gidecek
                {
                    rb.velocity = new Vector2(iskeletHizi, rb.velocity.y);
                }
                // isaret alir + - karakterin hangi yone gidecegini belirler
                transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
                anim.SetBool("atakYapti", false);
            }
            else // yani iskelet playera cok yakinsa
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetBool("atakYapti", true);
            }
        }

        anim.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
    }

}
