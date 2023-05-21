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

        // Inventory is not open, left mouse click or right mouse click to change attack direction
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


        if (Input.GetKeyDown(KeyCode.Keypad1) || (Input.GetKeyDown(KeyCode.Alpha1)))
        {
            // Debug.Log("Number 1 clicked");
            // this.inventory.UseItem(this.inventory.GetItemList);
            Item targetItem = null;
            foreach (Item item in this.inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.Sushi)
                {
                    targetItem = item;
                }
            }
            if (targetItem != null)
            {
                inventory.UseItem(targetItem);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || (Input.GetKeyDown(KeyCode.Alpha2)))
        {
            Item targetItem = null;
            foreach (Item item in this.inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.Veges)
                {
                    targetItem = item;
                }
            }
            if (targetItem != null)
            {
                inventory.UseItem(targetItem);
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || (Input.GetKeyDown(KeyCode.Alpha3)))
        {
            Item targetItem = null;
            foreach (Item item in this.inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.Meat)
                {
                    targetItem = item;
                }
            }
            if (targetItem != null)
            {
                inventory.UseItem(targetItem);
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
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (moveDir.x < 0.0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        body.velocity = new Vector2(moveDir.x * playerStat.currentMovespeed, moveDir.y * playerStat.currentMovespeed);    //Apply velocity
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollectible collectible))
        {
            collectible.Collect();
        }
        
        if (collision.gameObject.tag == "Sushi") 
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Sushi, amount = 1});
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Veges")
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Veges, amount = 1});
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Meat")
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Meat, amount = 1});
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Apple")
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Apple, amount = 1});
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Blueberry")
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Blueberry, amount = 1});
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Kiwi")
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Kiwi, amount = 1});
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Orange")
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Orange, amount = 1});
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Strawberry")
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Strawberry, amount = 1});
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Watermelon")
        {
            this.inventory.AddItem(new Item {itemType = Item.ItemType.Watermelon, amount = 1});
            Destroy(collision.gameObject);
        }
    }

    private void UseItem(Item item)
    {
        if (itemCooldownTimer <= 0.0f)
        {
            switch (item.itemType)
            {
                case Item.ItemType.Sushi:
                    Debug.Log("use a Sushi");
                    inventory.RemoveItem(new Item {itemType = Item.ItemType.Sushi, amount = 1});
                    StartCoroutine(FlashObject(this.gameObject, 0.5f, Color.green));
                    playerStat.RestoreHealth(15.0f);
                    break;
                case Item.ItemType.Veges:
                    Debug.Log("use a Veges");
                    inventory.RemoveItem(new Item {itemType = Item.ItemType.Veges, amount = 1});
                    StartCoroutine(FlashObject(this.gameObject, 0.5f, Color.yellow));
                    playerStat.BoostCrit(3.0f);
                    break;
                case Item.ItemType.Meat:
                    Debug.Log("use a Meat");
                    inventory.RemoveItem(new Item {itemType = Item.ItemType.Meat, amount = 1});
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

    public int GetScore()
    {
        Debug.Log("Called?");
        int score = 0000;
        var list = this.inventory.GetItemList();
        // Item itemClass = GetComponent<Item>();
        Debug.Log("Called2");

        foreach (var item in list)
        {
            var type = item.itemType;

            if (type == Item.ItemType.Apple)
            {
                Debug.Log("Gem :" + item.amount);
                score += 10 * item.amount;
            }
            if (type == Item.ItemType.Blueberry)
            {
                Debug.Log("Gem :" + item.amount);
                score += 10 * item.amount;
            }
            if (type == Item.ItemType.Kiwi)
            {
                Debug.Log("Gem :" + item.amount);
                score += 10 * item.amount;
            }
            if (type == Item.ItemType.Orange)
            {
                Debug.Log("Gem :" + item.amount);
                score += 10 * item.amount;
            }
            if (type == Item.ItemType.Strawberry)
            {
                Debug.Log("Gem :" + item.amount);
                score += 10 * item.amount;
            }
            if (type == Item.ItemType.Watermelon)
            {
                Debug.Log("Gem :" + item.amount);
                score += 10 * item.amount;
            }
        }
        Debug.Log("Called?3");

        return score;
    }
}