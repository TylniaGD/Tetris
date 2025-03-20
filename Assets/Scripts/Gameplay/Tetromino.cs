using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    Rigidbody2D rb;

    public bool isLanded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
}
