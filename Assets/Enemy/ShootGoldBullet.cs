using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGoldBullet : MonoBehaviour
{
    [SerializeField]
    GameObject Goldbullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shootGold());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator shootGold()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(Goldbullet, transform.position, transform.rotation);
    }
}
