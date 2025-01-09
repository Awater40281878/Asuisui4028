using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLoopManager : MonoBehaviour
{
    public static TerrainLoopManager terrainInstance;
    List<GameObject> terrainList,blockList,specialList;
    [SerializeField]
    float OriginSpeed;
    [SerializeField]
    float moveSpeed;
    public float _moveSpeed => moveSpeed;
    private void Start()
    {
        terrainInstance = this;
        moveSpeed = OriginSpeed;
    }
    public void SpeedUp()
    {
        moveSpeed = 500;
    }
    public void SlowDown()
    {
        moveSpeed = 60;
    }
    public void Normal()
    {
        moveSpeed = OriginSpeed;
    }
}
