using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : MonoBehaviour
{

    [SerializeField] float speed;
    public GameObject Speed { get => speed; private set => speed = value; }
    [SerializeField] float maxHp;    
    public GameObject MaxHp { get => maxHp; private set => maxHp = value; }

}
