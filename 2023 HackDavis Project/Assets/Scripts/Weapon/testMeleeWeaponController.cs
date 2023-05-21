using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMeleeWeaponController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject MeleeSwing = Instantiate(weaponData.Prefab);
        MeleeSwing.GetComponent<testMeleeWeaponBehaviour>().DirectionChecker(player.GetMouseDirection());
    }
}