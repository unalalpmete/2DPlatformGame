using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoaController : MonoBehaviour
{
    public static BoaController instance;

    [SerializeField]
    float boaYurumeHizi, boaKosmaHizi;

    Animator anim;
    Rigidbody2D rb;

    [SerializeField]
    float gorusMesafesi = 8f;

    [SerializeField]
    BoxCollider2D boaCollider;

    public bool oldumu;

    public LayerMask playerLayer;

    [SerializeField]
    GameObject kanamaEfekti;


    private void Awake()
    {
        instance = this;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        oldumu = false;
    }

    private void Update()
    {
        if (oldumu == true)
        {
            // boa oldu ise update fonklarini kontrol etme demektir.
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), gorusMesafesi, playerLayer);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * gorusMesafesi, Color.green);

        transform.localScale = new Vector3(-1, 1, 1);

        if (hit.collider)
        {
            // isinin carptigi nesnenin collideri
            if (hit.collider.CompareTag("Player"))
            {
                rb.velocity = new Vector2(-boaKosmaHizi, rb.velocity.y);
                anim.SetBool("kossunmu", true);
            }
        }
        // herhangi bir carpýsma yoksa boa yürüsün
        else
        {
            rb.velocity = new Vector2(-boaYurumeHizi, rb.velocity.y);
            anim.SetBool("kossunmu", false);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (boaCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (other.CompareTag("Player"))
            {
                anim.SetTrigger("atakYapti");

                other.GetComponent<PlayerHareketController>().GeriTepkiSayacBaslatma();
                other.GetComponent<PlayerHealthController>().CaniAzalt();
                SesManager.instance.SesEfektiCikar(3);
            }
        }
    }

    public void BoaOldu()
    {
        oldumu = true;
        anim.SetTrigger("canVerdi");
        SesManager.instance.SesEfektiCikar(1);

        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        Instantiate(kanamaEfekti, transform.position, transform.rotation);

        // boa nin icindeki boxColliderlari eklemis olduk
        foreach (BoxCollider2D box in GetComponents<BoxCollider2D>())
        {
            box.enabled = false;
        }

        Destroy(gameObject, 3f);

    }

}
