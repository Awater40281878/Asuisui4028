using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    Rigidbody playerRigidbody;
    [SerializeField]
    int maxHp;
    [SerializeField]
    int playerHp;
    [Header("UI")]
    [SerializeField]
    Image HpBar;
    [SerializeField]
    TMP_Text HPText;
    [SerializeField]
    GameObject GameOverUI;
    [SerializeField]
    List<Material> playerMat;
    //[SerializeField]
    //GameObject body;
    //bool isRotating= false;
    bool isMuteki = false;

    //float rotationDuration = 0.5f; // Duration of the rotation in seconds
    //private float startTime; // Time when the rotation starts
    //private Vector3 initialRotation; // Initial rotation of the object

    private void Awake()
    {
        Time.timeScale = 1;
        playerMat[0].color = Color.white;
        playerMat[1].color = Color.white;
        playerMat[2].color = Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHp = maxHp;
        UpdateHpUI();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getHit(int damage)
    {
        playerHp -= damage;
        UpdateHpUI();
        StartCoroutine(MuTeKiTime(0.1f));
        playerMat[0].color = Color.red;
        playerMat[1].color = Color.red;
        playerMat[2].color = Color.red;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            if(!isMuteki)
            getHit(5);
            //other.gameObject.SetActive(false);
        }
        if (other.tag == "Block")
        {
            if (!isMuteki)
                getHit(10);
        }
        if(other.tag == "Enemy")
        {
            if (!isMuteki)
                getHit(10);
            enemyHp _enemyHp = other.GetComponent<enemyHp>();
            _enemyHp.DeathEffect();
        }
    }
    private void UpdateHpUI()
    {
        float HpAmount = (float)playerHp / (float)maxHp;
        //Debug.Log(HpAmount);
        HpBar.fillAmount = HpAmount;
        HPText.text = playerHp.ToString();
        if (playerHp <= 0)
        {
            GameOver();
        }
    }
    void GameOver()
    {

        GameOverUI.SetActive(true);
        Time.timeScale = 0;
    }
    IEnumerator MuTeKiTime(float mutekiTime)
    {
        isMuteki = true;
        //Physics.IgnoreLayerCollision(8, 6, true);
        yield return new WaitForSeconds(mutekiTime);
        isMuteki = false;
        //Physics.IgnoreLayerCollision(8, 6, false);
        playerMat[0].color = Color.white;
        playerMat[1].color = Color.white;
        playerMat[2].color = Color.white;
    }
}
