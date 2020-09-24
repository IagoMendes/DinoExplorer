using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBullet : MonoBehaviour
{
    public float speed = 20.0f;

    void Update()
    {
        gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Crate")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
