using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iskeletHealthController : MonoBehaviour
{
    public int maxSaglik = 3;
    public int gecerliSaglik;
    public bool iskeletOldumu;

    private void Start()
    {
        gecerliSaglik = maxSaglik;
        iskeletOldumu = false;
    }

    public void CaniAzalt()
    {
        gecerliSaglik--;
        if (gecerliSaglik <=0)
        {
            gecerliSaglik = 0;
            iskeletOldumu = true;
            gameObject.GetComponent<Animator>().SetTrigger("canVerdi");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            // iskeleti oldurdugumuzde listeden cikarmaliyiz
            iskeletSpawnController.instance.ListeyiAzalt(this.gameObject);
            SesManager.instance.SesEfektiCikar(2); // iskelet olme 2de
            Destroy(gameObject, 1f);
        }
        else if (gecerliSaglik <= 3 && gecerliSaglik > 0)
        {
            SesManager.instance.SesEfektiCikar(1);
        }

    }

}
