using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    GunShoot gunPoint1, gunPoint2;
    [SerializeField]
    SpriteRenderer gunPoint1Img, gunPoint2Img;
    private float timeBetweenShots;
    private float timeSinceLastShot = 0.0f;
    [SerializeField]
    AudioSource gunSound;
    bool shooting;
    [SerializeField]
    Image heatBarImage;
    [Header("OverHeat Data")]
    [SerializeField]
    float currentHeat;
    [SerializeField]
    float maxHeat=100f;
    float coolDownRate=0;
    float shootingHeatRate =0;
    float timeSinceLastShooting = 0f;   
    [SerializeField]
    float coolDownDelay = 0.5f;     
    bool isCoolingDown = false;
    Color minHeatColor = new Color(1f, 0.933f, 0.737f); // ����Ȭ�0�ɪ��C�� (#FFEEBC)
    Color maxHeatColor = new Color(1f, 0.2f, 0.2f); // ����Ⱥ��ɪ��C�� (#FF8080)
    private bool isFlashing = false; // �O�_���b�{�{
    private float flashFrequency = 1800f / 60f; // �{�{�W�v�]85 BPM �ഫ���C���ơ^
    private float flashTimer = 0f; // �{�{�p�ɾ�
    private float currentAlpha = 1f; // ��e�z����
    private bool isFadingOut = true; // �Ω󱱨�{�{��V

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShots = 1 / (800 / 60.0f);
        gunPoint1Img.enabled = false;
        gunPoint2Img.enabled = false;
        gunSound = transform.parent.parent.parent.GetComponent<AudioSource>();
        shootingHeatRate = (100f / 6f)*4;
        coolDownRate = 100f;
        currentHeat = 0;
        //playerAim = GetComponent<PlayerAim>();
    }
    public void GetShootInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            shooting = true;
            gunPoint1Img.enabled = true;
            gunPoint2Img.enabled = true;
            gunSound.Play();
            timeSinceLastShooting = 0f;   // ���m�ɶ��p�ƾ�
            isCoolingDown = false;
        }
        if (context.canceled)
        {
            shooting = false;
            gunPoint1Img.enabled = false;
            gunPoint2Img.enabled = false;
            gunSound.Stop();
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastShot += Time.deltaTime;
        if (shooting && timeSinceLastShot >= timeBetweenShots&&currentHeat<maxHeat)
        {
            gunPoint1.Shoot();
            gunPoint2.Shoot();
            timeSinceLastShot = 0.0f;
            ShootingOverHeat();
        }
        else if(shooting&&currentHeat >= maxHeat)
        {
            shooting = false;
            gunPoint1Img.enabled = false;
            gunPoint2Img.enabled = false;
            gunSound.Stop();
        }
        timeSinceLastShooting += Time.deltaTime;
        if (!shooting && !isCoolingDown && timeSinceLastShooting >= coolDownDelay&&currentHeat>0)
        {
            isCoolingDown = true;
            //Debug.Log(timeSinceLastShooting);
        }

        if (isCoolingDown)
        {
            CoolDownShooting();
        }
        isFlashing = (currentHeat / maxHeat) >= 0.8f;
        HandleFlashing();
    }
    private void HandleFlashing()
    {
        if (isFlashing)
        {
            flashTimer += Time.deltaTime;
            if (flashTimer >= (1f / flashFrequency))
            {
                flashTimer = 0f;
                // �վ�z���ץH��{�{�{�ĪG
                if (isFadingOut)
                {
                    currentAlpha -= 0.6f;
                    UpdateUI();
                    if (currentAlpha <= 0f)
                    {
                        currentAlpha = 0f;
                        isFadingOut = false;
                    }
                }
                else
                {
                    currentAlpha += 0.6f;
                    UpdateUI();
                    if (currentAlpha >= 1f)
                    {
                        currentAlpha = 1f;
                        isFadingOut = true;
                    }
                }
                
            }
        }
        else
        {
            // ��_�z���׬� 1�]���{�{�ɡ^
            currentAlpha = 1f;
        }
    }
    private void UpdateUI()
    {
        float heatAmount = currentHeat / maxHeat;
        //Debug.Log(HpAmount);
        heatBarImage.fillAmount = heatAmount;
        Color currentColor = Color.Lerp(minHeatColor, maxHeatColor, heatAmount);
        //Debug.Log(currentColor);
        currentColor.a = currentAlpha;
        heatBarImage.color = currentColor;
    }
    private void ShootingOverHeat()
    {
        if (currentHeat <= maxHeat)
        {
            currentHeat += shootingHeatRate * Time.deltaTime;
            
        }
        UpdateUI();
    }
    private void CoolDownShooting()
    {
        currentHeat -= coolDownRate * Time.deltaTime;
        UpdateUI();
        //currentEnergy = Mathf.Max(currentEnergy, maxEnergy);  

        
        if (currentHeat <= 0)
        {
            isCoolingDown = false;
            currentHeat = 0;
        }
    }
}
