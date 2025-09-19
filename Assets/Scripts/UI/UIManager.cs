using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // textmeshpro için ekledik
using UnityEngine.UI; // UI nesneleri için ekledik
using UnityEngine.SceneManagement; // baska sahnelere gecmek icin

public class UIManager : MonoBehaviour
{
    public static UIManager instance; // heryerden ulasacagýmýz için böyle yaptýk

    [SerializeField]
    Slider playerSlider;

    [SerializeField]
    TMP_Text coinTxt;

    [SerializeField]
    public Text yildizTxt;

    [SerializeField]
    Transform butonlarPanel;

    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    GameObject bitisPanel;
 
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TumBtnAlphaDusur();
        butonlarPanel.GetChild(0).GetComponent<CanvasGroup>().alpha = 1f;
        PlayerHareketController.instance.HerseyiKapatNormaliAc();

        pausePanel.SetActive(false);
        bitisPanel.SetActive(false);
    }

    public void PSlideriGuncelle(int gecerliDeger,int MaxDeger)
    {
        playerSlider.maxValue = MaxDeger;
        playerSlider.value = gecerliDeger;
    }

    public void CoinAdetGuncelle()
    {
        coinTxt.text = GameManager.instance.toplananCoinAdet.ToString();
    }

    public void YildizAdetGuncelle(int adet)
    {
        yildizTxt.text = adet.ToString();  // yildiz
    }

    void TumBtnAlphaDusur()
    {
        foreach (Transform btn in butonlarPanel)
        {
            btn.gameObject.GetComponent<CanvasGroup>().alpha = 0.25f;
            btn.GetComponent<Button>().interactable = true;
        }
    }

    public void NormalButonaBasildi()
    {
        TumBtnAlphaDusur();
        // basýlan(secilen) objeyi(buton) otomatik bulma
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>().alpha = 1f;

        PlayerHareketController.instance.HerseyiKapatNormaliAc();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    }

    public void KilicButonaBasildi()
    {
        TumBtnAlphaDusur();
        // basýlan(secilen) objeyi(buton) otomatik bulma
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>().alpha = 1f;

        PlayerHareketController.instance.HerseyiKapatKiliciAc();

        //butonlarin interactable ozelligini kapatip acarak butonlardaki sacma hatayi engelleyebiliriz
        //bu kod kisminin sonuna yazariz cunku interactable false yaptiktan sonraki kodlar calismayacaktir
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;

    }

    public void OkButonaBasildi()
    {
        TumBtnAlphaDusur();
        // basýlan(secilen) objeyi(buton) otomatik bulma
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>().alpha = 1f;

        PlayerHareketController.instance.HerseyiKapatOkuAc();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    }

    public void MizrakButonaBasildi()
    {
        TumBtnAlphaDusur();
        // basýlan(secilen) objeyi(buton) otomatik bulma
        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetComponent<CanvasGroup>().alpha = 1f;

        PlayerHareketController.instance.HerseyiKapatMizrakiAc();

        UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
    }

    public void PausePanelAcKapat()
    {
        // hierarchy icinde aktif degil ise demek
        if (!pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f; // pause edildiginde oyun dursun
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f; // oyun calissin
        }
    }

    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("AnaMenu");
    }

    public void BitisPaneliniAc()
    {
        bitisPanel.SetActive(true);
        Time.timeScale = 0f;    
    }

    public void TekrarOyna()
    {
        /*if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            // bulundugumuz indexteki sahneyi oynatacak
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }*/

        // bulundugumuz indexteki sahneyi oynatacak
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OyundanCik()
    {
        Application.Quit();
    }
}
