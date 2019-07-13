using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string enemyName;
    public float curHp;
    public float maxHp;

    public GameObject TextName;
    public bool isSelected;

    public bool isDead;
    public float respawnTime;
    public GameObject RespawnPointLoc;

    public bool inCombat;
    public float wanderTime;
    public float movementSpeed;
    public float aggroRadius;
    public float resetPOS;

    public GameObject Target;

    //Attack
    public int AttackDamageMin;
    public int AttackDamageMax;
    public float AttackCooldownTimeMain;
    public float AttackCooldownTime;

    //Shaders
    public Shader shader1;
    public Shader shader2;
    public Renderer rend;

    //Enemy return
    public bool returnToSpawnPoint;

    //Loot Drop
    public GameObject obj1;

    void Start()
    {
        //rend = GetComponent<Renderer>();
        shader1 = Shader.Find("Legacy Shaders/Diffuse");
        shader2 = Shader.Find("Legacy Shaders/Self-Illumin/Diffuse");
    }
    
    void Update()
    {
        if (!isDead)
        {
            //Check to make sure enemy isn't too far from spawn
            float distanceFromSpawn = Vector3.Distance(RespawnPointLoc.transform.position, this.transform.position);
            if (distanceFromSpawn > resetPOS && !returnToSpawnPoint)
            {
                Target = null;
                returnToSpawnPoint = true;
            }

            if (Target == null)
            {
                if(returnToSpawnPoint)
                {
                    ReturnToSpawn();
                }
                else
                SearchForTarget();

                if (wanderTime > 0)
                {
                    transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                    wanderTime -= Time.deltaTime;
                }
                else
                {
                    wanderTime = Random.Range(2.0f, 8.0f);
                    Wander();
                }
            }
            else
            {
                FollowTarget();
            }
        }

        if (TextName != null)
        {
            TextName.transform.LookAt(Camera.main.transform.position);
            TextName.transform.Rotate(0, 180, 0);
            TextName.GetComponent<TextMesh>().color = Color.red;
            TextName.GetComponent<TextMesh>().text = "" + enemyName;
        }
        
        if(curHp <= 0 && !isDead)
        {
            isDead = true;
            curHp = 0;
            LootTable();
        }
    }

    void SearchForTarget()
    {
        Vector3 center = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Collider[] hitColliders = Physics.OverlapSphere(center, aggroRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if(hitColliders[i].transform.tag == "Player")
            {
                //can do anything in here like different attacks/abilities, cleave
                Target = hitColliders[i].transform.gameObject;
            }
            i++;
        }
    }

    void FollowTarget()
    {
        //Face towards Target always
        Vector3 targetPosition = Target.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);

        float distance = Vector3.Distance(Target.transform.position, this.transform.position);
        if(distance > 15)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else
        {
            if(AttackCooldownTime > 0)
            {
                AttackCooldownTime -= Time.deltaTime;
            }
            else
            {
                AttackCooldownTime = AttackCooldownTimeMain;
                AttackTarget();
            }
        }
        if (distance > 100)
        {
            Target = null;
            returnToSpawnPoint = true;
        }
    }

    void Wander()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    }

    void AttackTarget()
    {
        int RandomNum = Random.Range(1, 5);

        //Special Attack
        if(RandomNum == 3)
        {
            Target.transform.GetComponent<UserStats>().RecieveDamage(Random.Range(AttackDamageMin * 2, AttackDamageMax * 2));
            Target.transform.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            Target.transform.GetComponent<UserStats>().RecieveDamage(Random.Range(AttackDamageMin, AttackDamageMax));
            Target.transform.GetComponent<Renderer>().material.color = Color.red;
            StartCoroutine(ResetPlayer());
        }
    }

    IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        Target.transform.GetComponent<Renderer>().material.color = Color.blue;
    }

    void ReturnToSpawn()
    {
        Vector3 spawnPosition = RespawnPointLoc.transform.position;
        spawnPosition.y = transform.position.y;
        transform.LookAt(spawnPosition);

        float distance = Vector3.Distance(RespawnPointLoc.transform.position, this.transform.position);
        if(distance > resetPOS)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else
        {
            //Made it back to spawn
            returnToSpawnPoint = false;
        }
    }
    public void RecieveDamage (float dmg)
    {
        if(returnToSpawnPoint)
        {
            print("EVADE");
        }
        else
        {
            curHp -= dmg;
        }

        print("damage done = " + dmg);
        print("enemy hp = " + curHp);
    }

    public void LootTable()
    {
        int rand = Random.Range(0, 4);

        if (rand == 0)
        {
            Instantiate(obj1, transform.position + (transform.up * 3), transform.rotation);
            Debug.Log("Item Dropped!");
        }
        if (rand == 1)
        {
            Instantiate(obj1, transform.position + (transform.up * 3), transform.rotation);
            Debug.Log("Item Dropped!");
        }
        if (rand == 2)
        {
            Instantiate(obj1, transform.position + (transform.up * 3), transform.rotation);
            Debug.Log("Item Dropped!");
        }
        if (rand == 3)
        {
            Instantiate(obj1, transform.position + (transform.up * 3), transform.rotation);
            Debug.Log("Item Dropped!");
        }

        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(respawnTime);
        RespawnPointLoc.GetComponent<RespawnPoint>().SpawnEnemy();
        Destroy(this.gameObject);
    }

    public void Selected()
    {
        rend.material.shader = shader2;
    }

    public void Deselected()
    {
        rend.material.shader = shader1;
    }
}
