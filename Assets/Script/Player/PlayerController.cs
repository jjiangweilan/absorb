using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float speed = 0.3f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpImpulse = 15;
    [SerializeField] private float jumpTestOffset = 0.01f;
    [SerializeField] private Animator animator;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update () {
        UpdateMovement();
        UpdateAttack();
    }

    private void UpdateMovement()
    {

        bool inputVert = Input.GetKey(KeyCode.W);
        float horSpeed = speed * Input.GetAxis("Horizontal");

        if (inputVert && IsOnGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpImpulse), ForceMode2D.Impulse);

        }

        rb.velocity = new Vector2(horSpeed, rb.velocity.y);

        //flip
        if (!Mathf.Approximately(0, horSpeed))
        {
            Vector3 theScale = transform.localScale;
            theScale.x = Mathf.Sign(horSpeed) * Mathf.Abs(theScale.x);
            transform.localScale = theScale;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void UpdateAttack()
    {
        if (Input.GetAxis("Fire1") != 0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private bool IsOnGround()
    {
        var boxCollider = GetComponent<BoxCollider2D>();

        var start = boxCollider.bounds.min;
        start.y -= jumpTestOffset;

        var end = boxCollider.bounds.max;
        end.y = end.y - boxCollider.bounds.size.y - 2 * jumpTestOffset;

        var cast = Physics2D.Linecast(start, end, LayerMask.GetMask("Ground"));

        return cast.collider != null;
    }

    void FixedUpdate(){
        //DebugDraw();
    }

    private void DebugDraw(){
        var boxCollider = GetComponent<BoxCollider2D>();

        var start = boxCollider.bounds.min;
        start.y -= jumpTestOffset;

        var end = boxCollider.bounds.max;
        end.y = end.y - boxCollider.bounds.size.y - jumpTestOffset;

        Debug.DrawLine(start, end);
    }

}
