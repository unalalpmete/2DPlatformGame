using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninjaYildiziControllerr : MonoBehaviour
{
    [Header("Ninja Yýldýzý Ayarlarý")]
    [SerializeField] GameObject ninjaYildiziPrefab;
    [SerializeField] Transform cikisNoktasi;
    [SerializeField] float firlatmaHizi = 10f;

    [Header("Yýldýz Envanter")]
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
        // Prefab oluþtur
        GameObject yildiz = Instantiate(
            ninjaYildiziPrefab,
            cikisNoktasi.position,
            cikisNoktasi.rotation
        );

        // Rigidbody hareketi
        Rigidbody2D rb = yildiz.GetComponent<Rigidbody2D>();
        float yon = Mathf.Sign(transform.localScale.x); // karakterin baktýðý yön
        rb.velocity = cikisNoktasi.right * yon * firlatmaHizi;
        rb.angularVelocity = -720f;

        // Envanteri güncelle
        mevcutYildiz--;
        UIManager.instance.YildizAdetGuncelle(mevcutYildiz);
    }

    // Pickup veya baþka yollarla yýldýz eklemek için
    public void YildizEkle(int adet)
    {
        mevcutYildiz += adet;
        UIManager.instance.YildizAdetGuncelle(mevcutYildiz);
    }

}
