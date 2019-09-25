using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Event", menuName = "Event", order = -100)]
public class EventBehavior : ScriptableObject
{
    public void DestroyObject()
    {
        Debug.Log("Test 02 Event Success!");
        Destroy(ObjectReferences.Instance.testObject);
    }

    public void BuyRedKey()
    {
        if(GameManager.Instance.currentCollectableCount >= 1 && !KeyManager.Instance.hasRed)
        {
            KeyManager.Instance.PickedUpRed();
            GameManager.Instance.SubtractCollectableCount(1);
            AudioManager.instance.PlayClip(GameManager.Instance.positiveAudio);
        }
        else
        {
            AudioManager.instance.PlayClip(GameManager.Instance.negativeAudio);
        }
    }

    public void BuyBlueKey()
    {
        if (GameManager.Instance.currentCollectableCount >= 2 && !KeyManager.Instance.hasBlue)
        {
            KeyManager.Instance.PickedUpBlue();
            GameManager.Instance.SubtractCollectableCount(2);
            AudioManager.instance.PlayClip(GameManager.Instance.positiveAudio);
        }
        else
        {
            AudioManager.instance.PlayClip(GameManager.Instance.negativeAudio);
        }
    }

    public void BuyGreenKey()
    {
        if (GameManager.Instance.currentCollectableCount >= 3 && !KeyManager.Instance.hasGreen)
        {
            KeyManager.Instance.PickedUpGreen();
            GameManager.Instance.SubtractCollectableCount(3);
            AudioManager.instance.PlayClip(GameManager.Instance.positiveAudio);
        }
        else
        {
            AudioManager.instance.PlayClip(GameManager.Instance.negativeAudio);
        }
    }

    public void IntroGhostCutscene()
    {      
        CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().triggerCutscene = true;
    }

    public void CameraCut()
    {
        CutsceneManager.Instance.playerCamera.SetActive(false);
        CutsceneManager.Instance.cutToCamera.SetActive(true);
        CutsceneManager.Instance.cutsceneObject01.SetActive(false);
        //CutsceneManager.Instance.cutsceneObject02.SetActive(true);
        CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject.SetActive(false);
        CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject03.GetComponent<Animator>().SetBool("Fade", false);

        PlayerController.Instance.exclaimed = false;

    }

    public void StopCutscene()
    {
        CutsceneManager.Instance.playerCamera.SetActive(true);
        CutsceneManager.Instance.cutToCamera.SetActive(false);
        CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().animationCamera.SetActive(false);
        PlayerController.Instance.canMove = true;
        GameManager.Instance.CursorLock();
        DialogueManager.Instance.DequeueDialogue();
        GameManager.Instance.bossBattle = true;
    }

    public void CameraReturn()
    {
        CutsceneManager.Instance.playerCamera.SetActive(true);
        CutsceneManager.Instance.cutToCamera02.SetActive(false);
        CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject02.SetActive(false);
        CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject03.SetActive(false);
        CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject04.SetActive(false);
        PlayerController.Instance.canMove = true;
        GameManager.Instance.CursorLock();
    }

    public void ReturnToHub()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Spooky_Mansion_01_Foster")
        {
            GameManager.Instance.currentCollectableCount = 2;
        }
        else if (sceneName == "Spooky_Mansion_02_Foster")
        {
            GameManager.Instance.currentCollectableCount = 3;
        }

        SceneManager.LoadScene("MansionHub");
    }

    public void SkipIntroCutscene()
    {
        CutsceneManager.Instance.playerCamera.SetActive(true);

        if (CutsceneManager.Instance.cutToCamera02.activeInHierarchy == true)
        {
            CutsceneManager.Instance.cutToCamera02.SetActive(false);
        }

        if(CutsceneManager.Instance.cutToCamera.activeInHierarchy == true)
        {
            CutsceneManager.Instance.cutToCamera.SetActive(false);
        }

        if(CutsceneManager.Instance.cutsceneObject01.activeInHierarchy == true)
        {
            CutsceneManager.Instance.cutsceneObject01.SetActive(false);
        }

        if (CutsceneManager.Instance.cutsceneObject02.activeInHierarchy == true)
        {
            CutsceneManager.Instance.cutsceneObject02.SetActive(false);
        }

        if (CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject.activeInHierarchy == true)
        {
            CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject.SetActive(false);
        }

        if (CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject02.activeInHierarchy == true)
        {
            CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject02.SetActive(false);
        }

        if (CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject03.activeInHierarchy == true)
        {
            CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject03.SetActive(false);
        }

        if (CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject04.activeInHierarchy == true)
        {
            CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().optionalObject04.SetActive(false);
        }

        PlayerController.Instance.canMove = true;
        GameManager.Instance.CursorLock();
    }
}
