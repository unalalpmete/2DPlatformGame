using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuzakPlatformController : MonoBehaviour
{
    [SerializeField] GameObject okPrefab;
    [SerializeField] Transform[] okCikisNoktalari;
    [SerializeField] GameObject player;

    private bool okAtabilirmi = true; // Atýþa izin verilip verilmeyeceðini kontrol eder
    private float tekrarAtisSuresi = 2f; // 2 saniye bekleme süresi

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && okAtabilirmi == true)
        {
            StartCoroutine(TuzakiBaslat());
        }
    }

    private IEnumerator TuzakiBaslat()
    {
        okAtabilirmi = false;

        for (int i = 0; i < okCikisNoktalari.Length; i++)
        {
            
            // Oku instantiate et
            GameObject ok = Instantiate(okPrefab, okCikisNoktalari[i].position, Quaternion.identity);
            Rigidbody2D rb = ok.GetComponent<Rigidbody2D>();

            // Sadece yatay yön vektörü (X ekseni)
            // oklar sadece yatay duzlemde playera dogru fýrlatýlsýn
            float directionX = Mathf.Sign(player.transform.position.x - okCikisNoktalari[i].position.x);

            // Velocity uygula: X ekseninde sabit hýz, Y sabit
            rb.velocity = new Vector2(directionX * 7f, 0f);

            // Sprite’ýn yönünü velocity’ye göre ayarla
            Vector3 localScale = ok.transform.localScale;
            localScale.x = Mathf.Abs(localScale.x) * directionX; // Pozitif veya negatif yap
            ok.transform.localScale = localScale;

            Destroy(ok, 3.5f);

        }

        // 2 saniye beklemeden sonra tekrar atýþa izin ver
        yield return new WaitForSeconds(tekrarAtisSuresi);
        okAtabilirmi = true;

    }

}
