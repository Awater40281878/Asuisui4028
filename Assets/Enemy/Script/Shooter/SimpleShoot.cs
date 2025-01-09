using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShoot : MonoBehaviour
{
    GameObject player;
    GameObject bullet;
    private float timeBetweenShots;
    private float timeSinceLastShot = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeBetweenShots = 1 / (150 / 60.0f);
        //StartCoroutine(AimToPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= timeBetweenShots)
        {
            SimpleShootBullet();
        }

    }
    void SimpleShootBullet()
    {
        bullet = BulletPool.poolInstance.GetEnemyPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            EnemyBulletMove bulletMove = bullet.GetComponent<EnemyBulletMove>();
            bulletMove.Initial();
            timeSinceLastShot = 0.0f;
        }
    }
    void SimpleShootBreakableBullet()
    {
        bullet = BulletPool.poolInstance.GetEnemyBreakablePooledObject();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            EnemyBulletMove bulletMove = bullet.GetComponent<EnemyBulletMove>();
            bulletMove.Initial();
        }
    }

}
