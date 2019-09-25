using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthFollowCamera : MonoBehaviour
{
    public Camera camera;

    void Start()
    {
        camera = FindObjectOfType<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.down);
    }
}
