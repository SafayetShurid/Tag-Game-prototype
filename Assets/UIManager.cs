using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text redScoreText;
    public Text greenScoreText;

    public GameObject gameOverPanel;

    public GameObject roundBPanel;

    public Text gameOverText;

    public Text timerText;

    public static UIManager instance;

    private GameManager gameManager;

    public void Awake()
    {
        instance = this;
    }


    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        redScoreText.text = gameManager.redTeamScore.ToString();
        greenScoreText.text = gameManager.greenTeamScore.ToString();
        timerText.text = ((int)gameManager._time).ToString() +" s";
    }

    public void SetGameOverText(string _text)
    {
        gameOverText.text = _text;
    }
}
