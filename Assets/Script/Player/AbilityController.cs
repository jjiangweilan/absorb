using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour {

    public delegate void AbilityDel();
    Ability ability;
    AbilityDel[] accessiableAbility = new AbilityDel[4];

    // Use this for initialization
    void Start () {
        ability = new Ability(GetComponent<BoxCollider2D>());
        accessiableAbility[0] = ability.absorb;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            accessiableAbility[0]();
        }
    }
}
