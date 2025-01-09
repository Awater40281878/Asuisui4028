using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : EnemyMove
{

    // Start is called before the first frame update
    void Start()
    {
        enemyData = Resources.Load<EnemyData>("CameraFollow");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speed != enemyData.speed)
        {
            speed = enemyData.speed;
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
