using System.Collections;
using UnityEngine;

public class UserStats : MonoBehaviour
{
    public string username;
    public int level;
    public string UserClass;

    public float curHp;
    public float maxHp;
    public float curMana;
    public float maxMana;

    public float baseAttackPower;
    public float curAttackPower;
    public float baseAttackSpeed;
    public float curAttackSpeed;
    public float baseDodge;
    public float curDodge;
    public float baseHitPercent;
    public float curHitPercent;

    public float hpRegenTimer;
    public float hpRegenAmount;
    public float manaRegenTimer;
    public float manaRegenAmount;

    public float curXp;
    public float maxXp;

    public bool isDead;

    public GameObject selectedUnit;

    public EnemyStats enemyStatsScript;

    public bool behindEnemy;
    public bool canAttack;

    public float autoAttackCooldown;
    public float autoAttackCurTime;
    public bool canAutoAttack;

    public float doubleClickTimer;
    public bool didDoubleClick;

    public LayerMask RaycastLayers;
    public bool inLineOfSight;

    public bool hoverOverActive;
    public string hoverName;

    public float TickTime;

    public GameObject RangedSpellPrefab;

    //GUI Art
    public Texture hpBarTexture;
    public Texture manaBarTexture;
    public Texture barBackgroundTexture;

    //USER GUI Bars stats
    public float userHpBarLength;
    public float percentOfUserHp;
    public float userManaBarLength;
    public float percentOfUserMana;

    //Gathering
    public GameObject GatheringObjectSelected;
    public bool isGathering;
    public float GatheringTimer; //After this reach 0 will add item to inventory
    public Vector3 GatheringStartPos;

    //Custom Cursor Setup
    public Texture2D cursorMain;
    public Texture2D cursorBattle;
    public Texture2D cursorTalk;
    public Texture2D cursorClick;
    public Texture2D cursorTrade;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    //Spell system
    public Spell[] AllSpells;
    public Spell[] PlayerSpells;
    public Spell[] SpellNPC;
    public bool SpellLearnMenuShow;
    public Texture BlankIcon;
    public float attackRange;
    public bool isAttack = false;

    //Player Menus
    public bool PlayerSpellMenuShow;

    //Combat Text
    public GameObject CombatTextPrefab;

    //Quest Objectives
    public bool hasKey;
    public Transform doorHinge;

    //Transformations
    private bool beast;
    public GameObject beastAttack;
    private bool ghost;

    void Start()
    {
        //Add spells
        SpellNPC[0] = AllSpells[0];
        SpellNPC[1] = AllSpells[1];
        hasKey = false;

        Renderer rend = GetComponent<Renderer>();
    }
    
