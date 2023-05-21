using System.Collections;
using System.Collections.Generic;
using Player.Command;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IPlayerCommand leftMouse;
    private IPlayerCommand rightMouse;
    private IPlayerCommand Ibutton;
    [SerializeField] public InventoryController inventoryController;

    public Animator animator;
    private Rigidbody2D body;
    private Vector2 moveDir;
    public Vector2 GetMoveDir(){return moveDir;}
    //To preserve states
    private float lastHorizontalVector;
    private float lastVerticalVector;
    private Vector2 lastMovedVector;

    private Inventory inventory;
    private float itemUsageCooldown = 1.0f;
    private float itemCooldownTimer = 0.0f;

    public CharacterScriptableObject characterData;

    PlayerStat playerStat;

    public Vector2 GetLastMovedVector() 
    {
        return lastMovedVector;
    }

    public Vector2 GetMouseDirection()
    {
        Vector3 playerPosition = this.gameObject.transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = playerPosition.z;
        
        Vector2 direction = new Vector2(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y);
        direction.Normalize();
        return direction;
    }

    void Awake()
    {
        playerStat = FindObjectOfType<PlayerStat>();
    }
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f); //If we don't do this and game starts up and don't move, the projectile weapon will have no momentum

        this.leftMouse = ScriptableObject.CreateInstance<ShootingTowardsMouseCommand>();
        this.rightMouse = ScriptableObject.CreateInstance<ShootingForwardCommand>();
        this.Ibutton = ScriptableObject.CreateInstance<toggleInventoryCommand>();

        this.inventory = new Inventory(UseItem);
        inventoryController.SetInventory(this.inventory);
    }

    void Update()
    {
        InputManagement();
        Move();
        itemCooldownTimer -= Time.deltaTime;
    }

    void InputManagement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized; //Use normalize as moving in diagonal generates a value > 1 so cap it to 1

        if (moveDir.x != 0 || moveDir.y != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if(moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);    //Last moved X
        }

        if(moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);      //Last moved Y
        }

        if(moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);    //While moving
        }

        if (GameObject.FindGameObjectWithTag("Inventory") == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Left mouse button was clicked
                this.leftMouse.Execute(this.gameObject);
            }
            if (Input.GetMouseButtonDown(1))
            {
                // Right mouse button was clicked
                this.rightMouse.Execute(this.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            // The "I" key was pressed to toggle inventory
            this.Ibutton.Execute(this.gameObject);
        }
    }

    void Move()
    {
        if (moveDir.x > 0.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveDir.x < 0.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        body.velocity = new Vector2(moveDir.x * playerStat.currentMovespeed, moveDir.y * playerStat.currentMovespeed);    //Apply velocity
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectible collectible))
        {
            collectible.Collect();
        }
        
        if (collision.gameObject.tag == "Gem") 
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Gem, amount = 1});
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Mushroom")
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Mushroom, amount = 1});
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Heart")
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Heart, amount = 1});
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "CriticalSurge")
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.CriticalSurge, amount = 1});
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Swift")
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Swift, amount = 1});
            Destroy(collision.gameObject);
        }
    }

    private void UseItem(Item item)
    {
        if (itemCooldownTimer <= 0.0f)
        {
            switch (item.itemType)
            {
                case Item.ItemType.Heart:
                    Debug.Log("use a Heart");
                    inventory.RemoveItem(new Item {itemType = Item.ItemType.Heart, amount = 1});
                    StartCoroutine(FlashObject(this.gameObject, 0.5f, Color.green));
                    playerStat.RestoreHealth(15.0f);
                    break;
                case Item.ItemType.CriticalSurge:
                    Debug.Log("use a CriticalSurge");
                    inventory.RemoveItem(new Item {itemType = Item.ItemType.CriticalSurge, amount = 1});
                    StartCoroutine(FlashObject(this.gameObject, 0.5f, Color.yellow));
                    playerStat.BoostCrit(3.0f);
                    break;
                case Item.ItemType.Swift:
                    Debug.Log("use a Swift");
                    inventory.RemoveItem(new Item {itemType = Item.ItemType.Swift, amount = 1});
                    playerStat.BoostSpeed(1.5f);
                    
                    // this.gameObject.GetComponentInChildren<testWeaponController>().weaponData.CooldownDuration = 0.1f;

                    StartCoroutine(FlashObject(this.gameObject, 0.5f, Color.gray));
                    break;
            }
            itemCooldownTimer = itemUsageCooldown;
        }

    }


    public static IEnumerator FlashObject(GameObject obj, float flashDuration, Color c)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        Color originalColor = renderer.material.color;
        Color flashColor = c;

        renderer.material.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        renderer.material.color = originalColor;
    }
}