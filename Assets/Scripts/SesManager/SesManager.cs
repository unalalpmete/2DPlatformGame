using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesManager : MonoBehaviour
{
    public static SesManager instance;

    [SerializeField]
    AudioSource[] sesEfektleri;

    private void Awake()
    {
        // bu nesne henuz olusturulmadiysa henuz sahnede yoksa
        if (instance == null)
        {
            instance = this;
            // bu nesne baska bir sahneye gectiginde burayi yok etme demek
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(gameObject);
        }
        
    }

    public void SesEfektiCikar(int hangiSes)
    {
        sesEfektleri[hangiSes].Stop();
        sesEfektleri[hangiSes].Play();
    }

    public void KarisikSesEfektiCikar(int hangiSes)
    {
        sesEfektleri[hangiSes].Stop();
        sesEfektleri[hangiSes].pitch = Random.Range(0.8f, 1.3f); // seslerin random farkli tonlarda cikmasini saglar
        sesEfektleri[hangiSes].Play();
    }

}
