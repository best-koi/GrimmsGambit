using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class CharacterTemplate : MonoBehaviour
{ 
[SerializeField]
    protected int hp, maxHP;//Hit points stat 

[SerializeField]
protected string characterName;//The characters's name

[SerializeField]
protected TMP_Text healthText, nameText;//Text to indicate the character's health

[SerializeField]
protected Color characterColor;//A color that represents the character

[SerializeField]
protected Renderer renderer;//The character's renderer

[SerializeField]
protected GameObject self;

//A default Start() method 
protected virtual void Start()
{
    //Sets color to preset color
    renderer.material.color = characterColor;
}

    //Shows the default text above and below Character
    protected virtual void Update()
{
        if (this.hp <= 0)
            Destroy(self);

    healthText.text = $"{hp}/ {maxHP}";
    nameText.text = characterName;
    

}

//Sets the enemy hp
//Put in positive number to increase
//Put in negative number to decrease
public void AffectHP(int amount)
{
    hp += amount;
}

//sets HP to a fixed amount
public void SetHP(int amount)
{
    hp = amount;
}

//Returns the Character's Name
public string GetCharacterName()
{
    return characterName;
}

 //Returns the Character's Color (for Intent)
 public Color GetCharacterColor()
{
    return characterColor;
}

//Returns the Character's Current Health
public int GetHP()
{
    return hp;
}

//Returns the Character's Max Health
public int GetMaxHP()
{
    return maxHP;
}
}
