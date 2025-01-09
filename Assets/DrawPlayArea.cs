using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPlayArea : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(transform.position.x,transform.position.y+4.5f,transform.position.z+500), new Vector3(160,24,1000));
    }
}
