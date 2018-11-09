using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability{
    public Ability(Collider2D pc)
    {
        abilityBP = new Dictionary<string, AbilityBP>();
        playerCollider = pc;

        var jsonText = Resources.Load<TextAsset>("AbilityBPCollection");
        var loadedAbilityBPCollection = JsonUtility.FromJson<AbilityBPCollection>(jsonText.text);

        for (int i = 0; i < loadedAbilityBPCollection.abilityBPCollection.Length;i++)
        {
            abilityBP.Add(loadedAbilityBPCollection.abilityBPCollection[i].name, loadedAbilityBPCollection.abilityBPCollection[i]);
        }
    }
    //this variable will be initialized by Player Controller
    //once the player game obejct is initialized
    private Collider2D playerCollider;

    //dictionary to store the basic information of each ability
    //<name of the ability, ability info>
    private Dictionary<string, AbilityBP> abilityBP;

    //ability function collections
    public void absorb()
    {
        // GameObject absorbAbi = GameObject("absorb");
        // Vector3 trans = playerCollider.transform;
        // trans.x += trans.x / 2.0;
        // //TO_DO
    }
}
