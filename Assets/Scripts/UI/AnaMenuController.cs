using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AnaMenuController : MonoBehaviour
{
    [SerializeField] string digerSahne = "Level1";

    public void OyunaBasla()
    {
        Debug.Log("Oyna butonuna bas�ld�!");
        StartCoroutine(SahneyeGec());
    }

    IEnumerator SahneyeGec()
    {
        Debug.Log("Sahneye ge�i� coroutine ba�lad�...");
        yield return new WaitForSecondsRealtime(1f); //  Realtime bekleme
        Debug.Log("Sahne y�kleniyor: " + digerSahne);

        Time.timeScale = 1f; //  sahneye ge�meden �nce oyunu tekrar ba�lat
        SceneManager.LoadScene(digerSahne);
    }

    public void OyundanCik()
    {
        Debug.Log("Oyundan ��k�� butonuna bas�ld�!");
        Application.Quit();
    }
}
