using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalinanEngelController : MonoBehaviour
{
    [SerializeField]
    float donmeHizi = 200f;

    float zAngle;

    [SerializeField]
    float minZAngle = -75f;

    [SerializeField]
    float maxZAngle = 75f;



    private void Start()
    {
        
    }

    private void Update()
    {
        float orta = (minZAngle + maxZAngle) / 2f; // "orta" => bu aralýðýn tam ortasý (salýnýmýn merkez noktasý)
        float genislik = (maxZAngle - minZAngle) / 2f; // "geniþlik" => salýnýmýn yarýsý (+/- ne kadar açýya gideceðini belirler)

        // daha gercekci olmasi icin
        // sin sonucu -1 +1 aci olarak verir. Ama biz -75 +75 aci ile donmesini istedigimiz icin
        // genislik ile carpip orta ile topladik  
        // bizim aci degerlerimizde(-75/+75) orta 0 ciktigi icin sonucu degistirmedi.
        float angle = Mathf.Sin(Time.time * donmeHizi) * genislik + orta;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);




        /*
        zAngle += Time.deltaTime * donmeHizi;
        transform.rotation = Quaternion.AngleAxis(zAngle, Vector3.forward);

        if (zAngle < minZAngle)
        {
            donmeHizi = Mathf.Abs(donmeHizi);
        }

        if (zAngle > maxZAngle)
        {
            donmeHizi = -Mathf.Abs(donmeHizi);
        }
        */
    }

    // karakterimize zarar verme islemi
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponent<EdgeCollider2D>().IsTouchingLayers(LayerMask.GetMask("PlayerLayer")))
        {
            if (other.CompareTag("Player") && other.GetComponent<PlayerHareketController>().playerCanverdimi == false)
            {
                other.GetComponent<PlayerHareketController>().GeriTepkiSayacBaslatma();
                other.GetComponent<PlayerHealthController>().CaniAzalt();
            }
        }
    }

}
