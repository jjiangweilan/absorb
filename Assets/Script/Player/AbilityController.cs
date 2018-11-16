using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour {

    public delegate GameObject AbilityDel(MonoBehaviour x);
    public Ability ability;
    AbilityDel[] accessiableAbility = new AbilityDel[4];

    // Use this for initialization
    void Start () {
        ability = new Ability(this.transform);
        accessiableAbility[0] = ability.absorb;
    }

    // Update is called once per frame
    void Update () {
        if (ability.currSkill != null) { //freeze movement when using skill
            GetComponent<PlayerController>().isWalking = true;
            GetComponent<Animator>().SetBool("Walk", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
            return; 
        }

        if (Input.GetKeyDown(KeyCode.Space) && ability.currSkill == null && isOnGround() )
        {
            accessiableAbility[0](this);
        }
    }

    private bool isOnGround()
    {
        var boxCollider = GetComponent<BoxCollider2D>();

        var startLeft = boxCollider.bounds.min;
        var startRight = boxCollider.bounds.max;
        startRight.y -= boxCollider.bounds.size.y;

        var mask = LayerMask.GetMask("Ground");

        var hitLeft = Physics2D.Raycast(startLeft, Vector3.down, 0.03f, mask);
        var hitRight = Physics2D.Raycast(startRight, Vector3.down, 0.03f, mask);

        return hitLeft.collider != null || hitRight.collider != null;
    }
}
