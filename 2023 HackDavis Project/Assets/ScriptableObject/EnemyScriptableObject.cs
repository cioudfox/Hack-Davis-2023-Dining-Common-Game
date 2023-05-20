using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    [SerializeField] float damage;
    public GameObject Damage { get => damage; private set => damage = value; }
    [SerializeField] float speed;
    public GameObject Speed { get => speed; private set => speed = value; }
    [SerializeField] float maxHp;    
    public GameObject MaxHp { get => maxHp; private set => maxHp = value; }
        
}
