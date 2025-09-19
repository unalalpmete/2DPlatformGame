using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DikenController : MonoBehaviour
{
    private Coroutine damageCoroutine;  // olusan Coroutineyi saklay�p sonradan durdurmak icin bu degiskeni olusturduk

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �lk �arp��mada hemen hasar ver
            other.GetComponent<PlayerHealthController>().CaniAzalt();
            Debug.Log("Player ilk temasta 1 hasar ald�!");

            // Sonra d�ng�y� ba�lat (her 1 saniyede bir hasar)
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(DamageOverTime(other)); // burda da coroutinemizi sakliyoruz ve baslat�yoruz.
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Player ��k�nca coroutine durur
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
                Debug.Log("Player dikenin �st�nden ��kt�, hasar d�ng�s� durdu.");
            }
        }
    }

    private IEnumerator DamageOverTime(Collider2D player)
    {
        while (true) // Player dikenin �st�nde kald��� s�rece
        {
            yield return new WaitForSeconds(1f); // 1 saniye bekle
            player.GetComponent<PlayerHealthController>().CaniAzalt();
            Debug.Log("Player dikenin �st�nde kald�, 1 hasar daha ald�!");
        }
    }
}
