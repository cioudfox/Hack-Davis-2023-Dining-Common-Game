using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

    [SerializeField]
    float maxHp;
    public float MaxHp { get => maxHp; private set => maxHp = value; }

    [SerializeField]
    float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }    
    [SerializeField]
    float movingSpeed;
    public float MovingSpeed { get => movingSpeed; private set => movingSpeed = value; }

    [SerializeField]
    float criticalChance;
    public float CriticalChance { get => criticalChance; private set => criticalChance = value; }
}

