using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreTxt, timerTxt;
    float score;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        timerTxt.text =  string.Format("{0:F1}", timer);
    }
    public void AddScore()
    {
        score += 10;
        UpdateScore();
    }
    public void UpdateScore()
    {
        scoreTxt.text =  score.ToString();
    }
}
