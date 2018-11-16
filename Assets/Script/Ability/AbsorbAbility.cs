using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbAbility : MonoBehaviour {

    // Use this for initialization
    public Collider2D collider;

    void Start () {
    }

    // Update is called once per frame
    void Update () {
        Vector2 offset = collider.offset;
        collider.offset = new Vector2(offset.x + 4.667f/100, offset.y);
    }
}
