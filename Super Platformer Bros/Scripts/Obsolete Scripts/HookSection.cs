using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSection : MonoBehaviour
{
    public GameObject hookTrigger;
    public GameObject hook1Camera;
    public GameObject hook1Canvas;

    public GameObject hook2Camera;
    public GameObject hook2Canvas;

    public GameObject hook3Camera;
    public GameObject hook3Canvas;

    public GameObject hook4Camera;
    public GameObject hook4Canvas;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (hookTrigger.name == "Hook1_Trigger")
            {
                hook1Camera.SetActive(true);
                hook1Canvas.SetActive(true);

                hook2Camera.SetActive(false);
                hook2Canvas.SetActive(false);

                hook3Camera.SetActive(false);
                hook3Canvas.SetActive(false);

                hook4Camera.SetActive(false);
                hook4Canvas.SetActive(false);

            }
            else if (hookTrigger.name == "Hook2_Trigger")
            {
                hook1Camera.SetActive(false);
                hook1Canvas.SetActive(false);

                hook2Camera.SetActive(true);
                hook2Canvas.SetActive(true);

                hook3Camera.SetActive(false);
                hook3Canvas.SetActive(false);

                hook4Camera.SetActive(false);
                hook4Canvas.SetActive(false);

            }
            else if (hookTrigger.name == "Hook3_Trigger")
            {
                hook1Camera.SetActive(false);
                hook1Canvas.SetActive(false);

                hook2Camera.SetActive(false);
                hook2Canvas.SetActive(false);

                hook3Camera.SetActive(true);
                hook3Canvas.SetActive(true);

                hook4Camera.SetActive(false);
                hook4Canvas.SetActive(false);

            }
            else if (hookTrigger.name == "Hook4_Trigger")
            {
                hook1Camera.SetActive(false);
                hook1Canvas.SetActive(false);

                hook2Camera.SetActive(false);
                hook2Canvas.SetActive(false);

                hook3Camera.SetActive(false);
                hook3Canvas.SetActive(false);

                hook4Camera.SetActive(true);
                hook4Canvas.SetActive(true);

            }
        }
    }
}
