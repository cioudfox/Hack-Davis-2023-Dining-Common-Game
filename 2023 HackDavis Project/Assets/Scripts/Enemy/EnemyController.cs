using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform tragetDestination;
    GameObject targetGameObject;
    
    public EnemyScriptableObject enemyData;

    Rigidbody2D rgdbd2d;
    // Start is called before the first frame update
    private void Awake()
    {
        rgdbd2d = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        tragetDestination = target.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = (tragetDestination.position - transform.position).normalized;
        rgdbd2d.velocity = direction * enemyData.Speed;       
    }

    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     if (collision.gameObject == targetGameObject)
    //     {
    //         Attack();
    //     }
    // }

    // private void Attack()
    // {
    //     Debug.Log("Attack!");
    //     // need to arrage with the hp system.
    // }
}
