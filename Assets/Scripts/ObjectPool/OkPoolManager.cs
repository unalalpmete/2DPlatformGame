using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkPoolManager : MonoBehaviour
{
    // her yerden ulasacagimiz icin boyle yaptik
    public static OkPoolManager instance;

    [SerializeField]
    GameObject okPrefab;

    GameObject okObje;

    // havuzdaki oklari bu listede olusturcaz
    List<GameObject> okPool = new List<GameObject>();

    private void Awake()
    {
        instance = this;

        OklariOlustur();
    }

    void OklariOlustur()
    {
        for (int i = 0; i < 10; i++)
        {
            okObje = Instantiate(okPrefab);
            okObje.SetActive(false);
            // olusan oklarin parentini OkPool nesnesi yaptik ki olusan oklar bu nesnenin icinde olussun
            okObje.transform.parent = transform.parent;

            okPool.Add(okObje);
        }
    }

    public void OkuFirlat(Transform okCikisNoktasi, Transform parent)
    {
        for (int i = 0; i < okPool.Count; i++)
        {
            // havuzun icersindeki elemanin aktifligi kapaliysa 
            if (!okPool[i].gameObject.activeInHierarchy)
            {
                okPool[i].SetActive(true);
                okPool[i].gameObject.transform.position = okCikisNoktasi.position;

                if (parent.transform.localScale.x > 0)
                {
                    okPool[i].GetComponent<Rigidbody2D>().velocity = transform.right * transform.localScale.x * 15f;
                }
                else
                {
                    okPool[i].GetComponent<Rigidbody2D>().velocity = -transform.right * transform.localScale.x * 15f;
                }
                

                return;
            }
            else
            {
                YenidenOkOlustur(okCikisNoktasi, parent);
            }
        }
    }

    void YenidenOkOlustur(Transform okCikisNoktasi, Transform parent)
    {
        okObje = Instantiate(okPrefab);
        okObje.transform.parent = transform.parent;
        okPool.Add(okObje);
    }

}
