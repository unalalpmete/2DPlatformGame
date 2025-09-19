using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AnaMenuController : MonoBehaviour
{
    [SerializeField] string digerSahne = "Level1";

    public void OyunaBasla()
    {
        Debug.Log("Oyna butonuna basýldý!");
        StartCoroutine(SahneyeGec());
    }

    IEnumerator SahneyeGec()
    {
        Debug.Log("Sahneye geçiþ coroutine baþladý...");
        yield return new WaitForSecondsRealtime(1f); //  Realtime bekleme
        Debug.Log("Sahne yükleniyor: " + digerSahne);

        Time.timeScale = 1f; //  sahneye geçmeden önce oyunu tekrar baþlat
        SceneManager.LoadScene(digerSahne);
    }

    public void OyundanCik()
    {
        Debug.Log("Oyundan çýkýþ butonuna basýldý!");
        Application.Quit();
    }
}
