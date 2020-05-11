using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Enemy
{
    public string name;

    public enum Type
    {
    STRONG,
    LITTLESTRONG ,
    MEDIOCRE ,
    WEAK
    }

    public enum EncounterChance
    {
        COMMON,
        UNCOMMON,
        RARE,
        SUPERRARE
    }

    public Type EnemyType;
    public EncounterChance encounterchance;

    public string type;
    public float baseHP;
    public float currentHP;

    public float baseMP;
    public float currentMP;

    public int initiative;
    public int agility;

    public float baseAttack;
    public float currentAttack;
    public float baseDefense;
    public float currentDefense;
}
