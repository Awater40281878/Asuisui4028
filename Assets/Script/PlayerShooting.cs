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
    Color minHeatColor = new Color(1f, 0.933f, 0.737f); // 熱能值為0時的顏色 (#FFEEBC)
    Color maxHeatColor = new Color(1f, 0.2f, 0.2f); // 熱能值滿時的顏色 (#FF8080)
    private bool isFlashing = false; // 是否正在閃爍
    private float flashFrequency = 1800f / 60f; // 閃爍頻率（85 BPM 轉換為每秒次數）
    private float flashTimer = 0f; // 閃爍計時器
    private float currentAlpha = 1f; // 當前透明度
    private bool isFadingOut = true; // 用於控制閃爍方向

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
            timeSinceLastShooting = 0f;   // 重置時間計數器
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
                // 調整透明度以實現閃爍效果
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
            // 恢復透明度為 1（不閃爍時）
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
