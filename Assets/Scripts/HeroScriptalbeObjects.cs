using System.Collections.Generic;
using UnityEngine;

public enum CharacterCategory
{
    Unknown,
    Rock,
    Paper,
    Scissor
}

[CreateAssetMenu(fileName = "New Hero", menuName ="Game/Heros")]
public class HeroScriptalbeObjects : ScriptableObject
{
    public string hero_Name;
    public Sprite hero_Sprite;
    public CharacterCategory hero_Catagory;
    public List<CharacterCategory> CanDefeatEnemyList;
    public List<CharacterCategory> CanBeDefeatedByEnemyList;
}
