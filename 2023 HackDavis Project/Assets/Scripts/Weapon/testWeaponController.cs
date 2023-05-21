using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootingDirection
{
    Forward,
    Mouse
}

public class testWeaponController : WeaponController
{
    private ShootingDirection shootDir;

    public void SetShootDir(ShootingDirection shootDir)
    {
        this.shootDir = shootDir;
        Debug.Log(this.shootDir);

    }

    protected override void Start()
    {
        base.Start();
        shootDir = ShootingDirection.Forward;
    }
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnTestWeapon = Instantiate(weaponData.Prefab);
        spawnTestWeapon.transform.position = transform.position;
        if (this.shootDir == ShootingDirection.Forward)
        {
            spawnTestWeapon.GetComponent<testWeaponBehaviour>().DirectionChecker(player.GetLastMovedVector());
        }
        else if (this.shootDir == ShootingDirection.Mouse)
        {
            spawnTestWeapon.GetComponent<testWeaponBehaviour>().DirectionChecker(player.GetMouseDirection());
        }
    }
}