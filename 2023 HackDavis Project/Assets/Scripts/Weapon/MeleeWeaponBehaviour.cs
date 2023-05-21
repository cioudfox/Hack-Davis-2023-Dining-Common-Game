using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;

    //Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    // protected int currentPierce;
    protected PlayerController player;
    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        player = FindObjectOfType<PlayerController>();
        // currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
        
        // float dirx = direction.x;
        // float diry = direction.y;
        // Vector3 scale = transform.localScale;  //may be used to flip sprites
        // transform.localScale = scale;
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z = (float)(Atan2(direction.y, direction.x) * 180 / PI);
        // Debug.Log(transform.position);
        transform.position = direction + player.transform.position;
        transform.rotation = Quaternion.Euler(rotation);
        // Debug.Log(transform.position);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStat enemy = col.GetComponent<EnemyStat>();
            enemy.TakeDamage(currentDamage);
        }
    }
}