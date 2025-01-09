using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera playerVCam,SceneVCam;
    //[Header("UI")]
    //[SerializeField]
    //Image energyBarImage;
    //[Header("Energy Data")]
    //[SerializeField]
    //float currentEnergy;
    //[SerializeField]
    //float maxEnergy;
    //float energyRegenRate = 50;
    //float timeSinceLastEnergyUse = 0f;   // 距離上次使用能量的時間
    //[SerializeField]
    //float regenDelay = 1f;     // 回復能量的延遲時間 (1秒)
    //bool isRegening = false;  // 是否正在回復能量
    //bool isOutBurst = false;
    [Space(height: 20)]

    [SerializeField]
    float dashForce;
    Rigidbody playerRigidbody;
    [SerializeField]
    float moveHspeed,moveVspeed, maxVelocity, moveHspeedMultiplier, moveVspeedMultiplier;
    [SerializeField]
    float zSpeed,oriZSpeed;
    [SerializeField]
    float leanAngle, lerpTime, lerpMultiplier;
    [SerializeField]
    float dutchMultiplier;

    //Dash
    bool canDash = true;
    bool isDashing = false;
    float dashingTime = 0.2f;
    float dashCooldown = 0.3f;
    bool isRotating = false;
    [SerializeField]
    List<Material> playerMat;
    [SerializeField]
    GameObject body,flyinglean;
    float rotationDuration = 0.5f; // Duration of the rotation in seconds
    float rotateStartTime; // Time when the rotation starts
    float leanStartTime;
    Vector3 initialRotation; // Initial rotation of the object
    Vector2 movementInput;
    float leanInput;
    bool manualLean = true;
    [Header("Effect")]
    [SerializeField]
    GameObject BounceExpolsion;
    [SerializeField]
    ParticleSystem speedLine;
    Vector3 movement;

    //bool isBoosting = false;
    //bool isBraking = false;
    bool isBouncing = false;
    EnemyBulletData bulletData;
    EnemyData enemyData, enemyKeepDisData;

    [SerializeField]
    float goldspeedMulti;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        bulletData = Resources.Load<EnemyBulletData>("BulletData/NormalBullet");
        enemyData = Resources.Load<EnemyData>("EnemyData/EnemyData");
        enemyKeepDisData = Resources.Load<EnemyData>("EnemyData/EnemyData_keepDistance");
        zSpeed = oriZSpeed;
        manualLean = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        //currentEnergy = maxEnergy;
        //UpdateUI();
    }
    public void GetMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>(); 
    }
    public void GetDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canDash/*&&currentEnergy>=20&&!isOutBurst*/)
            {
                rotateStartTime = Time.time;
                StartCoroutine(OnDash());
                //currentEnergy -= 20;
                //UpdateUI();
               // timeSinceLastEnergyUse = 0f;   // 重置時間計數器
                //isRegening = false;
            }
        }
    }
    public void GetBounce(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rotateStartTime = Time.time;
            isBouncing = true;
            isRotating = true;
            StartCoroutine(MuTeKiTime(0.2f));
            //currentEnergy -= 20;
            //UpdateUI();
            //timeSinceLastEnergyUse = 0f;   // 重置時間計數器
            //isRegening = false;
        }
    }
    public void GetLean(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            leanInput = context.ReadValue<float>();
            Debug.Log(leanInput);
            manualLean = true;
        }
        if (context.canceled)
        {
            manualLean = false;
            leanInput = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90*leanInput), lerpTime * Time.deltaTime * lerpMultiplier);
        //transform.Translate(Vector3.forward* zSpeed * Time.deltaTime);
        //Debug.Log(movementInput.x);
        //playerRigidbody.velocity = Vector3.right * moveSpeed;
        //playerRigidbody.velocity = Vector3.ClampMagnitude(playerRigidbody.velocity, maxVelocity);
        //Debug.Log(Vector3.forward * zSpeed * Time.deltaTime);
        // Normal Movement
        if (!isDashing)
        {
            if (movementInput.x>0.2f||movementInput.y>0.2f|| movementInput.x < -0.2f || movementInput.y < -0.2f)
            {
                movement = new Vector3(movementInput.x, movementInput.y, 0);
            }
            else
            {
                movement = Vector3.zero;
            }
            if (manualLean ==false)
            {
                moveHspeedMultiplier = 1f;
                playerRigidbody.velocity = new Vector3(movement.x * moveHspeed, movement.y * moveVspeed, movement.z);
            }
            else
            {
                moveHspeedMultiplier = 0.5f;
                moveVspeedMultiplier = 0.5f;
                playerRigidbody.velocity = new Vector3(movement.x * moveHspeed* moveHspeedMultiplier, movement.y * moveVspeed* moveVspeedMultiplier, movement.z);
            }
            playerRigidbody.velocity = Vector3.ClampMagnitude(playerRigidbody.velocity, maxVelocity);
            // flying lean
            if (playerRigidbody.velocity.x < -0.2f)
            {
                //leanStartTime = Time.time;
                if(manualLean ==false)
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, leanAngle), lerpTime * Time.deltaTime * lerpMultiplier);
                //float t = (Time.time - rotateStartTime) / 2.0f;
                //vCam.m_Lens.Dutch = Mathf.SmoothStep(vCam.m_Lens.Dutch, -10, t);
                playerVCam.m_Lens.Dutch = Mathf.Lerp(playerVCam.m_Lens.Dutch, -10, 0.2f * Time.deltaTime * 8);
                SceneVCam.m_Lens.Dutch = Mathf.Lerp(playerVCam.m_Lens.Dutch, -10, 0.2f * Time.deltaTime * 8);
            }
            else if (playerRigidbody.velocity.x > 0.2f)
            {
                //leanStartTime = Time.time;
                if (manualLean == false)
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -leanAngle), lerpTime * Time.deltaTime * lerpMultiplier);
                //float t = (Time.time - rotateStartTime) / 2.0f;
                //vCam.m_Lens.Dutch = Mathf.SmoothStep(vCam.m_Lens.Dutch, 10, t);
                playerVCam.m_Lens.Dutch = Mathf.Lerp(playerVCam.m_Lens.Dutch, 10, 0.2f * Time.deltaTime * 8);
                SceneVCam.m_Lens.Dutch = Mathf.Lerp(playerVCam.m_Lens.Dutch, 10, 0.2f * Time.deltaTime * 8);

            }
            else if (playerRigidbody.velocity.x == 0 && !isDashing)
            {
                //leanStartTime = Time.time;
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), lerpTime * Time.deltaTime * lerpMultiplier);
                //float t = (Time.time - rotateStartTime) / 2.0f;
                //vCam.m_Lens.Dutch = Mathf.SmoothStep(vCam.m_Lens.Dutch, 0, t);
                playerVCam.m_Lens.Dutch = Mathf.Lerp(playerVCam.m_Lens.Dutch, 0, 0.2f * Time.deltaTime * 8);
                SceneVCam.m_Lens.Dutch = Mathf.Lerp(playerVCam.m_Lens.Dutch, 0, 0.2f * Time.deltaTime * 8);

            }
        }

        //if (currentEnergy<=0)
        //{
        //    regenDelay = 3f;
        //    isOutBurst = true;
        //    //isBraking = false;
        //    //isBoosting = false;
        //}
        //if (currentEnergy <= 20)
        //{
        //    energyBarImage.color = Color.red;
        //}else 
        //{
        //    energyBarImage.color = Color.white;
        //}
        //// 計時器每幀更新
        //timeSinceLastEnergyUse += Time.deltaTime;
        ////Debug.Log(timeSinceLastEnergyUse);
        //// 如果能量不是滿的並且已經超過回復延遲時間，開始回復能量
        //if (!isRegening && currentEnergy < maxEnergy && timeSinceLastEnergyUse >= regenDelay)
        //{
        //    StartEnergyRegen();
        //}

        //// 如果正在回復能量，逐漸增加能量
        //if (isRegening)
        //{
        //    RegenerateEnergy();
        //}
        if (isRotating)
        {
            //initialRotation = transform.rotation.ToEulerAngles();
            transform.rotation = Quaternion.Euler(0, 0, 0);
            float elapsedTime = Time.time - rotateStartTime;
            float angle = Mathf.Lerp(transform.rotation.z, 360f, Mathf.SmoothStep(0f, 1f, elapsedTime / rotationDuration));
            if (leanInput >= 0)
            {
                body.transform.eulerAngles = initialRotation + new Vector3(0f, 0f, angle);
            }
            else
            {
                body.transform.eulerAngles = initialRotation - new Vector3(0f, 0f, angle);
            }

            // Stop rotation after completing one full rotation
            if (elapsedTime >= rotationDuration)
            {
                isRotating = false;
                //playerMat[0].color = Color.white;
                //playerMat[1].color = Color.white;
                //playerMat[2].color = Color.white;
                if (isBouncing)
                {
                    isBouncing = false;
                }
                //body.transform.rotation = transform.rotation;
            }
        }
        //if (isBoosting&&!isBraking)
        //{
        //    if (currentEnergy >= 0)
        //    {
        //        currentEnergy -= 15 * Time.deltaTime;
        //        UpdateUI();
        //        isRegening = false;
        //        timeSinceLastEnergyUse = 0f;
        //        zSpeed = 500f;
        //        enemyKeepDisData.speed = 500f;
        //    }
        //   // Debug.Log("Boost"+bulletData.speed);
        //}
        //if (isBraking&&!isBoosting)
        //{
        //    if (currentEnergy >= 0)
        //    {
        //        currentEnergy -= 30 * Time.deltaTime;
        //        UpdateUI();
        //        isRegening = false;
        //        timeSinceLastEnergyUse = 0f;
        //        zSpeed = 200f;
        //        enemyKeepDisData.speed = 200f;
        //    }
        //    //Debug.Log("Break" + bulletData.speed);
        //}
        //if (!isBoosting && !isBraking)
        //{
        //    bulletData.speed = bulletData.originSpeed;
        //    enemyData.speed = enemyData.originSpeed;
        //    enemyKeepDisData.speed = enemyKeepDisData.originSpeed;
        //    zSpeed = oriZSpeed;
        //}

    }
    IEnumerator OnDash()
    {
        canDash = false;
        isDashing = true;
        isRotating = true;
        if (playerRigidbody.velocity.x != 0)
        {
            playerRigidbody.AddForce(playerRigidbody.velocity * dashForce, ForceMode.Impulse);
        }
        
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        StartCoroutine(StartCountdown());
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    IEnumerator StartCountdown()
    {
        float duration = dashCooldown;
        float totalTime = 0;
        float startTime = Time.time;

        while (totalTime <= duration)
        {
            totalTime = Time.time - startTime;
            float currentValue = totalTime / duration;
            yield return null;
        }
    }
    IEnumerator MuTeKiTime(float mutekiTime)
    {
        //isMuteki = true;
        Physics.IgnoreLayerCollision(8, 6, true);
        playerMat[0].color = Color.green;
        playerMat[1].color = Color.green;
        playerMat[2].color = Color.green;
        yield return new WaitForSeconds(mutekiTime);
        //isMuteki = false;
        Physics.IgnoreLayerCollision(8, 6, false);
        playerMat[0].color = Color.white;
        playerMat[1].color = Color.white;
        playerMat[2].color = Color.white;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyGoldBullet"&&isBouncing)
        {
            Debug.Log("Gold");
            GoldBulletMove gold = other.GetComponent<GoldBulletMove>();
            gold.speed *= goldspeedMulti;
            gold.bounceBack = true;
            Vector3 spawnEffectPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);
            GameObject explosionInstance = Instantiate(BounceExpolsion, transform);
            Destroy(explosionInstance, 1f);
        }
    }
    //private void UpdateUI()
    //{
    //    float EnergyAmount = (float)currentEnergy / (float)maxEnergy;
    //    //Debug.Log(HpAmount);
    //    energyBarImage.fillAmount = EnergyAmount;
    //}
    //private void StartEnergyRegen()
    //{
    //    isRegening = true;
    //}
    //private void RegenerateEnergy()
    //{
    //    currentEnergy += energyRegenRate * Time.deltaTime;
    //    UpdateUI();
    //    currentEnergy = Mathf.Min(currentEnergy, maxEnergy);  // 確保能量不超過最大值

    //    // 如果能量已經回滿，停止回復
    //    if (currentEnergy >= maxEnergy)
    //    {
    //        isRegening = false;
    //        isOutBurst = false;
    //        regenDelay = 1f;
    //    }
    //}
    //public void GetBoost(InputAction.CallbackContext context)
    //{
    //    var emission = speedLine.emission;
    //    if (context.performed&&currentEnergy >=15&&!isOutBurst)
    //    {
    //        currentEnergy -= 5;
    //        UpdateUI();
    //        isRegening = false;
    //        isBoosting = true;

    //        emission.rateOverTime = 500f;
    //    }
    //    if (context.canceled)
    //    {
    //        isBoosting = false;
    //        emission.rateOverTime = 50f;
    //    }
    //}
    //public void GetBrake(InputAction.CallbackContext context)
    //{
    //    var emission = speedLine.emission;
    //    if (context.performed&&currentEnergy >= 15&&!isOutBurst)
    //    {
    //        currentEnergy -= 15;
    //        UpdateUI();
    //        isRegening = false;
    //        isBraking = true;
    //        emission.rateOverTime = 0f;
    //    }
    //    if (context.canceled)
    //    {
    //        isBraking = false;
    //        emission.rateOverTime = 100f;
    //    }
    //}
}
