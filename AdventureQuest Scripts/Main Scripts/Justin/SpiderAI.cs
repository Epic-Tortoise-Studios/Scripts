using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAI : MonoBehaviour
{
    public GameObject spider;
    public float xAngle, yAngle, zAngle;
    public float spinTimer;
    public float minSpin;
    public float maxSpin;

    public bool canRotate;
    public float rotateTimeout = 2f;

    Rigidbody m_Rigidbody;
    Vector3 m_EulerAngleVelocity;

    public GameObject player;
    public Transform playerlocation;

    public Transform enemylocation;
    public float currentDistance;
    public float attackDistance = 6;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        xAngle = 0;
        zAngle = 0;
        spinTimer = Random.Range(minSpin, maxSpin);
        spider = this.gameObject;

        //m_EulerAngleVelocity = new Vector3(0, 180, 0);

        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentDistance = Vector3.Distance(playerlocation.position, enemylocation.position);
        Debug.Log(currentDistance);
        spinTimer -= 1 * Time.deltaTime;
        SpiderRotate();

        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);

        if(currentDistance <= attackDistance)
        {
            Attack();
        }
    }

    void SpiderRotate()
    {
        if (spinTimer <= 0 && canRotate == true)
        {
            m_EulerAngleVelocity = new Vector3(0, 180, 0);
            //spider.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
            spinTimer = Random.Range(minSpin, maxSpin);
            StartCoroutine(rotateTimer());
        }
    }

    private IEnumerator rotateTimer()
    {
        canRotate = false;
        yield return new WaitForSeconds(rotateTimeout);
        canRotate = true;
        spinTimer = Random.Range(minSpin, maxSpin);
    }

    void Attack()
    {

    }
}
