using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPattern : MonoBehaviour
{
    private float timeBetweenShots;
    private float timeSinceLastShot = 0.0f;
    GameObject bullet;

    float shootingCoolDown = 0.5f;
    bool canShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShots = 1 / (700 / 60.0f);
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (canShoot)
        {
            StartCoroutine(ShootRoutine());
        }
    }
    public void ShootBullet()
    {
        bullet = BulletPool.poolInstance.GetEnemyPooledObject();
        if (timeSinceLastShot >= timeBetweenShots)
        {
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
                EnemyBulletMove bulletMove = bullet.GetComponent<EnemyBulletMove>();
                bulletMove.Initial();
            }
            timeSinceLastShot = 0.0f;
        }
    }
    public void ShootBreakableBullet()
    {
        bullet = BulletPool.poolInstance.GetEnemyBreakablePooledObject();
        if (timeSinceLastShot >= timeBetweenShots)
        {
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
                EnemyBulletMove bulletMove = bullet.GetComponent<EnemyBulletMove>();
                bulletMove.Initial();
            }
            timeSinceLastShot = 0.0f;
        }
    }
    IEnumerator ShootRoutine()
    {
        canShoot = false;
        ShootBullet();
        yield return new WaitForSeconds(shootingCoolDown);// Wait for cooldown
        canShoot = true;// Enable shooting again
    }
}
