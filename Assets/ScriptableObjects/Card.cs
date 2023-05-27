using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card",menuName = "Cards/Minion")]
public class Card : ScriptableObject
{
    public new string name;   //By default any object already has a variable called name. So we use new keyword
    public string description;

    public Sprite artwork;

    public int manaCost;
    public int attack;
    public int health;
}
