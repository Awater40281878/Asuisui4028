using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMove : BulletBase
{

    //public float speed;
    [SerializeField]
    private float spreadSpeed;
    //protected Rigidbody bulletRigidbody;

    private bool initialMove = false;
    private bool moveToPlayer = false;
    private Vector3 destination;

    private void Awake()
    {
        //bulletRigidbody = GetComponent<Rigidbody>();
        bulletData = Resources.Load<EnemyBulletData>("BulletData/NormalBullet");
        //Initial();
    }
    public void Initial()
    {
        BulletlifeTime = 10f;
        initialMove = false;
        moveToPlayer = false;
        StartCoroutine(CountDownInactive(BulletlifeTime));
        moveToPlayer = true;
        speed = bulletData.speed;
        hit.gameObject.SetActive(false);
        bulletBody.SetActive(true);
    }
    public void NoMoveInitial()
    {
        initialMove = false;
        moveToPlayer = false;
        StartCoroutine(CountDownInactive(BulletlifeTime));
        speed = bulletData.speed;
    }
    void FixedUpdate()
    {
        if (initialMove)
        {
            var step = spreadSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
            if (Vector3.Distance(transform.position, destination) < 0.001f)
            {
                initialMove = false;
                moveToPlayer = true;
            }
        }
        if (speed != bulletData.speed)
        {
            speed = bulletData.speed;
            // bulletData.DataUpdate = false;
        }
        if (moveToPlayer)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        Debug.Log("Bullet"+ speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            moveToPlayer = false;
            bulletBody.SetActive(false);
            hit.gameObject.SetActive(true);
            hitParticle.Play();
        }
    }
    public void Speard()
    {
        destination = transform.position + new Vector3(Random.Range(-2f,2f), Random.Range(-2f,2f), Random.Range(-2f,2f));
        initialMove = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
