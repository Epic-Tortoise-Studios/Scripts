using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformationController : MonoBehaviour
{
    #region Sigleton
    private static TransformationController instance;
    public static TransformationController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<TransformationController>();
            return instance;
        }
    }
    #endregion

    public TransformationSO humanTransformation;
    public TransformationSO ghostTransformation;
    public TransformationSO beastTransformation;

    private GameObject player;
    public GameObject playerRig;
    //public GameObject playerHand;
    public GameObject beastRig;
    [HideInInspector]
    public GameObject beastFist; //Need to remove
    [HideInInspector]
    public GameObject possessableEnemy;
    private Animator anim;
    private Animator savedAnim;

    public Transform bodySpawnPosition;
    [HideInInspector]
    public GameObject droppedBody;
    [HideInInspector]
    public GameObject transformPosition;
    public GameObject particleTarget;

    //Objects to turn on during Ghost Mode
    private GameObject[] spirits;
    private GameObject[] ghostWalls;
    private GameObject[] solidWalls;
    private GameObject[] impassableWalls;
    private GameObject[] possessableIndicators;
    private GameObject[] breadcrumbs;
    private GameObject[] ghostFog;

    //For Cosmetics
    private ParticleSystem particleAttractor;
    private Light mainLight;
    private Color colorChange;
    private float duration = 5;
    private float smoothness = 0.02f;

    private Image abilityImage;
   // private Image cooldownImage;
    public Sprite possessedSprite;
    private TMPro.TextMeshProUGUI abilityText;
    [HideInInspector]
    public TMPro.TextMeshProUGUI controlText;
    [HideInInspector]
    public TMPro.TextMeshProUGUI infoText;
    public float cooldown = 10;

    public float possessTime;
    public float possessWalkSpeed;
    public float possessSprintSpeed;

    public bool canTransform = true;
    private bool isCooldown;
    private bool isGhost;
    [HideInInspector]
    public bool possessedEnemy;
    [HideInInspector]
    public bool insideEnemy;

    public bool oldBeast = false;

    public TransformationType type;

    public enum TransformationType
    {
        HUMAN,
        GHOST,
        BEAST,
    }

    void Start()
    {
        player = this.gameObject;
        anim = PlayerController.Instance.anim;
        savedAnim = PlayerController.Instance.anim;
        mainLight = GameObject.FindGameObjectWithTag("MainLight").GetComponent<Light>();
        particleAttractor = gameObject.GetComponentInChildren<ParticleSystem>();
        spirits = GameObject.FindGameObjectsWithTag("Spirit");
        ghostWalls = GameObject.FindGameObjectsWithTag("GhostWall");
        solidWalls = GameObject.FindGameObjectsWithTag("SolidWall");
        impassableWalls = GameObject.FindGameObjectsWithTag("ImpassableWall");
        possessableIndicators = GameObject.FindGameObjectsWithTag("Possessable");
        breadcrumbs = GameObject.FindGameObjectsWithTag("Breadcrumb");
        ghostFog = GameObject.FindGameObjectsWithTag("GhostFog");

        /*
        abilityImage = GameObject.FindObjectOfType<AbilityImageCheck>().GetComponent<Image>();
        //cooldownImage = GameObject.FindObjectOfType<CDImageCheck>().GetComponent<Image>();
        abilityText = GameObject.FindObjectOfType<AbilityTextCheck>().GetComponent<TMPro.TextMeshProUGUI>();
        controlText = GameObject.FindObjectOfType<ControlTextCheck>().GetComponent<TMPro.TextMeshProUGUI>();
        

        abilityImage.sprite = humanTransformation.sprite;
        abilityText.text = "Human";
      */

        infoText = GameObject.FindObjectOfType<InfoTextCheck>().GetComponent<TMPro.TextMeshProUGUI>();
        infoText.enabled = false;

        humanTransformation.walkSpeed = PlayerController.Instance.walkSpeed;
        humanTransformation.sprintSpeed = PlayerController.Instance.sprintSpeed;
        humanTransformation.jumpForce = PlayerController.Instance.jumpForce;

        //playerHand.SetActive(false);

        #region Loops
        foreach (GameObject s in spirits)
        {
            if (s != null)
            {
                s.SetActive(false);
            }
        }

        foreach (GameObject w in ghostWalls)
        {
            if (w != null)
            {
                w.SetActive(false);
            }
        }

        foreach(GameObject i in possessableIndicators)
        {
            if(i != null)
            {
                i.SetActive(false);
            }
        }

        foreach (GameObject i in impassableWalls)
        {
            if (i != null)
            {
                i.SetActive(false);
            }
        }

        foreach (GameObject b in breadcrumbs)
        {
            if (b != null)
            {
                b.SetActive(false);
            }
        }
        #endregion

    }

    void Update()
    {
        TransformInput();

        /*if(abilityImage == null)
        {
            abilityImage = GameObject.FindObjectOfType<AbilityImageCheck>().GetComponent<Image>();
            //cooldownImage = GameObject.FindObjectOfType<CDImageCheck>().GetComponent<Image>();
            abilityText = GameObject.FindObjectOfType<AbilityTextCheck>().GetComponent<TMPro.TextMeshProUGUI>();
            controlText = GameObject.FindObjectOfType<ControlTextCheck>().GetComponent<TMPro.TextMeshProUGUI>();
            infoText = GameObject.FindObjectOfType<InfoTextCheck>().GetComponent<TMPro.TextMeshProUGUI>();
        }*/

        /*if (isCooldown)
        {
            cooldownImage.fillAmount -= 1 / cooldown * Time.deltaTime;
        }
        else
        {
            cooldownImage.fillAmount = 1;
            cooldownImage.gameObject.SetActive(false);
        }*/
    }

    void TransformInput()
    {
        if (canTransform)
        {
            if (Input.GetKeyDown(ghostTransformation.transformKey))
            {
                if (type != TransformationType.HUMAN && !possessedEnemy)
                {
                    PlayerController.Instance.anim = savedAnim;
                    beastRig.SetActive(false);
                    playerRig.SetActive(true);
                    type = humanTransformation.type;
                    HumanType();
                }

                else if (type == TransformationType.HUMAN && !possessedEnemy)
                {
                    if (transformPosition != null)
                    {
                        Destroy(GameObject.FindGameObjectWithTag("TransformPosition"));
                    }

                    type = ghostTransformation.type;
                    GhostType();
                }
            }
            else if (Input.GetKey(beastTransformation.transformKey) && oldBeast)
            {
                if (type == TransformationType.HUMAN && !possessedEnemy)
                {
                    type = beastTransformation.type;
                    PlayerController.Instance.anim = beastRig.GetComponent<Animator>();
                    BeastType();
                }
            }
            //UPDATED 8/21/19 by:Chris (Combined Ghost/Human into one key press)
            /*else if (Input.GetKeyDown(humanTransformation.transformKey))
            {
                if (type != TransformationType.HUMAN && !possessedEnemy)
                {
                    PlayerController.Instance.anim = savedAnim;
                    beastRig.SetActive(false);
                    playerRig.SetActive(true);
                    type = humanTransformation.type;
                    HumanType();
                }
            }*/
        }
    }

    #region TypeFunctions

    public void HumanType()
    {
        gameObject.layer = 0;
        Physics.IgnoreLayerCollision(12, 13, false);

        if(abilityImage != null)
        {
            abilityImage.sprite = humanTransformation.sprite;
            abilityText.text = "Human";
        }
        PlayerController.Instance.walkSpeed = humanTransformation.walkSpeed;
        PlayerController.Instance.sprintSpeed = humanTransformation.sprintSpeed;
        PlayerController.Instance.jumpForce = humanTransformation.jumpForce;
        colorChange = humanTransformation.lightColor;

        if (isGhost)
        {
            StartCoroutine(GhostToHuman());
            anim.SetBool("isGhost", false);
        }

        if (mainLight != null)
        {
            StartCoroutine(LerpColor());
        }
    }

    public void GhostType()
    {
        gameObject.layer = 12;
        Physics.IgnoreLayerCollision(12, 13, true);

        if(abilityImage != null)
        {
            abilityImage.sprite = ghostTransformation.sprite;
            abilityText.text = "Ghost";
        }
        PlayerController.Instance.walkSpeed = ghostTransformation.walkSpeed;
        PlayerController.Instance.sprintSpeed = ghostTransformation.sprintSpeed;
        PlayerController.Instance.jumpForce = ghostTransformation.jumpForce;
        colorChange = ghostTransformation.lightColor;

        anim.SetBool("isGhost", true);

        if (droppedBody == null)
        {
            StartCoroutine(HumanToGhost());
        }
        else
        {
            Debug.Log("Body Already Instantiated");
        }

        if (mainLight != null)
        {
            StartCoroutine(LerpColor());
        }
    }

    void BeastType()
    {
        abilityImage.sprite = beastTransformation.sprite;
        abilityText.text = "Beast";
        PlayerController.Instance.walkSpeed = beastTransformation.walkSpeed;
        PlayerController.Instance.sprintSpeed = beastTransformation.sprintSpeed;
        colorChange = beastTransformation.lightColor;

        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.PlayClip(beastTransformation.transformAudio);

        beastRig.SetActive(true);
        playerRig.SetActive(false);

        if (mainLight != null)
        {
            StartCoroutine(LerpColor());
        }
    }

    void PossessEnemy()
    {
        if (possessedEnemy)
        {
            anim.SetBool("isPossessing", true);
            Debug.Log("Possessed Enemy In Player Abilities");

        }
        else
        {
            anim.SetBool("isPossessing", false);
        }
    }

    #endregion

    #region Coroutines
    public IEnumerator HumanToGhost()
    {
        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.PlayClip(ghostTransformation.transformAudio);
        Instantiate(ghostTransformation.instantiateObj, bodySpawnPosition.transform.position, bodySpawnPosition.transform.rotation);
        droppedBody = GameObject.FindGameObjectWithTag("DroppedBody");
        isGhost = true;
        
        #region Loops
        foreach (GameObject s in spirits)
        {
            if (s != null)
            {
                s.SetActive(true);
            }
        }

        foreach (GameObject w in ghostWalls)
        {
            if(w != null)
            {
                w.SetActive(true);
            }
        }

        foreach (GameObject w in solidWalls)
        {
            if(w != null)
            {
                w.SetActive(false);
            }
        }

        foreach (GameObject i in possessableIndicators)
        {
            if (i != null)
            {
                i.SetActive(true);
            }
        }

        foreach (GameObject i in impassableWalls)
        {
            if (i != null)
            {
                i.SetActive(true);
            }
        }

        foreach (GameObject b in breadcrumbs)
        {
            if (b != null)
            {
                b.SetActive(true);
            }
        }

        foreach (GameObject f in ghostFog)
        {
            if (f != null)
            {
                f.SetActive(false);
            }
        }
        #endregion

        yield break;
    }

    public IEnumerator GhostToHuman()
    {
        isGhost = false;
        Destroy(droppedBody);

        #region Loops
        foreach (GameObject s in spirits)
        {
            if(s != null)
            {
                s.SetActive(false);
            }
        }

        foreach (GameObject w in ghostWalls)
        {
            if (w != null)
            {
                w.SetActive(false);
            }
        }

        foreach (GameObject w in solidWalls)
        {
            if (w != null)
            {
                w.SetActive(true);
            }
        }

        foreach (GameObject i in possessableIndicators)
        {
            if (i != null)
            {
                i.SetActive(false);
            }
        }

        foreach (GameObject i in impassableWalls)
        {
            if (i != null)
            {
                i.SetActive(false);
            }
        }

        foreach (GameObject b in breadcrumbs)
        {
            if (b != null)
            {
                b.SetActive(false);
            }
        }

        foreach (GameObject f in ghostFog)
        {
            if (f != null)
            {
                f.SetActive(true);
            }
        }
        #endregion

        //Fills variable with the empty game object instantiated OnDestroy from the body
        yield return new WaitForSeconds(.2f);
        transformPosition = GameObject.FindGameObjectWithTag("TransformPosition");

        //Teleports player to the transform we just set above and sets ghost state to false
        yield return new WaitForSeconds(.2f);
        this.gameObject.transform.position = transformPosition.transform.position;

        //Fades the camera back in, destroys teleport transform, makes sure bools are set to false, allows companions to follow again
        yield return new WaitForSeconds(2);
        Destroy(transformPosition);
    }

    public IEnumerator PossessingEnemy()
    {
        anim.SetBool("isPossessing", true);
        particleAttractor.Play();
        colorChange = humanTransformation.lightColor;
        StartCoroutine(LerpColor());
        yield return new WaitForSeconds(.5f);
        particleAttractor.Stop();
        abilityImage.sprite = possessedSprite;
        abilityText.text = "Possessed";
        //cooldownImage.gameObject.SetActive(true);
        infoText.enabled = false;

        //For Pickups
        //playerHand.SetActive(true);

        isCooldown = true;
        insideEnemy = true;
        anim.SetBool("isPossessing", true);

        type = humanTransformation.type;
        PlayerController.Instance.walkSpeed = possessWalkSpeed;
        PlayerController.Instance.sprintSpeed = possessSprintSpeed;
        PlayerController.Instance.jumpForce = humanTransformation.jumpForce;

        this.gameObject.transform.position = possessableEnemy.transform.position;
        possessableEnemy.GetComponent<CapsuleCollider>().isTrigger = true;
        possessableEnemy.transform.parent = this.gameObject.transform;
        possessableEnemy.transform.rotation = this.gameObject.transform.rotation;
        gameObject.layer = 0;

        #region OffLoops
        foreach (GameObject s in spirits)
        {
            if (s != null)
            {
                s.SetActive(false);
            }
        }

        foreach (GameObject w in ghostWalls)
        {
            if (w != null)
            {
                w.SetActive(false);
            }
        }

        foreach (GameObject w in solidWalls)
        {
            if (w != null)
            {
                w.SetActive(true);
            }
        }

        foreach (GameObject i in possessableIndicators)
        {
            if (i != null)
            {
                i.SetActive(false);
            }
        }

        foreach (GameObject i in impassableWalls)
        {
            if (i != null)
            {
                i.SetActive(false);
            }
        }

        foreach (GameObject b in breadcrumbs)
        {
            if (b != null)
            {
                b.SetActive(false);
            }
        }

        foreach (GameObject f in ghostFog)
        {
            if (f != null)
            {
                f.SetActive(true);
            }
        }
        #endregion

        //Removes enemy as child from any parent, sets body active again, turns off bools
        yield return new WaitForSeconds(cooldown);
        abilityImage.sprite = ghostTransformation.sprite;
        abilityText.text = "Ghost";
        colorChange = ghostTransformation.lightColor;
        StartCoroutine(LerpColor());

        //playerHand.SetActive(false);

        possessedEnemy = false;
        isCooldown = false;
        isGhost = true;
        insideEnemy = false;

        anim.SetBool("isPossessing", false);

        //playerRig.SetActive(true);

        type = ghostTransformation.type;
        PlayerController.Instance.walkSpeed = ghostTransformation.walkSpeed;
        PlayerController.Instance.sprintSpeed = ghostTransformation.sprintSpeed;
        PlayerController.Instance.jumpForce = ghostTransformation.jumpForce;

        possessableEnemy.transform.parent = null;
        possessableEnemy.GetComponent<CapsuleCollider>().isTrigger = false;
        gameObject.layer = 12;

        #region OnLoops
        foreach (GameObject s in spirits)
        {
            if (s != null)
            {
                s.SetActive(true);
            }
        }

        foreach (GameObject w in ghostWalls)
        {
            if (w != null)
            {
                w.SetActive(true);
            }
        }

        foreach (GameObject w in solidWalls)
        {
            if (w != null)
            {
                w.SetActive(false);
            }
        }

        foreach (GameObject i in possessableIndicators)
        {
            if (i != null)
            {
                i.SetActive(true);
            }
        }

        foreach (GameObject i in impassableWalls)
        {
            if (i != null)
            {
                i.SetActive(true);
            }
        }

        foreach (GameObject b in breadcrumbs)
        {
            if (b != null)
            {
                b.SetActive(true);
            }
        }

        foreach (GameObject f in ghostFog)
        {
            if (f != null)
            {
                f.SetActive(false);
            }
        }
        #endregion

        yield return null;
    }
    #endregion

    #region Cosmetic
    IEnumerator LerpColor()
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.
        while (progress < 1)
        {
            mainLight.color = Color.Lerp(mainLight.color, colorChange, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
            
        }
        yield return new WaitForSeconds(2);
        StopCoroutine("LerpColor");
    }
    #endregion
}
