using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoop : TerrainLoopManager
{
    [SerializeField]
    GameObject startPoint,endpoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(-Vector3.forward * TerrainLoopManager.terrainInstance._moveSpeed*Time.deltaTime);
        //Debug.Log(TerrainLoopManager.terrainInstance._moveSpeed);
        if (transform.localPosition.z<-195f)
        {
            //Debug.Log(transform.position.z);
            transform.position = startPoint.transform.position;
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Finish")
    //    {
    //        transform.position = startPoint.transform.position;
    //    }
    //}
}
