using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    #region Sigleton
    private static CameraController instance;
    public static CameraController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CameraController>();
            return instance;
        }
    }
    #endregion

    public Transform colliding;
    public Transform target;
    public Transform pivot;

    public float rotateSpeed;
    public float maxViewAngle;
    public float minViewAngle;

    public bool useOffsetValues;
    public bool lockCursor = true;
    public bool invertY;

    public Vector3 offset;

    private float originalDistance;
    private float currentDistance;
    private float targetDistance;
    private float currentVelocity;

    public LayerMask layerMask;

    private GameObject linecastHit;

    //public GameObject fadeGO;
    private Image fadeImage;

    private void Start()
    {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        //Sets the Pivots position to be at the Target
        pivot.transform.position = target.transform.position;

        //Sets Pivot to be the child of the Target
        pivot.transform.parent = null;

        originalDistance = this.gameObject.transform.localPosition.magnitude;
        currentDistance = originalDistance;

        /*fadeGO.SetActive(true);
        fadeImage = fadeGO.GetComponent<Image>();
        fadeImage.canvasRenderer.SetAlpha(0.0f);*/
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.cursorLocked)
        {
            CameraControl();
        }

        OnDrawGizmos();
    }

    public void CameraControl()
    {
            pivot.transform.position = target.transform.position;

            //Get X Position of the mouse and rotate the target
            float horizontal = Input.GetAxisRaw("Mouse X") * rotateSpeed;
            pivot.Rotate(0, horizontal, 0, Space.World);

            //Get Y Position of the mouse and rotate the pivot
            float vertical = Input.GetAxisRaw("Mouse Y") * rotateSpeed;
            pivot.Rotate(-vertical, 0, 0, Space.Self);

            //Limit the up/down camera rotation
            if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180)
            {
                pivot.rotation = Quaternion.Euler(maxViewAngle, transform.rotation.eulerAngles.y, 0);
            }

            if (pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 360 + minViewAngle)
            {
                pivot.rotation = Quaternion.Euler(360 + minViewAngle, transform.rotation.eulerAngles.y, 0);
            }


            //Move camera based on the current rotation of the target and the original offset
            float desiredYAngle = pivot.eulerAngles.y;
            float desiredXAngle = pivot.eulerAngles.x;

            Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);

            transform.position = target.position - (rotation * offset);

            if (transform.position.y < target.position.y)
            {
                transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);
            }

            transform.LookAt(target);
            RaycastHit hit;
            if (Physics.Linecast(this.transform.position, target.position, out hit, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    linecastHit = hit.collider.gameObject;
                    Debug.Log("Linecast Hitting: " + linecastHit.name);
                }
                else
                {
                    this.transform.position = colliding.position;
                    linecastHit = hit.collider.gameObject;
                    Debug.Log("Linecast Hitting: " + linecastHit.name);
                }
            }
        
    }

    /*public void FadeIn()
    {
        fadeImage.CrossFadeAlpha(1, 1, false);
    }

    public void FadeOut()
    {
        fadeImage.CrossFadeAlpha(0, 1, false);
    }
    */

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(this.transform.position, target.position);
    }
}


















   


