using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    Rigidbody2D rb;

    [Space]

    public bool isLanded = false;
    public bool endGame = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (endGame) return;

        if (!isLanded)
        {
            if ((collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Tetromino"))
                && gameObject.CompareTag("Tetromino"))
            {
                rb.bodyType = RigidbodyType2D.Static;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                isLanded = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (endGame) return;

        if (gameObject.CompareTag("Tetromino"))
        {
            rb.bodyType = RigidbodyType2D.Static;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            endGame = true;
        }
    }
}
