using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninjaYildiziControllerr : MonoBehaviour
{
    [Header("Ninja Y�ld�z� Ayarlar�")]
    [SerializeField] GameObject ninjaYildiziPrefab;
    [SerializeField] Transform cikisNoktasi;
    [SerializeField] float firlatmaHizi = 10f;

    [Header("Y�ld�z Envanter")]
    [SerializeField] int baslangicYildiz = 10;
    int mevcutYildiz;

    void Start()
    {
        mevcutYildiz = baslangicYildiz;
        UIManager.instance.YildizAdetGuncelle(mevcutYildiz);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && mevcutYildiz > 0)
        {
            Firlat();
        }
    }

    void Firlat()
    {
        // Prefab olu�tur
        GameObject yildiz = Instantiate(
            ninjaYildiziPrefab,
            cikisNoktasi.position,
            cikisNoktasi.rotation
        );

        // Rigidbody hareketi
        Rigidbody2D rb = yildiz.GetComponent<Rigidbody2D>();
        float yon = Mathf.Sign(transform.localScale.x); // karakterin bakt��� y�n
        rb.velocity = cikisNoktasi.right * yon * firlatmaHizi;
        rb.angularVelocity = -720f;

        // Envanteri g�ncelle
        mevcutYildiz--;
        UIManager.instance.YildizAdetGuncelle(mevcutYildiz);
    }

    // Pickup veya ba�ka yollarla y�ld�z eklemek i�in
    public void YildizEkle(int adet)
    {
        mevcutYildiz += adet;
        UIManager.instance.YildizAdetGuncelle(mevcutYildiz);
    }

}
