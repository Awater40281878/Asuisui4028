using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testEnemy : MonoBehaviour
{
    [SerializeField]
    List<GameObject> enemy;
    [SerializeField]
    List<GameObject> spawnPoint;

    private void Awake()
    {

    }
    public void GetTestSpawn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SpawnEnemy();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyLoop());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    IEnumerator SpawnEnemyLoop()
    {
        while (true)
        {
            int temp1 = Random.Range(2, 5);
            for (int i = 0; i <= temp1; i++)
            {
                int temp = Random.Range(0, 10);
                int temp2 = Random.Range(0, 2);
                Instantiate(enemy[temp2], spawnPoint[temp].transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(4f);
        }
    }
    void SpawnEnemy()
    {
        Random.Range(-10, 10);
        Vector3 pos = new Vector3(Random.Range(-10, 10),Random.Range(-10, 10),100);
        Instantiate(enemy[0], pos, transform.rotation);
    }
}
