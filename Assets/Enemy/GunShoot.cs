using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField]
    PlayerAim playerAim;
    GameObject lockedEnemy;
    PlayerBulletMove bulletMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Shoot()
    {
        GameObject bullet = BulletPool.poolInstance.GetPlayerPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            if (playerAim._lockedEnemy != null)
            {
                bulletMove = bullet.GetComponent<PlayerBulletMove>();
                bulletMove.SetLockedEnemy(playerAim._lockedEnemy);
            }         
            bullet.SetActive(true);
        }
    }
}
