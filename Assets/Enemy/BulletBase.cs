using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] protected GameObject hit;
    [SerializeField] protected ParticleSystem hitParticle;
    [SerializeField] protected GameObject bulletBody;
    [SerializeField] protected GameObject muzzleFlash;
    //[SerializeField] protected Rigidbody bulletRigidbody;
    //[SerializeField] protected Collider BulletCollider;
    //[SerializeField] protected Light lightSource;


    protected EnemyBulletData bulletData;
    [SerializeField]protected float BulletlifeTime;
    public float speed;
    [Header("Gizmo")]
    [SerializeField]
    protected float sphereRadius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected IEnumerator CountDownInactive(float lifeTime)
    {
        //Debug.Log(gameObject.name+":"+ lifeTime);
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
