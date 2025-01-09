using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBulletMove : BulletBase
{
    [SerializeField]
    GameObject goldBulletExplosion;
    [SerializeField]
    float lifeTime ;
    [SerializeField]
    float explosionTime;
    MeshRenderer meshRenderer;
    Collider goldCollider;
    GameObject goldEffect;
    
    public bool bounceBack = false;
    [SerializeField]
    bool goback=false;
    ScoreManager scoreManager;
    float timer;
    private bool initialMove = false;
    private bool moveToPlayer = false;
    private void Awake()
    {
        //bulletData = Resources.Load<EnemyBulletData>("BulletData/GoldBullet");
        goldBulletExplosion = Resources.Load<GameObject>("ExplosionNovaFire");
    }
    // Start is called before the first frame update
    void Start()
    {
        moveToPlayer = false;
        StartCoroutine(CountDownInactive(lifeTime));
        moveToPlayer = true;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        //speed = bulletData.speed;
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        timer = 0;
        goldCollider = GetComponent<Collider>();
        goldEffect = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveToPlayer)
        {
            transform.Translate(-Vector3.forward * speed*Time.deltaTime);
            //Debug.Log(-Vector3.forward * speed);
        }
        if (bounceBack)
        {
            goback = true;
            timer += Time.deltaTime;
            if(timer>=1.5f)
            {
                StartCoroutine(GoldBulletEffect());
            }
        }
        //if (speed != bulletData.speed&&!goback)
        //{
        //    speed = bulletData.speed;
        //    //Debug.Log(speed);
        //}
    }
    IEnumerator GoldBulletEffect()
    {
        bounceBack = false;
        speed = 0;
        meshRenderer.enabled = false;
        goldCollider.enabled = false;
        goldEffect.SetActive(false);
        Collider[] destroyList = Physics.OverlapSphere(transform.position,sphereRadius);
        for(int i=0; i < destroyList.Length; i++)
        {
            Debug.Log(i+":"+destroyList[i].tag);
            switch (destroyList[i].tag)
            {
                case "EnemyBullet":
                    destroyList[i].gameObject.SetActive(false);
                    break;
                case "Enemy":
                    enemyHp _enemyHp = destroyList[i].gameObject.GetComponent<enemyHp>();
                    _enemyHp.DeathEffect();
                    scoreManager.AddScore();
                    break;
            }
        }
        GameObject temp = Instantiate(goldBulletExplosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(temp);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bounceBack)
        {
            if(other.tag == "Enemy")
            {
               // Debug.Log("Hit"+ other.name);
               StartCoroutine(GoldBulletEffect());
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
