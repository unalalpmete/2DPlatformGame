using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsDusmeNoktasiController : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D rbObstacle;

    public float dusmeHizi = 4f;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // düsmenoktasina degdigimizde ebeveyni null yaptik
            gameObject.transform.SetParent(null);
            rbObstacle.bodyType = RigidbodyType2D.Dynamic;
            rbObstacle.velocity = Vector2.down * dusmeHizi;
        }
    }

}
