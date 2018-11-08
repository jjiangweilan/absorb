using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float speed = 0.3f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpImpulse = 15;
    [SerializeField] private float jumpTestOffset = 0.01f;
    [SerializeField] private Animator animator;

    private bool isWalking;
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

        if (inputVert && isOnGround())
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

        if (isWalking == true && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            isWalking = false;
            StartCoroutine(waitAndSetWalkToFalse());
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            isWalking = true;
            animator.SetBool("Walk", isWalking);
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

    //check if the player is on the ground
    //cast two distanced ray from the left bottom and right bottom
    private bool isOnGround()
    {
        var boxCollider = GetComponent<BoxCollider2D>();

        var startLeft = boxCollider.bounds.min;
        var startRight = boxCollider.bounds.max;
        startRight.y -= boxCollider.bounds.size.y;

        var mask = LayerMask.GetMask("Ground");

        var hitLeft = Physics2D.Raycast(startLeft, Vector3.down, jumpTestOffset, mask);
        var hitRight = Physics2D.Raycast(startRight, Vector3.down, jumpTestOffset, mask);

        return hitLeft.collider != null || hitRight.collider != null;
    }

    void FixedUpdate(){
        //DebugDraw();
    }

    private IEnumerator waitAndSetWalkToFalse()
    {
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("Walk" , isWalking);
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
