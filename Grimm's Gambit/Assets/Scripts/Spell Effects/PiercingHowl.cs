using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PiercingHowl : SpellEffect
{

    public PiercingHowl()
    {
        _spellName = "Piercing Howl";
        _spellDescription = "All enemies will target the Hound this turn. Gain 7 Block per enemy target changed.";
    }

    public override void DoSpellEffect(Minion caster, Minion target)
    {
        //The following assumes that only two parties exist and characters have properly labeled owners.
        //This is used to register both the friendly and enemy parties for aoe usage
        List<Transform> friendlyParty, hostileParty;
        UnitParty[] parties = GameObject.FindObjectsOfType<UnitParty>();
        List<Transform> currentParty = parties[0].GetAll();
        if (currentParty[0].GetComponent<Minion>().ownerPlayer == true)
        {
            friendlyParty = currentParty;
            hostileParty = parties[1].GetAll();
        }
        else
        {
            hostileParty = currentParty;
            friendlyParty = parties[1].GetAll();
        }

        CharacterTemplate newTarget = friendlyParty[0].GetComponent<CharacterTemplate>(); //Default value set for compilation reasons (this card isn't called unless the hound is alive though due to game logic)
        foreach (Transform member in friendlyParty) //Determines which member is the hound, and grabs a reference to his CharacterTemplate and Minion scripts
        {
            caster = member.GetComponent<Minion>();
            target = member.GetComponent<Minion>();
            newTarget = member.GetComponent<CharacterTemplate>();
            if (newTarget.GetCharacterName() == "The Hound")
            {
                break; //Ends this for loop if the character name matches the hound
            }
        }
        int targetsChanged = 0;
        foreach (Transform member in hostileParty) //Sets new targets when necessary
        {
            EnemyTemplate currentEnemy = member.GetComponent<EnemyTemplate>();
            if(currentEnemy.SetAttackTarget(newTarget))
            {
                targetsChanged++; //Increments the amount of targets changed based upon successful function runtime
            }
        }
        //Application of block to the hound
        caster.AddAffix(Affix.Block, 7*targetsChanged); //Gives the hound 7 times the amount of targets changed of block
    }
}
