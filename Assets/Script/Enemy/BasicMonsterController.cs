using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonsterController : MonoBehaviour {
    // Use this for initialization
    [SerializeField] float platformTestLendth;
    [SerializeField] Collider2D collider_;
    void Start () {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(3,0);
    }

    // Update is called once per frame
    void Update () {
    }

    bool isOnPlatform()
    {
        float direction = Mathf.Sign(transform.localScale.x);

        var start = collider_.bounds.center;
        start.y -= collider_.bounds.size.y / 2.0f - 0.01f;
        if (direction == 1)
        {
            start.x += collider_.bounds.center.x / 2.0f;
        }
        else
        {
            start.x -= collider_.bounds.center.x / 2.0f;
        }

        var rayTest = Physics2D.Raycast(start, Vector2.down, platformTestLendth);

        return rayTest.collider != null;
    }

    void FixedUpdate()
    {
    }

}
