using UnityEngine;

[System.Serializable]
public struct AbilityBPCollection
{
    public AbilityBP[] abilityBPCollection;
}

[System.Serializable]
public struct AbilityBP {
    //name of the ability
    public string name;

    //the basic damage parameter
    public float damage;

    //the damage delta that specify who damage will vary for each hit
    //high range means unstable damage
    public float damageDelta;

    //description of the ability
    public string description;
}
