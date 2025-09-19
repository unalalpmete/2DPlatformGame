using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirilanManager : MonoBehaviour
{
    [SerializeField]
    bool sandikmi, korkulukmu;

    Animator anim;

    int kacinciVurus;

    [SerializeField]
    GameObject parlamaEfekti;

    [SerializeField]
    GameObject coinPrefab;

    Vector2 patlamaMiktari = new Vector2(1, 4);

    private void Awake()
    {
        anim = GetComponent<Animator>();
        kacinciVurus = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("kilicVurusBox"))
        {
            // sandik
            if (sandikmi == true)
            {
                if (kacinciVurus == 0)
                {
                    anim.SetTrigger("sallanma");
                    Instantiate(parlamaEfekti, transform.position, transform.rotation);
                }
                else if (kacinciVurus == 1)
                {
                    anim.SetTrigger("sallanma");
                    Instantiate(parlamaEfekti, transform.position, transform.rotation);
                }
                else
                {
                    GetComponent<BoxCollider2D>().enabled = false;

                    SesManager.instance.SesEfektiCikar(9); // sandik sesi 9da

                    anim.SetTrigger("parcalanma");

                    for (int i = 0; i < 3; i++)
                    {
                        // 3 alt�n da ayn� yerde olu�tugu i�in 1 alt�n varm�s gibi duruyordu.
                        // Farkl� yerlerde olu�turarak bunu �nledik.
                        // 1. alt�n default noktan�n solunda, 2.si default noktada, 3.s� de defaultun sa��nda oluscak.
                        Vector3 rastgeleVector = new Vector3(transform.position.x + (i - 1), transform.position.y, transform.position.z);

                        GameObject coin = Instantiate(coinPrefab, rastgeleVector, transform.rotation);

                        // olusan coinlere hareket verebilmek i�in bodytypelar�n� dynamic yapt�k.
                        coin.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                        // coinler olustuktan sonra rastgele saga sola s�cras�n
                        coin.GetComponent<Rigidbody2D>().velocity = patlamaMiktari * new Vector2(Random.Range(1, 2), transform.localScale.y + Random.Range(0, 2));
                    }

                }
                kacinciVurus++;
            }

            // korkuluk
            if (korkulukmu == true)
            {
                if (kacinciVurus == 0)
                {
                    Instantiate(parlamaEfekti, transform.position, transform.rotation);
                    SesManager.instance.SesEfektiCikar(3); // darbe alma 3te
                }
                else if (kacinciVurus == 1)
                {
                    Instantiate(parlamaEfekti, transform.position, transform.rotation);
                    SesManager.instance.SesEfektiCikar(3); // darbe alma 3te
                }
                else
                {
                    GetComponent<BoxCollider2D>().enabled = false;


                    for (int i = 0; i < 3; i++)
                    {
                        // 3 alt�n da ayn� yerde olu�tugu i�in 1 alt�n varm�s gibi duruyordu.
                        // Farkl� yerlerde olu�turarak bunu �nledik.
                        // 1. alt�n default noktan�n solunda, 2.si default noktada, 3.s� de defaultun sa��nda oluscak.
                        Vector3 rastgeleVector = new Vector3(transform.position.x + (i - 1), transform.position.y, transform.position.z);

                        GameObject coin = Instantiate(coinPrefab, rastgeleVector, transform.rotation);

                        // olusan coinlere hareket verebilmek i�in bodytypelar�n� dynamic yapt�k.
                        coin.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                        // coinler olustuktan sonra rastgele saga sola s�cras�n
                        coin.GetComponent<Rigidbody2D>().velocity = patlamaMiktari * new Vector2(Random.Range(1, 2), transform.localScale.y + Random.Range(0, 2));
                    }

                    SesManager.instance.SesEfektiCikar(9);
                    Destroy(gameObject);


                }
                kacinciVurus++;
            }

            
        }
    }

}
