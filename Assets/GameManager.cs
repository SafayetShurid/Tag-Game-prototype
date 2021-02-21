using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int redTeamScore = 0;
    public int greenTeamScore = 0;


    public GameObject[] players;
    public int currentPlayerIndex = 0;

    public GameObject currentPlayer;
    

    public static GameManager instance;
    public void Awake()
    {
        instance = this;
        currentPlayer = players[0];
    }


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RedScoreUp()
    {
        redTeamScore++;
    }

    public void GreenScoreUp()
    {
        greenTeamScore++;
    }

    public void SetNextPlayer()
    {
        if(currentPlayerIndex<players.Length-1)
        {
            currentPlayerIndex++;
            currentPlayer = players[currentPlayerIndex];
            currentPlayer.AddComponent<Player>();
        }
        else
        {
            if(redTeamScore>greenTeamScore)
            {
                UIManager.instance.SetGameOverText("Red Team Wins");
            }
            else if(greenTeamScore>redTeamScore)
            {
                UIManager.instance.SetGameOverText("Green Team Wins");
            }
            else
            {
                UIManager.instance.SetGameOverText("Game Tie");
            }
            UIManager.instance.gameOverPanel.SetActive(true);
        }
       

    }






}
