using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpawnPoint : MonoBehaviour
{
    [SerializeField]
    GameObject enemyToSpawn;
    [SerializeField]
    float spawnCoolDown;
    [SerializeField]
    float childNum;
    [SerializeField]
    List<GameObject> spawnPointList;

    // Start is called before the first frame update
    void Start()
    {
        childNum = transform.childCount;
        for(int i=0; i<childNum; i++)
        {
            spawnPointList.Add(transform.GetChild(i).gameObject);
        }
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemy()
    {
        //while (true)
       // {
            if (spawnPointList!= null)
            {
                for(int i=0; i < spawnPointList.Count; i++)
                {
                    Instantiate(enemyToSpawn, spawnPointList[i].transform.position, spawnPointList[i].transform.rotation);
                }
            }
            yield return new WaitForSeconds(spawnCoolDown);
       // }
    }
}
