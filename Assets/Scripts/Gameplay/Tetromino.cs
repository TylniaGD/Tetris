using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] string floorTag = "Floor";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(floorTag) && gameObject.CompareTag("Tetromino"))
        {
            rb.bodyType = RigidbodyType2D.Static;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
