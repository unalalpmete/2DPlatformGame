using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuzakPlatformController : MonoBehaviour
{
    [SerializeField] GameObject okPrefab;
    [SerializeField] Transform[] okCikisNoktalari;
    [SerializeField] GameObject player;

    private bool okAtabilirmi = true; // At��a izin verilip verilmeyece�ini kontrol eder
    private float tekrarAtisSuresi = 2f; // 2 saniye bekleme s�resi

    

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

            // Sadece yatay y�n vekt�r� (X ekseni)
            // oklar sadece yatay duzlemde playera dogru f�rlat�ls�n
            float directionX = Mathf.Sign(player.transform.position.x - okCikisNoktalari[i].position.x);

            // Velocity uygula: X ekseninde sabit h�z, Y sabit
            rb.velocity = new Vector2(directionX * 7f, 0f);

            // Sprite��n y�n�n� velocity�ye g�re ayarla
            Vector3 localScale = ok.transform.localScale;
            localScale.x = Mathf.Abs(localScale.x) * directionX; // Pozitif veya negatif yap
            ok.transform.localScale = localScale;

            Destroy(ok, 3.5f);

        }

        // 2 saniye beklemeden sonra tekrar at��a izin ver
        yield return new WaitForSeconds(tekrarAtisSuresi);
        okAtabilirmi = true;

    }

}
