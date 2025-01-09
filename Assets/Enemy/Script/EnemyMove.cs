using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float rotationSpeed;
    [SerializeField]
    protected float radius;
    [SerializeField]
    protected float lifeTime;
    protected float angle;
    protected EnemyData enemyData;
    private void Awake()
    {
        enemyData = Resources.Load<EnemyData>("EnemyData/EnemyData");
    }
    // Start is called before the first frame update
    void Start()
    {
        //CircularMovement();
        StartCoroutine(TimeToDestroy());
    }
    void FixedUpdate()
    {
        if (speed != enemyData.originSpeed)
        {
            speed = enemyData.originSpeed;
        }
        transform.Translate(-Vector3.forward * speed);
        //timer += Time.deltaTime;
        //Debug.Log(timer);
    }
    IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
