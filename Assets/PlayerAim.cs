using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    GameObject aimmingImage1, aimmingImage2, aimmingImage3,lockImage;
    [SerializeField]
    GameObject lockedEnemy;
    [SerializeField]
    GameObject emptyAimObject1, emptyAimObject2, emptyAimObject3;
    Vector2 aimInput;
    Vector3 aimPos;
    [SerializeField]
    float aimSpeed;
    Vector3 direction;
    public GameObject _lockedEnemy => lockedEnemy;
    float lockCountTimer;
    //[SerializeField]
    //float fillDuration;
    //[SerializeField]
    //float scaleDuration;
    //[SerializeField]
    //float aimmedScale;
    bool aimmed = false;
    [SerializeField]
    LayerMask raycastIgnore;
    private void Awake()
    {
        //RaycastIgnore = LayerMask.GetMask("Bullet");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GetAimInput(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (aimInput.x > 0.2f || aimInput.y > 0.2f || aimInput.x < -0.2f || aimInput.y < -0.2f)
        {
            aimPos = new Vector3(aimInput.x, aimInput.y, 0);
        }
        else
        {
            aimPos = Vector3.zero;
        }
        emptyAimObject3.transform.Translate(new Vector3(aimPos.x * aimSpeed , aimPos.y * aimSpeed, 0));
        emptyAimObject2.transform.position = GetPointAtZ(transform.position, emptyAimObject3.transform.position, emptyAimObject2.transform.position.z);
        emptyAimObject1.transform.position = GetPointAtZ(transform.position, emptyAimObject3.transform.position, emptyAimObject1.transform.position.z);
        emptyAimObject3.transform.position = new Vector3(Mathf.Clamp(emptyAimObject3.transform.position.x, 1585, 2415), Mathf.Clamp(emptyAimObject3.transform.position.y, -95f, 135f), emptyAimObject3.transform.position.z);
        aimmingImage1.transform.position = playerCamera.WorldToScreenPoint(emptyAimObject1.transform.position);
        aimmingImage2.transform.position = playerCamera.WorldToScreenPoint(emptyAimObject2.transform.position);
        aimmingImage3.transform.position = playerCamera.WorldToScreenPoint(emptyAimObject3.transform.position);
        direction = emptyAimObject3.transform.position - transform.position;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, direction, out hit, 200f, raycastIgnore))
        {

            if(hit.collider.gameObject.tag == "Enemy")
            {
                Debug.DrawRay(transform.position, emptyAimObject3.transform.position * hit.distance, Color.red);
                lockedEnemy = hit.collider.gameObject;
                lockImage.SetActive(true);
                lockImage.transform.position = playerCamera.WorldToScreenPoint(lockedEnemy.transform.position);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, direction, Color.green);
            lockedEnemy = emptyAimObject3;
            lockImage.SetActive(false);
        }

    }
    Vector3 GetPointAtZ(Vector3 origin, Vector3 end, float point)
    {
        // 計算線段上 Z 坐標為 z 的點的比例
        float t = (point - origin.z) / (end.z - origin.z);

        // 使用線性插值計算該點的 X 和 Y 坐標
        float x = Mathf.Lerp(origin.x, end.x, t);
        float y = Mathf.Lerp(origin.y, end.y, t);

        return new Vector3(x, y, point);
    }
    //IEnumerator FillAndScale()
    //{
    //    aimmingImage.transform.localScale = Vector3.one;
    //    // Fill the image
    //    float elapsedTime = 0f;
    //    while (elapsedTime < fillDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        aimmingImage.fillAmount = Mathf.Clamp01(elapsedTime / fillDuration);
    //        yield return null;
    //    }

    //    // Ensure fill amount is exactly 1
    //    aimmingImage.fillAmount = 1f;

    //    elapsedTime = 0f;
    //    while (elapsedTime < scaleDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float scale = Mathf.Lerp(1f, 1.2f, elapsedTime / (scaleDuration / 2));
    //        aimmingImage.transform.localScale = Vector3.one * scale;
    //        yield return null;
    //    }

    //    elapsedTime = 0f;
    //    while (elapsedTime < scaleDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float scale = Mathf.Lerp(1.2f, 0.9f, elapsedTime / (scaleDuration / 2));
    //        aimmingImage.transform.localScale = Vector3.one * scale;
    //        yield return null;
    //    }

    //    // Ensure the scale is exactly 0.9
    //    aimmingImage.transform.localScale = Vector3.one * 0.9f;
    //    aimmed = true;
    //}
}
