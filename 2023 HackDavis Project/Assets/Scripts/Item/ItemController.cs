using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour   
{
    [SerializeField] public float itemDropRange;
    [SerializeField] public float itemDropSpeed;
    [SerializeField] public float itemLifetime;
    [SerializeField] public float itemCollectSpeed;
    [SerializeField] public float itemCollectRange;

    GameObject item;
    Vector3 targetDropPosition;
    bool doneDropping;
    GameObject player;

    void Start()
    {
        this.item = this.gameObject;

        var Xrandom = Random.Range(-itemDropRange, itemDropRange);
        var Yrandom = Random.Range(-itemDropRange, itemDropRange);
        this.targetDropPosition = this.item.transform.position + new Vector3(Xrandom, Yrandom, 0.0f);

        this.doneDropping = false;

        this.player = GameObject.Find("Player");

    }

    // private void OnTriggerEnter2D(Collider other)
    // {

    // }

    // // Update is called once per frame.
    void Update()
    {
        // Debug.Log(this.player.transform.position);
        // Debug.Log(this.item.transform.position);

        if (!this.doneDropping)
        {
            if (this.targetDropPosition != this.item.transform.position)
            {
                this.item.transform.position = Vector3.MoveTowards(this.item.transform.position, this.targetDropPosition, this.itemDropSpeed * Time.deltaTime);
            }
            else
            {
                this.doneDropping = true;
            }
        }
        else
        {
            // Item existence time
            this.itemLifetime -= Time.deltaTime;
            if (this.itemLifetime < 0.0f)
            {
                Destroy(this.gameObject);
            }

            // Item picked up by player
            if (Vector3.Distance(this.item.transform.position, this.player.transform.position) < itemCollectRange)
            {
                this.item.transform.position = Vector3.MoveTowards(this.item.transform.position, this.player.transform.position, this.itemCollectSpeed * Time.deltaTime);
            }
        }
    }
}
