using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{

    [SerializeField] float speed;
    public float Speed { get => speed; private set => speed = value; }
    [SerializeField] float maxHp;    
    public float MaxHp { get => maxHp; private set => maxHp = value; }

}
