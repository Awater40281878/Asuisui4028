using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForward : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject PlayerCam;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(PlayerCam.transform.position.x - 2000, PlayerCam.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = PlayerCam.transform.rotation;
        Vector3 movement = Vector3.forward.normalized * speed * Time.deltaTime;
        transform.position += movement;
    }
}
