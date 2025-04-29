using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private bool isCollected = false;
    private float rotateSpeed = 90f; // degrees per second

    private void Update()
    {
        // Constant rotation
        if (!isCollected)
        {
            transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected && collision.gameObject.GetComponent<PlayerController>() != null)
        {
            isCollected = true;
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.PickUpKey();
            StartCoroutine(CollectAnimation());
        }
    }

    private IEnumerator CollectAnimation()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 startPos = transform.position;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = startPos + new Vector3(0f, t * 1f, 0f); // move up
            sr.color = new Color(1f, 1f, 1f, 1f - t); // fade out
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
