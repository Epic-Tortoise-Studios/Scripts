using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatText : MonoBehaviour
{

    public GameObject mainTarget;
    public GameObject camera1;
    public int damage;

    public float fadeTime;
    public float yPos;

    public GUIStyle textStyle = new GUIStyle();

    public float newAlpha;

    public float randomX;
    public float randomY;

    public float fadeOutTimer;

    public Vector2 targetPos;


    void Start()
    {
        targetPos = Camera.main.WorldToScreenPoint(mainTarget.transform.position);
        camera1 = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void onGUI ()
    {
        if(mainTarget != null)
        {
            GUI.Label(new Rect(targetPos.x - 100, Screen.height - targetPos.y - 75 - yPos, 120, 30), "" + damage, textStyle);
        }
    }
    
    void Update()
    {
        if(mainTarget != null)
        {
            targetPos = Camera.main.WorldToScreenPoint(mainTarget.transform.position);
        }

        if (fadeTime > 0)
        {
            if(fadeOutTimer > 0)
            {
                fadeOutTimer -= Time.deltaTime;
            }
            else
            {
                newAlpha -= 0.05f;
                textStyle.normal.textColor = new Vector4(1, 1, 1, newAlpha);
            }

            fadeTime -= Time.deltaTime;
            yPos += 1.0f;
            if(textStyle.fontSize < 20)
            {
                textStyle.fontSize += 1;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
