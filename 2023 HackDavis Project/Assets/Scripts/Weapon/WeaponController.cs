using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Generic Project Name;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    public float weaponCooldown;
    public float currentCooldownTimer;
    //public type?;

    protected PlayerController player;
    
    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerController>();
        weaponCooldown = weaponData.CooldownDuration;
        currentCooldownTimer = weaponData.CooldownDuration;
    }   
    
    protected virtual void Update()
    {
        currentCooldownTimer -=  Time.deltaTime;
        if(currentCooldownTimer <= 0f)
        {
            // Debug.Log(currentCooldown);
            Attack();
        }
    }
    protected virtual void Attack()
    {
        // Debug.Log("fire2");
        // Debug.Log(currentCooldown);
        currentCooldownTimer = weaponCooldown;
    }
}