    void OnGUI ()
    {
        //display tooltip hover
        if(hoverOverActive)
        {
            GUI.Label(new Rect(Input.mousePosition.x - 100, Screen.height - Input.mousePosition.y, 100, 20), "" + hoverName);
        }

        GUI.DrawTexture(new Rect(20, 30, 120, 70), barBackgroundTexture);
        GUI.DrawTexture(new Rect(30, 40, userHpBarLength, 20), hpBarTexture);
        GUI.DrawTexture(new Rect(30, 65, userManaBarLength, 20), manaBarTexture);
        GUI.Label(new Rect(50, 40, 200, 20), "" + curHp + " / " + maxMana);
        GUI.Label(new Rect(50, 65, 200, 20), "" + curMana + " / " + maxMana);

        //Tooltip spell buttons
        Rect rect1 = new Rect(Screen.width / 2, Screen.height - 64, 32, 32);

        if (GUI.Button (new Rect (Screen.width/2, Screen.height - 64, 32, 32), "5"))
        {
            UsedSpell(PlayerSpells[0].id);
        }
        if(rect1.Contains(Event.current.mousePosition))
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 150, 150, 100), barBackgroundTexture);
            GUI.Label(new Rect(Input.mousePosition.x + 20, Screen.height - Input.mousePosition.y - 150, 150, 100),
                "Spell name: " + PlayerSpells[0].name + "\n" +
                "Spell description:" + PlayerSpells[0].description + "\n" +
                "Spell id: " + PlayerSpells[0].id);
        }

        if (hasKey)
        {
            GUI.Box(new Rect(215, 525, 320, 35), "You found the key!");
        }

        //Spell NPC Menu
        if (SpellLearnMenuShow)
        {
            //Show buy spell menu, Show spells that haven't been bought yet. Add spell to array when bought
            GUI.DrawTexture(new Rect(100, 200, 300, 400), barBackgroundTexture);

            int i;
            for(i=0;i<SpellNPC.Length;i++)
            {
                if(GUI.Button (new Rect (100, 200 + (i*50), 100, 32), "" + SpellNPC[i].name))
                {
                    //buy spell
                    int e;
                    for (e = 0; e < PlayerSpells.Length; e++)
                    {
                        if (PlayerSpells [e].icon == null)
                        {
                            PlayerSpells[e] = SpellNPC[i];
                            break;
                        }
                    }
                }
            }
        }

        //Spell Menu Button
        if (GUI.Button(new Rect(50, Screen.height - 64, 32, 64), "Player Spells Menu Button"))
        {
            PlayerSpellMenuShow = !PlayerSpellMenuShow;
        }

        //player spell menu
        if (PlayerSpellMenuShow)
        {
            //show player spell menu for spells learned
            GUI.DrawTexture(new Rect(450, 200, 300, 400), barBackgroundTexture);

            int j;
            int SpellNum = 0;
            for (j = 0; j < 2; j++)
            {

                int f;
                for (f = 0; f < 4; f++)
                {

                    //if spell array is null then display blank icon
                    if (PlayerSpells[SpellNum].icon == null)
                    {
                        //Display Blank Icon
                        if(GUI.Button(new Rect(500 + (j * 120), 220 + (f * 64), 48, 48), BlankIcon, GUIStyle.none))
                        {
                            //No spell
                            print("No spell here");
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(500 + (j * 120), 220 + (f * 64), 48, 48), PlayerSpells[SpellNum].icon, GUIStyle.none))
                        {
                            print("Spell: " + PlayerSpells[SpellNum].name);
                        }
                    }
                    SpellNum++;
                }
            }
        }
    }
    void Update()
    {
        //Mana Regen
        if(curMana < 100)
        {
            curMana += 2 * Time.deltaTime;
        }

        /*(
        //Spell
        if(Input.GetKeyDown("5"))
        {
            UsedSpell(PlayerSpells[0].id);
        }

        //Spell
        if (Input.GetKeyDown("6"))
        {
            UsedSpell(PlayerSpells[1].id);
        }*/

        //User bars calculated
        if(curHp <= maxHp)
        {
            percentOfUserHp = curHp / maxHp;
            userHpBarLength = percentOfUserHp * maxHp;
            percentOfUserMana = curMana / maxMana;
            userManaBarLength = percentOfUserMana * maxMana;
        }

        //Make sure mana and hp don't exceed max values
        if(curHp > maxHp)
            curHp = maxHp;
        if (curMana > maxMana)
            curMana = maxMana;

        //Make sure values can't go below 0
        if (curHp < 0)
            curHp = 0;
        if (curMana < 0)
            curMana = 0;
        
        if(Input.GetMouseButtonDown(0))
        {
            SelectTarget(0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            //SelectTarget(1);
            RightClickObject();
        }

        if (selectedUnit != null)
        {
            Vector3 toTarget = (selectedUnit.transform.position - transform.position).normalized;
            //Check if player is behind enemy (Calc dodge, parry, extra dmg, ect.)
            if (Vector3.Dot(toTarget, selectedUnit.transform.forward) < 0)
            {
                behindEnemy = false;
            }
            else
            {
                behindEnemy = true;
            }

            //Calc if the player is facing the enemy and is within attack distance.
            float distance = Vector3.Distance(this.transform.position, selectedUnit.transform.position);
            Vector3 targetDir = selectedUnit.transform.position - transform.position;
            Vector3 forward = transform.forward;
            float angle = Vector3.Angle(targetDir, forward);

            if (angle > 60.0)
            {
                canAttack = false;
                autoAttackCurTime = 0;
            }
            else
            {
                if(distance < 60)
                {
                    canAttack = true;
                }
                else
                {
                    canAttack = false;
                    autoAttackCurTime = 0;
                }
            }

            //Detect if theres an object blocking between selected enemy and player (LOS)
            RaycastHit hit;
            if(Physics.Linecast(selectedUnit.transform.position, transform.position, out hit, RaycastLayers))
            {
                inLineOfSight = false;
            }
            else
            {
                inLineOfSight = true;
            }
            
        //double click detect
        if(doubleClickTimer > 0)
            {
                doubleClickTimer -= Time.deltaTime;
            }
            else
            {
                didDoubleClick = false;
            }
        }

        //autoAttack
        if (selectedUnit != null && canAttack && canAutoAttack == true)
        {
            if(autoAttackCurTime < autoAttackCooldown)
            {
                autoAttackCurTime += Time.deltaTime;
            }
            else
            {
                BasicAttack();
                autoAttackCurTime = 0;
            }
        }

        /*if (Input.GetKeyDown("1"))
        {
            if(beast)
            {
                Instantiate(beastAttack, transform.position + (transform.forward * 2), transform.rotation);
            }
        }*/

        if (Input.GetKeyDown(KeyCode.J))
        {
            if(!ghost && !beast)
            {
                beast = true;
                StartCoroutine(BeastMode());
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!ghost && !beast)
            {
                ghost = true;
                StartCoroutine(GhostMode());
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if(beast || ghost)
            {
                beast = false;
                ghost = false;
                HumanMode();
            }
        }

        //Ranged Spell
        if(Input.GetKeyDown ("2"))
        {
            float distance = Vector3.Distance(this.transform.position, selectedUnit.transform.position);
            if (distance < attackRange && !isAttack)
            {
                if (curMana >= 10)
                {
                    RangedSpell();
                    isAttack = false;
                    curMana -= 10.0f;
                }
            }
        }

        //tooltip pop-up
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2;

        if(Physics.Raycast (ray2, out hit2, 10000))
        {
            if(hit2.transform.tag == "enemy")
            {
                Cursor.SetCursor(cursorBattle, hotSpot, cursorMode);
                hoverName = hit2.transform.GetComponent<EnemyStats>().enemyName;
                hoverOverActive = true;
            }
            else if (hit2.transform.tag == "talkNPC")
            {
                Cursor.SetCursor(cursorTalk, hotSpot, cursorMode);
            }
            else if (hit2.transform.tag == "question")
            {
                Cursor.SetCursor(cursorClick, hotSpot, cursorMode);
            }
            else if(hit2.transform.tag == "trader")
            {
                Cursor.SetCursor(cursorTrade, hotSpot, cursorMode);
            }
            else
            {
                //origional cursor
                Cursor.SetCursor(cursorMain, hotSpot, cursorMode);
                hoverOverActive = false;
            }
        }

        if(isGathering)
        {
            if(GatheringTimer > 0)
            {
                GatheringTimer -= Time.deltaTime;
            }
            else
            {
                print("Gathered item");
                isGathering = false;
                Destroy(GatheringObjectSelected.gameObject);
            }

            if(this.transform.position != GatheringStartPos)
            {
                isGathering = false;
                print("INTERRUPTED: Player moved while gathering");
            }
        }
    }
   

    void RightClickObject ()
    {
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2;

        if (Physics.Raycast (ray2, out hit2, 150))
        {
            if (hit2.transform.tag == "Ore")
            {
                if(!isGathering)
                {
                    print("Started Gathering");
                    GatheringObjectSelected = hit2.transform.gameObject;
                    GatheringStartPos = this.transform.position;
                    GatheringTimer = 3.0f;
                    isGathering = true;
                }
            }
            if (hit2.transform.tag ==  "spellNPC")
            {
                //Show learn spell menu
                SpellLearnMenuShow =! SpellLearnMenuShow;
            }
        }
    }
    void SelectTarget (int selectedNum)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 10000))
        {
            if(hit.transform.tag == "enemy")
            {
                selectedUnit = hit.transform.gameObject;

                selectedUnit.transform.GetComponent<EnemyStats>().Selected();

                enemyStatsScript = selectedUnit.transform.gameObject.transform.GetComponent<EnemyStats>();

                if(selectedNum == 0 && selectedUnit == null)
                {
                    canAutoAttack = false;
                }
                else if (selectedNum == 1)
                {
                    canAutoAttack = true;
                }
            }
            else
            {
                if(selectedUnit != null)
                {
                    if(didDoubleClick == false)
                    {
                        didDoubleClick = true;
                        doubleClickTimer = 0.3f;
                    }
                    else
                    {
                        selectedUnit.transform.GetComponent<EnemyStats>().Deselected();
                        print("DESELECT");
                        selectedUnit = null;
                        didDoubleClick = false;
                        doubleClickTimer = 0;
                        autoAttackCurTime = 0;
                    }
                }
            }
        }
    }

    public void BasicAttack ()
    {
        enemyStatsScript.RecieveDamage(10);
    }

    void RangedSpell()
    {
        Vector3 SpawnSpellLoc = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        
        GameObject clone;
        clone = Instantiate(RangedSpellPrefab, SpawnSpellLoc, Quaternion.identity);
        clone.transform.GetComponent<RangedSpell>().Target = selectedUnit;

    }

    IEnumerator BeastMode()
    {
        this.GetComponent<UserMovement>().runSpeed = 25;
        this.gameObject.tag = "Beast";
        GetComponent<Renderer>().material.color = Color.green;

        yield return new WaitForSeconds(5);

        HumanMode();

    }

    IEnumerator GhostMode()
    {
        this.gameObject.tag = "Ghost";
        GetComponent<Renderer>().material.color = Color.magenta;

        yield return new WaitForSeconds(5);

        HumanMode();
    }

    void HumanMode()
    {
        this.gameObject.tag = "Player";
        beast = false;
        ghost = false;
        GetComponent<Renderer>().material.color = Color.blue;
    }

    public void RecieveDamage(float dmg)
    {
        curHp -= dmg;
    }

    public void OpenDoor()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var newRot = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(0.0f, -70.0f, 0.0f), Time.deltaTime * 250);
            doorHinge.rotation = newRot;
            hasKey = false;
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Platform")
        {
            transform.parent = GameObject.Find ("PlatformNode").transform;
        }
    }
    void OnCollisionExit()
    {
        transform.parent = null;
    }

    void OnTriggerStay (Collider collisionInfo2)
    {
        //PoisonTrap Damage/Slow
        if (collisionInfo2.gameObject.tag == "PoisonArea")
        {
            TickTime -= 1 * Time.deltaTime;
            this.GetComponent<UserMovement>().runSpeed = 10;
            GetComponent<Renderer>().material.color = Color.red;

            if (TickTime <= 0)
            {
                curHp -= 1;
                GetComponent<Renderer>().material.color = Color.blue;
                TickTime = 5.0f;
            }
        }

        //SpearTrap Damage
        if (collisionInfo2.gameObject.tag == "SpearTrap")
        {
            GetComponent<Renderer>().material.color = Color.red;
            TickTime -= 1 * Time.deltaTime;

            if (TickTime <= 0)
            {
                curHp -= 3;
                GetComponent<Renderer>().material.color = Color.blue;
                TickTime = 2;
            }
        }

        //FireTrap Damage
        if (collisionInfo2.gameObject.tag == "FireTrap")
        {
            GetComponent<Renderer>().material.color = Color.red;
            TickTime -= 1 * Time.deltaTime;

            if (TickTime <= 0)
            {
                curHp -= 5;
                GetComponent<Renderer>().material.color = Color.blue;
                TickTime = 1;
            }
        }

        //SpikeTrap Damage
        if (collisionInfo2.gameObject.tag == "AxeTrap")
        {
            GetComponent<Renderer>().material.color = Color.red;
            TickTime -= 1 * Time.deltaTime;

            if (TickTime <= 0)
            {
                curHp -= 5;
                GetComponent<Renderer>().material.color = Color.blue;
                TickTime = 0.5f;
            }
        }
    }
    
    void OnTriggerEnter(Collider collisionInfo3)
    {
        //Health Pickup
        if (collisionInfo3.gameObject.tag == "Health")
        {
            curHp += 10f;
        }

        //Mana Pickup
        if (collisionInfo3.gameObject.tag == "Mana")
        {
            curMana += 50f;
        }

        //Extra Health
        if(collisionInfo3.gameObject.tag == "Heart")
        {
            maxHp += 20f;
        }

        //QuestItem Pickup
        if (collisionInfo3.gameObject.tag == "Key")
        {
            hasKey = true;
        }

        //Quest Check
        if (collisionInfo3.gameObject.tag == "QuestDoor")
        {
            if (hasKey)
            {
                var newRot = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(0.0f, 90f, 0.0f), Time.deltaTime * 1500);
                doorHinge.rotation = newRot;
                hasKey = false;
            }
        }
    }
    void OnTriggerExit(Collider collisionInfo3)
    {
        //Resume normal Speed after Poison Trap
        if (collisionInfo3.gameObject.tag == "PoisonArea")
        {
            this.GetComponent<UserMovement>().runSpeed = 40;
        }
    }

    void UsedSpell(int id)
    {
        switch (id)
        {
            case 0:
                print("Used spell 1");
                DealMeleeCombatDamage(Random.Range(100, 1000));
                break;
            case 1:
                print("Used spell 2");
                break;
            case 2:
                print("Used spell 3");
                break;
            case 3:
                print("Used spell 4");
                break;
            case 4:
                print("Used spell 5");
                break;
            default:
                print("Spell Error");
                    break;
        }
    }

    void DealMeleeCombatDamage (int Damage)
    {
        //Calculate damage here

        //Spawn combat text for damage
        GameObject clone;
        clone = Instantiate(CombatTextPrefab, transform.position, Quaternion.identity);
        clone.transform.GetComponent<CombatText>().mainTarget = selectedUnit;
        clone.transform.GetComponent<CombatText>().damage = Damage;
    }
}
