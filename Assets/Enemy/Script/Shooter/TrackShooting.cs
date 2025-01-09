using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackShooting : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    GameObject bullet;
    private float timeBetweenShots;
    private float timeSinceLastShot = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeBetweenShots = 1 / (200 / 60.0f);
        //StartCoroutine(AimToPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= timeBetweenShots)
        {
            AimPlayer();
            ShootBullet();
        }

    }
    void ShootBullet()
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
    IEnumerator AimToPlayer()
    {
        while (true)
        {
            transform.LookAt(player.transform);
            Debug.Log("Update :" + player.transform.position);
            yield return new WaitForSeconds(0.1f);
        }
    }
    void AimPlayer()
    {
        transform.LookAt(player.transform);
    }
}
