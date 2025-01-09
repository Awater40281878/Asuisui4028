using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> enemyList;
    [SerializeField]
    [Space(10)]
    List<GameObject> _12SpawnPointList, _6SpawnPointList, _3SpawnPointList, _3VerticalSpawnPointList;
    [SerializeField]
    [Space(10)]
    List<GameObject> _12EndPointList, _6EndPointList, _3EndPointList, _3VerticalEndPointList;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemy(GameObject enemy,GameObject spawnPoint, GameObject endPoint)
    {
            GameObject temp = Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
            EnemyMoveA move = temp.GetComponent<EnemyMoveA>();
            move.endPoint = endPoint;
    }
    IEnumerator WaveSpawn()
    {
        yield return new WaitForSeconds(3f);
        SpawnEnemy(enemyList[0], _6SpawnPointList[0], _6EndPointList[0]);
        SpawnEnemy(enemyList[1], _6SpawnPointList[1], _6EndPointList[1]);
        SpawnEnemy(enemyList[0], _6SpawnPointList[2], _6EndPointList[2]);
        SpawnEnemy(enemyList[0], _6SpawnPointList[3], _6EndPointList[3]);
        SpawnEnemy(enemyList[1], _6SpawnPointList[4], _6EndPointList[4]);
        SpawnEnemy(enemyList[0], _6SpawnPointList[5], _6EndPointList[5]);
        yield return new WaitForSeconds(12f);
        SpawnEnemy(enemyList[0], _6SpawnPointList[0], _6EndPointList[0]);
        SpawnEnemy(enemyList[0], _6SpawnPointList[1], _6EndPointList[1]);
        SpawnEnemy(enemyList[0], _6SpawnPointList[2], _6EndPointList[2]);
        SpawnEnemy(enemyList[1], _6SpawnPointList[4], _6EndPointList[4]);
        yield return new WaitForSeconds(12f);
        ///右邊垂直
        SpawnEnemy(enemyList[0], _3VerticalSpawnPointList[3], _3VerticalEndPointList[6]);
        SpawnEnemy(enemyList[1], _3VerticalSpawnPointList[4], _3VerticalEndPointList[7]);
        SpawnEnemy(enemyList[0], _3VerticalSpawnPointList[5], _3VerticalEndPointList[8]);
        yield return new WaitForSeconds(12f);
        SpawnEnemy(enemyList[0], _6SpawnPointList[0], _6EndPointList[0]);
        SpawnEnemy(enemyList[1], _6SpawnPointList[1], _6EndPointList[1]);
        SpawnEnemy(enemyList[0], _6SpawnPointList[2], _6EndPointList[2]);
        SpawnEnemy(enemyList[0], _6SpawnPointList[3], _6EndPointList[3]);
        SpawnEnemy(enemyList[1], _6SpawnPointList[4], _6EndPointList[4]);
        SpawnEnemy(enemyList[0], _6SpawnPointList[5], _6EndPointList[5]);
        yield return new WaitForSeconds(12f);
        SpawnEnemy(enemyList[1], _3VerticalSpawnPointList[4], _3VerticalEndPointList[7]);
        SpawnEnemy(enemyList[1], _3VerticalSpawnPointList[1], _3VerticalEndPointList[1]);
        yield return new WaitForSeconds(12f);
        SpawnEnemy(enemyList[0], _3VerticalSpawnPointList[3], _3VerticalEndPointList[3]);
        SpawnEnemy(enemyList[1], _3VerticalSpawnPointList[4], _3VerticalEndPointList[4]);    ///中間垂直
        SpawnEnemy(enemyList[0], _3VerticalSpawnPointList[5], _3VerticalEndPointList[5]);
        yield return new WaitForSeconds(12f);
        ///左邊垂直
        SpawnEnemy(enemyList[0], _3VerticalSpawnPointList[0], _3VerticalEndPointList[0]);
        SpawnEnemy(enemyList[1], _3VerticalSpawnPointList[1], _3VerticalEndPointList[1]);
        SpawnEnemy(enemyList[0], _3VerticalSpawnPointList[2], _3VerticalEndPointList[2]);
        yield return new WaitForSeconds(12f);
        SpawnEnemy(enemyList[1], _6SpawnPointList[0], _6EndPointList[0]);
        SpawnEnemy(enemyList[0], _6SpawnPointList[1], _6EndPointList[1]);
        SpawnEnemy(enemyList[1], _6SpawnPointList[2], _6EndPointList[2]);
        SpawnEnemy(enemyList[1], _6SpawnPointList[3], _6EndPointList[3]);
        SpawnEnemy(enemyList[0], _6SpawnPointList[4], _6EndPointList[4]);
        SpawnEnemy(enemyList[1], _6SpawnPointList[5], _6EndPointList[5]);
    }
}
