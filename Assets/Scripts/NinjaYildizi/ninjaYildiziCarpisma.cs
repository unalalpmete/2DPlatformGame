using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninjaYildiziCarpisma : MonoBehaviour
{
    [SerializeField] GameObject parlamaEffect;

    [SerializeField] GameObject iksirPrefab;
    [SerializeField] GameObject kanPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("iskelet"))
        {
                Debug.Log("Çarpýþma tetiklendi: " + other.name + " Tag: " + other.tag);
                SesManager.instance.SesEfektiCikar(1);
                Instantiate(parlamaEffect, other.transform.position, Quaternion.identity);
                Instantiate(kanPrefab, other.transform.position, Quaternion.identity);
                //other.GetComponent<iskeletHealthController>().CaniAzalt();

                iskeletHealthController iskelet = other.GetComponent<iskeletHealthController>();
            if (iskelet != null)
            {
                //iskelet.CaniAzalt();
                iskelet.gecerliSaglik--;
                if (iskelet.gecerliSaglik <= 0)
                {
                    iskelet.gecerliSaglik = 0;
                    iskelet.iskeletOldumu = true;
                    gameObject.GetComponent<Animator>().SetTrigger("canVerdi");
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    // iskeleti oldurdugumuzde listeden cikarmaliyiz
                    iskeletSpawnController.instance.ListeyiAzalt(this.gameObject);
                    SesManager.instance.SesEfektiCikar(2); // iskelet olme 2de
                    Destroy(gameObject, 1f);
                }
                else if (iskelet.gecerliSaglik <= 3 && iskelet.gecerliSaglik > 0)
                {
                    SesManager.instance.SesEfektiCikar(1);
                }
            }

                // Destroy(gameObject);
        }
        

            

        if (gameObject.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("DusmanLayer")))
        {
            

            if (other.CompareTag("Orumcek"))
            {
                Debug.Log("Çarpýþtý: " + other.name);
                SesManager.instance.SesEfektiCikar(1);
                Instantiate(parlamaEffect, other.transform.position, Quaternion.identity);
                //other.gameObject.GetComponent<OrumcekController>().GeriTepki(); // fonksiyon yanlýs cagýrýldý 

                OrumcekController orumcek = other.GetComponent<OrumcekController>();
                if (orumcek != null)
                {
                    // Hemen can azalt ve slider güncelle
                    orumcek.gecerliSaglik--;
                    if (orumcek.gecerliSaglik <= 0)
                    {
                        Instantiate(iksirPrefab, other.transform.position, Quaternion.identity);
                        other.gameObject.SetActive(false);
                    }
                    orumcek.SliderGuncelle();

                    // Sonra hareket coroutine baþlat
                    //orumcek.GeriTepki();
                    //StartCoroutine(orumcek.GeriTepki());
                }
                orumcek.GeriTepki();

                Destroy(gameObject);
            }

            if (other.CompareTag("Boa"))
            {
                Debug.Log("Çarpýþtý: " + other.name);
                SesManager.instance.SesEfektiCikar(1);
                Instantiate(kanPrefab, other.transform.position, Quaternion.identity);
                Instantiate(parlamaEffect, other.transform.position, Quaternion.identity);

                BoaController boa = other.GetComponent<BoaController>();
                if (boa != null)
                {
                    boa.BoaOldu();
                    //Destroy(gameObject);
                }

                //BoaController.instance.BoaOldu();
                //Destroy(gameObject);
            }
        }

    }

    void Start()
    {
        // Belirli süre sonunda yok olsun
        Destroy(gameObject, 3f);
    }

}
