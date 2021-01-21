using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Animator target;
    public AudioSource audioSource;
    public AudioClip pickUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("Player"))
        {
            audioSource.volume = 0.65f;
            audioSource.PlayOneShot(pickUpSound);
            Destroy(this.gameObject);
            Destroy(target.GetComponent<BoxCollider2D>());
            target.SetBool("Unlocked",true);
        }
    }

    
}
