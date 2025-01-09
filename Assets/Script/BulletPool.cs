using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    
    public static BulletPool poolInstance;

    [Header("PlayerBulletPool")]
    [SerializeField]
    GameObject playerObjectToPool;
    [SerializeField]
    GameObject playerBulletPoolParent;
    [SerializeField]
    public int playerAmountToPool;
    List<GameObject> playerBulletPool;

    [Header("enemyBulletPool")]
    [SerializeField]
    GameObject enemyObjectToPool,enemyBreakableObject;
    [SerializeField]
    GameObject enemyBulletPoolParent, enemyBreakableAmountParent;
    [SerializeField]
    int enemyAmountToPool, enemyBreakableAmountToPool;
    [SerializeField]
    List<GameObject> enemyBulletPool,EnemyBulletBreakablePool;

    //public List<GameObject> enemyActivePooledObject = new List<GameObject>();
    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private void Awake()
    {
        poolInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("hi");
        enemyBulletPool = new List<GameObject>();
        playerBulletPool = new List<GameObject>();
        EnemyBulletBreakablePool = new List<GameObject>();
        GameObject tmp,tmp1;
        for (int i = 0; i < enemyAmountToPool; i++)
        {
            tmp = Instantiate(enemyObjectToPool);
            tmp.SetActive(false);
            tmp.transform.SetParent(enemyBulletPoolParent.transform);
            enemyBulletPool.Add(tmp);
        }
        for (int i = 0; i < enemyBreakableAmountToPool; i++)
        {
            tmp = Instantiate(enemyBreakableObject);
            tmp.SetActive(false);
            tmp.transform.SetParent(enemyBreakableAmountParent.transform);
            EnemyBulletBreakablePool.Add(tmp);
        }
        for (int i = 0; i < playerAmountToPool; i++)
        {
            tmp1 = Instantiate(playerObjectToPool);
            tmp1.SetActive(false);
            tmp1.transform.SetParent(playerBulletPoolParent.transform);
            playerBulletPool.Add(tmp1);
        }
    }
    public GameObject GetEnemyPooledObject()
    {
        for (int i = 0; i < enemyAmountToPool; i++)
        {
            if (!enemyBulletPool[i].activeInHierarchy)
            {
                return enemyBulletPool[i];
            }
        }
        return null;
    }

    public GameObject GetEnemyBreakablePooledObject()
    {
        for (int i = 0; i < enemyBreakableAmountToPool; i++)
        {
            if (!EnemyBulletBreakablePool[i].activeInHierarchy)
            {
                return EnemyBulletBreakablePool[i];
            }
        }
        return null;
    }
    public GameObject GetPlayerPooledObject()
    {
        for (int i = 0; i < playerAmountToPool; i++)
        {
            if (!playerBulletPool[i].activeInHierarchy)
            {
                return playerBulletPool[i];
            }
        }
        return null;
    }
}
