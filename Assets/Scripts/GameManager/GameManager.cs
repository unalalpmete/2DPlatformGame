using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int toplananCoinAdet;

    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        toplananCoinAdet = 0;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.instance.PausePanelAcKapat();
        }
    }

    public void OyunCikisEkraniniAc()
    {
        UIManager.instance.BitisPaneliniAc();
    }

   

}
