using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability{
    public Ability(Transform pf)
    {
        abilityBP = new Dictionary<string, AbilityBP>();
        parentTransform = pf;

        var jsonText = Resources.Load<TextAsset>("AbilityBPCollection");
        var loadedAbilityBPCollection = JsonUtility.FromJson<AbilityBPCollection>(jsonText.text);

        for (int i = 0; i < loadedAbilityBPCollection.abilityBPCollection.Length;i++)
        {
            abilityBP.Add(loadedAbilityBPCollection.abilityBPCollection[i].name, loadedAbilityBPCollection.abilityBPCollection[i]);
        }

        currSkill = null;
    }

    //current skill that is used by player
    public GameObject currSkill;

    //this variable will be initialized by Player Controller
    //once the player game obejct is initialized
    private Transform parentTransform;

    //dictionary to store the basic information of each ability
    //<name of the ability, ability info>
    private Dictionary<string, AbilityBP> abilityBP;

    //ability function collections

    public GameObject absorb(MonoBehaviour caller)
    {
        Debug.Log("absorb skill called");
        //load prefab from resource
        var absorbSkillPrefab = Resources.Load("Prefab/AbsorbSkill");

        //instantiate
        var absorbSkill = Object.Instantiate(absorbSkillPrefab) as GameObject;
        currSkill = absorbSkill;

        //animation
        var ani = absorbSkill.GetComponent<Animator>();
		ani.Play("AbsorbSkill");
		caller.StartCoroutine(killSkill(ani.runtimeAnimatorController.animationClips[0].length));

        //skill transformation
        var absorbBP = abilityBP["absorb"];
        absorbSkill.transform.SetParent(parentTransform);
		absorbSkill.transform.localPosition = new Vector3(absorbBP.localPosition[0], absorbBP.localPosition[1], absorbBP.localPosition[2]);
		absorbSkill.transform.localScale = new Vector3(absorbBP.localScale[0], absorbBP.localScale[1], absorbBP.localScale[2]);

        //ps: collider and trigger effect on enemy is handled by the skill itself
        return absorbSkill;
    }

    private IEnumerator killSkill(float time)
    {
        yield return new WaitForSeconds(time);
        Object.Destroy(currSkill);
    }
}

