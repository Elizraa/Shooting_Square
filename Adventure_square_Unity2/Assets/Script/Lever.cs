using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject lever;
    public float leverOnChange;
    private Vector3 leverStart;
    public GameObject target;
    private Vector3 targetStart, temp;
    public Vector2 positionToMoveTo;
    private bool leverOn;

    float timeElapsed;
    public float lerpDuration;

    public AudioSource audioSource;
    public AudioClip leverOnSound;
    // Start is called before the first frame update
    void Start()
    {
        leverStart = lever.transform.localPosition;
        targetStart = target.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (leverOn)
        {
            StartCoroutine(DoPosition(positionToMoveTo, lerpDuration));
        }
        else
        {
            StartCoroutine(RedoPosition(targetStart, lerpDuration));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lever.transform.localPosition = leverStart;
            leverOn = false;
         }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            temp = lever.transform.localPosition;
            temp.y -= leverOnChange;
            lever.transform.localPosition = temp;
            audioSource.PlayOneShot(leverOnSound);
            leverOn = true;
        }
    }

    IEnumerator DoPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = target.transform.localPosition;

        while (time < duration)
        {
            if (!leverOn) yield break;
            temp = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            target.transform.localPosition = temp;
            yield return null;
        }
        target.transform.localPosition = targetPosition;
    }
    IEnumerator RedoPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = target.transform.localPosition;

        while (time < duration)
        {
        if (leverOn) yield break;
            temp = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            target.transform.localPosition = temp;
            yield return null;
        }
        target.transform.localPosition = targetPosition;
    }
}

