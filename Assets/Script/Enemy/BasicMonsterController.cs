﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonsterController : MonoBehaviour {
    // Use this for initialization
    [SerializeField] float platformTestLendth;
    [SerializeField] Collider2D collider_;
    private Rigidbody2D rb;
    bool absorbing = false;
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

        if (hit.collider == null) 
        {
            return false;
        }

        if (hit.collider.gameObject.tag == "Ground")
        {
            return true;
        }

        return false;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Absorb")
        {
            Absorbed(other);
        }
    }

    void Absorbed(Collider2D absorb)
    {
        if (absorbing == false)
        {
            rb.velocity = Vector2.zero;

            //absorb trap
            var absorbTrapPrefab = Resources.Load("Prefab/AbsorbTrap");
            var absorbTrap = Instantiate(absorbTrapPrefab) as GameObject;
            
            absorbTrap.transform.position = this.transform.position;
            absorbing = true;

            //absorb catch
            var absorbCatchPrefab = Resources.Load("Prefab/AbsorbCatch");
            var absorbCatch = Instantiate(absorbCatchPrefab) as GameObject;

            absorbCatch.transform.SetParent(this.transform);
            absorbCatch.transform.localPosition = new Vector2(0, 0);

            //freeze absorb skill's animation
            absorb.gameObject.GetComponent<Animator>().speed = 0;
        }
        
    }
}
