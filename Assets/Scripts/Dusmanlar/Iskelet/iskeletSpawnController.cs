using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iskeletSpawnController : MonoBehaviour
{
    public static iskeletSpawnController instance;

    [SerializeField]
    GameObject iskeletPrefab;

    public List<GameObject> iskeletList = new List<GameObject>();

    [SerializeField]
    float spawnSuresi;

    float spawnSayac;
    int iskeletAdet;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (iskeletList.Count > 0)
        {
            iskeletList.Clear();
        }
        iskeletAdet = 0;

        StartCoroutine(iskeletSpawnRoutine());

    }

    IEnumerator iskeletSpawnRoutine()
    {
        yield return new WaitForSeconds(spawnSuresi);

        spawnSayac = 0;

        if (iskeletList.Count < 3)
        {
            // kac saniyede bir iskelet olusacagini belirleriz
            while (spawnSayac < spawnSuresi)
            {
                spawnSayac += Time.deltaTime;
                yield return null;
            }

            GameObject iskelet = Instantiate(iskeletPrefab, transform.position, Quaternion.identity);
            // olusan iskeleti bu scriptin bagli oldugu nesnenin cocugu yapiyoruz
            iskelet.transform.SetParent(transform);
            iskeletList.Add(iskelet);
            iskeletAdet++;

            if (spawnSayac >= spawnSuresi && iskeletAdet < 6)
            {
                StartCoroutine(iskeletSpawnRoutine());
            }

        }
    }

    public void ListeyiAzalt(GameObject iskelet)
    {
        iskeletList.Remove(iskelet);

        if (iskeletAdet >= 6)
        {
            StartCoroutine(OyundanCikRoutine());
        }
    }

    IEnumerator OyundanCikRoutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.OyunCikisEkraniniAc();
    }

}
