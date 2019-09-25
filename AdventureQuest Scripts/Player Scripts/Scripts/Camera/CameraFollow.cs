using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    /*public Vector3 offset1;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float pitch = 2f;

    public float yawSpeed = 100f;

    public float currentZoom = 10f;
    public float currentYaw = 0f;*/

    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    /*void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        //currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }*/

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    /*void LateUpdate()
    {
        transform.position = target.position - offset1 * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }*/
}
