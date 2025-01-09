using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShotGun : MonoBehaviour
{
    [SerializeField]
    int bulletAmount;
    GameObject bullet;
    [SerializeField]
    GameObject goldBullet;

    float shootingCoolDown = 1.5f;
    float shootCount;
    bool canShoot = false;
    void Start()
    {
        canShoot = true;
    }
    
    void Update()
    {
        if (canShoot&&shootCount<2)
        {
            StartCoroutine(ShootRoutine());
            shootCount++;
        }
        else if(canShoot && shootCount == 2)
        {
            StartCoroutine(ShootGoldBullet());
            shootCount = 0;
        }
    }
    IEnumerator ShootRoutine()
    {
        canShoot = false;
        ShotGunMode(bulletAmount);
        yield return new WaitForSeconds(shootingCoolDown);// Wait for cooldown
        canShoot = true;// Enable shooting again
    }
    IEnumerator ShootGoldBullet()
    {
        canShoot = false;
        Instantiate(goldBullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(shootingCoolDown);
        canShoot = true;
    }
    public void ShotGunMode(int amount)
    {
        for (int i = 0; i <= amount; i++)
        {
            bullet = BulletPool.poolInstance.GetEnemyBreakablePooledObject();
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);
                EnemyBreakableBulletMove bulletMove = bullet.GetComponent<EnemyBreakableBulletMove>();
                bulletMove.NoMoveInitial();
                bulletMove.Speard();
            }
        }
    }
}
