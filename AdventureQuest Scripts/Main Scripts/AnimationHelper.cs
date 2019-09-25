using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    [Header("Animation Type")]
    public bool cutscene;
    public bool unlock;

    [Header("Animation Variables")]
    public GameObject animatorObject;
    public GameObject animatorTrigger;
    private Animator anim;

    public AnimatorControllerParameterType parameterType;
    public string parameterName;
    public bool playerIsTrigger;
    private bool animated = false;

    [Header("Optional")]
    public bool useAudio;
    public bool useDialogue;
    public string tagName;
    public GameObject animationCamera;
    public GameObject playerCamera;
    public GameObject optionalObject;
    public GameObject optionalObject02;
    public GameObject optionalObject03;
    public GameObject optionalObject04;
    public DialogueBase dialogue;
    public DialogueBase dialogue02;
    public DialogueBase dialogue03;
    public AudioClip triggerNoise;
    public AudioClip music;

    //For Intro Cutscene
    [HideInInspector]
    public bool triggerCutscene;
    private bool triggeredCutscene;
    private bool skip;
    

    void Start()
    {
        if(animatorObject != null)
        {
            anim = animatorObject.GetComponent<Animator>();
        }

        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        
    }


    void Update()
    {
        if(triggerCutscene && !triggeredCutscene)
        {
            StartCoroutine(IntroGhost());
            triggeredCutscene = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(playerIsTrigger)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (unlock)
                {
                    UnlockAnimation();
                }
                else if (cutscene)
                {
                    CutsceneAnimation();
                }
            }
        }
        else
        {
            if (other.gameObject.CompareTag(tagName))
            {
                if (unlock)
                {
                    UnlockAnimation();
                }
            }
        }
    }

    public void UnlockAnimation()
    {
        if (parameterType == AnimatorControllerParameterType.Bool && !animated)
        {
            anim.SetBool(parameterName, true);
            animated = true;

            if (useAudio)
            {
                AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
                AudioManager.instance.PlayClip(triggerNoise);
            }
        }

    }

    public void CutsceneAnimation()
    {
        if (!animated)
        {
            StartCoroutine(BeginEncounter());
            animated = true;
        }
    }


    #region Intro Cutscene Coroutines
    IEnumerator BeginEncounter()
    {
        if (!skip)
        {
            GameManager.Instance.CursorUnlock();
            PlayerController.Instance.playerExclaim.SetActive(true);
            AudioManager.instance.PlayClip(AudioManager.instance.exclaim);
            DialogueManager.Instance.TriggerDialogue(dialogue);
            PlayerController.Instance.canMove = false;
            yield return new WaitForSeconds(2);
            PlayerController.Instance.playerExclaim.SetActive(false);
        }
        else
        {

        }

        yield return null;
    }

    public IEnumerator IntroGhost()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0, 2.45f, -75);
        playerCamera.SetActive(false);
        animationCamera.SetActive(true);
        anim.SetBool(parameterName, true);

        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.PlayClip(music);

        yield return new WaitForSeconds(11.4f);
        optionalObject03.SetActive(true);
        DialogueManager.Instance.TriggerDialogue(dialogue02);

        yield return null;
    }

    public IEnumerator EndEncounter()
    {
        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.StopClip();
        optionalObject02.SetActive(true);
        playerCamera.SetActive(false);
        CutsceneManager.Instance.cutToCamera02.SetActive(true);
        PlayerController.Instance.canMove = false;
        GameManager.Instance.CursorUnlock();
        DialogueManager.Instance.TriggerDialogue(dialogue03);
        PlayerController.Instance.exclaimed = false;
        yield return null;
    }
    #endregion

    #region Level Cutscenes
    public IEnumerator SimpleCutscene()
    {
        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.PlayClip(triggerNoise);
        GameManager.Instance.CursorUnlock();
        PlayerController.Instance.canMove = false;
        PlayerController.Instance.exclaimed = false;
        playerCamera.SetActive(false);
        animationCamera.SetActive(true);
        optionalObject.SetActive(true);
        DialogueManager.Instance.TriggerDialogue(dialogue);
        yield return null;
    }
    #endregion

    public void SkipButton()
    {
        CutsceneManager.Instance.playerCamera.SetActive(true);

        if (CutsceneManager.Instance.cutToCamera02.activeInHierarchy == true)
        {
            CutsceneManager.Instance.cutToCamera02.SetActive(false);
        }

        if (CutsceneManager.Instance.cutToCamera.activeInHierarchy == true)
        {
            CutsceneManager.Instance.cutToCamera.SetActive(false);
        }

        PlayerController.Instance.canMove = true;
        GameManager.Instance.CursorLock();
    }
}
