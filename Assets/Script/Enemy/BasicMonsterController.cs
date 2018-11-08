using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonsterController : MonoBehaviour {
    // Use this for initialization
    [SerializeField] float platformTestLendth;
    [SerializeField] Collider2D collider_;
    private Rigidbody2D rb;
    void Start () {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(2,0);
    }

    // Update is called once per frame
    void Update () {
        flip();
        movement();
    }

    //flip the sprite basing on velocity
    void flip()
    {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;

        if (velocity.x != 0)
        {
            transform.localScale = new Vector2(-1 * Mathf.Sign(velocity.x), 1);
        }
    }

    //change the velocity basing on the result from isOnPlatform
    //and hasGroundInTheFront
    void movement()
    {
        if (!isOnPlatform() || hasGroundInTheFront())
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
    }

    //shot a ray to test if there is anything
    //that in front of the collider
    bool hasGroundInTheFront()
    {

        float direction = Mathf.Sign(rb.velocity.x);

        var start = collider_.bounds.center;

        start.y -= collider_.bounds.size.y / 2.0f;
        var hit = Physics2D.Raycast(start, new Vector3(direction, 0, 0), collider_.bounds.size.x);

        return hit.collider != null;
    }

    //shot a ranged ray basing on current moving direction
    //to test is collider is going to dropping from platform
    bool isOnPlatform()
    {

        float direction = Mathf.Sign(rb.velocity.x);

        var start = collider_.bounds.center;
        start.y = start.y - collider_.bounds.size.y / 2.0f - 0.01f;
        if (direction == 1)
        {
            start.x += collider_.bounds.size.x / 2.0f;
        }
        else
        {
            start.x -= collider_.bounds.size.x / 2.0f;
        }

        var rayTest = Physics2D.Raycast(start, Vector2.down, platformTestLendth, LayerMask.GetMask("Ground"));

        return rayTest.collider != null;
    }

    void FixedUpdate()
    {
    }

    public void attacked()
    {

    }
}
