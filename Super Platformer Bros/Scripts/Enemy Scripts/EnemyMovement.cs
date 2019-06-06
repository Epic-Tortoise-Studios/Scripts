using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    /*public float minimum = -1.0F;
    public float maximum = 1.0F;

    static float t = 0.0f;
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(minimum, maximum, t), 0, 0);
        t += 0.5f * Time.deltaTime;

        if (t > 1.0f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
        }

    }*/

    private bool dirRight = true;
    public float speed;
    public float movingTimer;

    public void Update()
    {
        if (dirRight == true)
        {
           StartCoroutine(MoveRight());
        }
        else if (dirRight == false)
        {
            StartCoroutine(MoveLeft());
        }
    }

    IEnumerator MoveRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        yield return new WaitForSeconds(movingTimer);
        dirRight = false;
    }

    IEnumerator MoveLeft()
    {
        transform.Translate(-Vector2.right * speed * Time.deltaTime);
        yield return new WaitForSeconds(movingTimer);
        dirRight = true;
    }
}
