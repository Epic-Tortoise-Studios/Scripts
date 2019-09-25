using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public Transform instantiatePosition;
    public GameObject instantiateEmpty;

    private void OnDestroy()
    {
        Instantiate(instantiateEmpty, instantiatePosition.position, instantiatePosition.rotation);
        //TransformationController.Instance.droppedBody = null;
    }
}
