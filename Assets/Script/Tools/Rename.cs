using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//[ExecuteInEditMode]
public class Rename : MonoBehaviour
{
    public List<GameObject> renameList;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < renameList.Count; i++)
        {
            //renameList[i].name = (i + 1).ToString();
            renameList[i].transform.localPosition = new Vector3(0,0,i * 200f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
