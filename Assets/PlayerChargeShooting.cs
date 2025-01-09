using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerChargeShooting : MonoBehaviour
{
    float chargeShootingCounter;
    [SerializeField]
    float maxChargeTime;
    [SerializeField]
    GameObject ChargeBullet;
    bool chargeShooting;
    bool manualLean = false;
    // Start is called before the first frame update
    void Start()
    {
        chargeShootingCounter = 0;
    }
    public void GetChargeShootInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            chargeShooting = true;

        }
        if (context.canceled)
        {
            chargeShooting = false;
        }
    }
    public void GetAimLean(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            manualLean = true;
        }
        if (context.canceled)
        {
            manualLean = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (chargeShooting && chargeShootingCounter < maxChargeTime)
        {
            chargeShootingCounter += Time.deltaTime;
            //Debug.Log(chargeShootingCounter);
        }
        else if (chargeShooting == false && chargeShootingCounter >= maxChargeTime)
        {
            if (manualLean)
            {
                Instantiate(ChargeBullet, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(ChargeBullet, transform.position, Quaternion.identity);
            }
            
            chargeShootingCounter = 0;
        }
    }

}
