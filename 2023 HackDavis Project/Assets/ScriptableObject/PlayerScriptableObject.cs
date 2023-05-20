using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/Player")]

public class PlayerScriptableObject : ScriptableObject
{
    [SerializeField] GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    [SerializeField] float damage;
    public float Damage { get => damage; private set => damage = value; }
    [SerializeField] float speed;
    public float Speed { get => speed; private set => speed = value; }
    [SerializeField] float maxHp;    
    public float MaxHp { get => maxHp; private set => maxHp = value; }

    [SerializeField] float coolDownDuration;    
    public float CoolDownDuration { get => coolDownDuration; private set => coolDownDuration = value; }


}
