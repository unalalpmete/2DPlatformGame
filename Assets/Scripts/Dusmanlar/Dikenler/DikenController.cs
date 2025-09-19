using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DikenController : MonoBehaviour
{
    private Coroutine damageCoroutine;  // olusan Coroutineyi saklayýp sonradan durdurmak icin bu degiskeni olusturduk

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ýlk çarpýþmada hemen hasar ver
            other.GetComponent<PlayerHealthController>().CaniAzalt();
            Debug.Log("Player ilk temasta 1 hasar aldý!");

            // Sonra döngüyü baþlat (her 1 saniyede bir hasar)
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamageOverTime(other)); // burda da coroutinemizi sakliyoruz ve baslatýyoruz.
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player çýkýnca coroutine durur
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
                Debug.Log("Player dikenin üstünden çýktý, hasar döngüsü durdu.");
            }
        }
    }

    private IEnumerator DamageOverTime(Collider2D player)
    {
        while (true) // Player dikenin üstünde kaldýðý sürece
        {
            yield return new WaitForSeconds(1f); // 1 saniye bekle
            player.GetComponent<PlayerHealthController>().CaniAzalt();
            Debug.Log("Player dikenin üstünde kaldý, 1 hasar daha aldý!");
        }
    }
}
