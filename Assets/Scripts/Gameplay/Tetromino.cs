using UnityEngine;

public class Tetromino : MonoBehaviour
{
    Rigidbody2D rb;

    [Space]

    public bool isLanded = false;
    // public float raycastDistance = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        #region Testing the new collision detector - he hits himself
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance);

        //Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);

        //if (!isLanded)
        //{
        //    if (hit.collider != null)
        //    {
        //        if ((hit.collider.CompareTag("Floor") || hit.collider.CompareTag("Tetromino"))
        //            && hit.collider.gameObject != gameObject)
        //        {
        //            rb.bodyType = RigidbodyType2D.Static;
        //            isLanded = true;
        //        }
        //    }
        //}
        #endregion
    }

    void OnCollisionEnter2D(Collision2D collision) // It works but the blocks affect each other not only the lower part of the block
    {
        if (!isLanded)
        {
            if ((collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Tetromino"))
                && gameObject.CompareTag("Tetromino"))
            {
                rb.bodyType = RigidbodyType2D.Static;
                isLanded = true;
            }
        }
    }
}
