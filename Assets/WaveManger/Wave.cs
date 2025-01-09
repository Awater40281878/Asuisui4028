using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WaveData",menuName = "WaveData/New WaveData")]
public class Wave : ScriptableObject
{
    [SerializeField]
    int waveNumber;
    [SerializeField]
    int maxSubWaveNumber;
    [SerializeField]
    List<EnemyToSpawn> enemyToSpawnList;


    public int _waveNumber => waveNumber;
    public int _maxSubWaveNumber => maxSubWaveNumber;
    public List<EnemyToSpawn> _enemyToSpawnList => enemyToSpawnList;
    //public List<GameObject> _spawnPointList => spawnPointList;
    //public List<GameObject> _endPointList => endPointList;
}
[System.Serializable]
public class EnemyToSpawn
{
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    int amount;
    public GameObject _enemy => enemy;
    public int _amount => amount;
}
