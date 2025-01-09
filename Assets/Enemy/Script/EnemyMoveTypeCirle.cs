using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveTypeCirle : MonoBehaviour
{
    Vector3 circularMotion;
    float speed;
    [SerializeField]
    float angle, radius,rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculateMovement();
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        transform.position += circularMotion * Time.deltaTime;
    }
    void CircularMovement()
    {
        CalculateMovement();
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        transform.position += circularMotion * Time.deltaTime;
    }
    void CalculateMovement()
    {
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
        circularMotion = new Vector3(x, y, 0);
        angle += rotationSpeed * Time.deltaTime;
        if (angle >= 360f)
        {
            angle -= 360f;
        }
    }
}
