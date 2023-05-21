using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    // [SerializeField] public GameObject sushiPrefab;
    // [SerializeField] public GameObject vegesPrefab;
    // [SerializeField] public GameObject meatPrefab;

    // [SerializeField] public GameObject applePrefab;
    // [SerializeField] public GameObject blueberryPrefab;
    // [SerializeField] public GameObject kiwiPrefab;

    // [SerializeField] public GameObject orangePrefab;

    // [SerializeField] public GameObject strawberryPrefab;

    // [SerializeField] public GameObject watermelonPrefab;


    public FruitScriptableObject fruitData;
    public EnemyScriptableObject enemyData;

    float currentDamage;
    float currentSpeed;
    float currentHp;
    public float despawnDistance = 20.0f;

    Transform player;

    // Update is called once per frame
    void Awake()
    {
        currentDamage = enemyData.Damage;
        currentSpeed = enemyData.Speed;
        currentHp = enemyData.MaxHp;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(float damage)
    {   
        float playerCritChance = player.gameObject.GetComponent<PlayerStat>().currentCritChance;
        float realDamage;
        bool isCriticalHit = Random.Range(0,100) < playerCritChance;
        if(isCriticalHit)
        {
            realDamage = damage * 2.0f;
        }
        else
        {
            realDamage = damage;
        }
        realDamage -= enemyData.Defence;
        if(realDamage <= 1)
        {
            currentHp -= 1;
        }
        else
        {
            currentHp -= realDamage;
        }
        
        DamgePopup.Create(this.gameObject.transform.position, (int)realDamage, isCriticalHit);
        
        if(currentHp <= 0)
        {
            Kill();
        }
    }


    public void Kill()
    {
        Destroy(gameObject);
        Debug.Log(fruitData.applePrefab);
        float sushiChance = Random.value;
        float vegesChance = Random.value;
        float meatChance = Random.value;

        float fruitChance1 = Random.value;
        float fruitChance2 = Random.value;
        float fruitChance3 = Random.value;
        float fruitChance4 = Random.value;
        float fruitChance5 = Random.value;
        float fruitChance6 = Random.value;


        if (fruitChance1 < 0.6f) 
            Instantiate(fruitData.applePrefab, transform.position, Quaternion.identity);
        if (fruitChance2 < 0.6f) 
            Instantiate(fruitData.blueberryPrefab, transform.position, Quaternion.identity);
        if (fruitChance3 < 0.6f) 
            Instantiate(fruitData.kiwiPrefab, transform.position, Quaternion.identity);
        if (fruitChance4 < 0.6f) 
            Instantiate(fruitData.orangePrefab, transform.position, Quaternion.identity);
        if (fruitChance5 < 0.6f) 
            Instantiate(fruitData.strawberryPrefab, transform.position, Quaternion.identity);
        if (fruitChance6 < 0.6f) 
            Instantiate(fruitData.watermelonPrefab, transform.position, Quaternion.identity);


        if (sushiChance < 0.3f) 
            Instantiate(fruitData.sushiPrefab, transform.position, Quaternion.identity);

        if (vegesChance < 0.25f) 
            Instantiate(fruitData.vegesPrefab, transform.position, Quaternion.identity);

        if (meatChance < 0.2f) 
            Instantiate(fruitData.meatPrefab, transform.position, Quaternion.identity);

    }
    private void OnCollisionStay2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            PlayerStat ps = collision2D.gameObject.GetComponent<PlayerStat>();
            ps.TakeDamage(currentDamage);
        }
    }

    private void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        //  ew = FindObjectOfType<EnemyWave>();
        transform.position = player.position + es.GenerateRandomPosition();
    }
}
