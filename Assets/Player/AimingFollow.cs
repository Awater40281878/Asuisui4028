using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingFollow : MonoBehaviour
{
    [SerializeField]
    Camera MainCam;
    [SerializeField]
    Vector3 playerPos;
    Vector3 AimingUIPos;
    // Start is called before the first frame update
    void Start()
    {
        MainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = MainCam.WorldToScreenPoint(playerPos);
    }
}
