using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int redTeamScore = 0;
    public int greenTeamScore = 0;


    public GameObject[] players;
    public int currentPlayerIndex = 0;
    private Vector3[] startingIndexes = new Vector3[3];

    public GameObject currentPlayer;

    private bool roundFinished;
    

    public static GameManager instance;

   
    public GameObject[] enemies;

    public float _time = 60;

    public void Awake()
    {
        instance = this;
        currentPlayer = players[0];
    }


    void Start()
    {
        int i = 0;
       foreach(var v in players)
        {
            startingIndexes[i] = v.transform.localPosition;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _time -= Time.deltaTime;
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
            if(roundFinished)
            {
                currentPlayer.GetComponent<Player>().playerType = Player.PlayerType.RED;
            }
        }
        else
        {
            /*if(redTeamScore>greenTeamScore)
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
            }*/
            // UIManager.instance.SetGameOverText("Game Over");
            //  UIManager.instance.gameOverPanel.SetActive(true);

            UIManager.instance.roundBPanel.SetActive(true);
            RoundFinished();
           
        }
       

    }

    public void RoundFinished()
    {

        StartCoroutine(RoundFinishRoutine());
    }

    IEnumerator RoundFinishRoutine()
    {
        yield return new WaitForSeconds(1f);
        int i = 0;
        roundFinished = true;
        foreach (var v in players)
        {
            v.GetComponent<SpriteRenderer>().color = new Color32(255, 90, 90, 255);
            v.transform.localPosition = startingIndexes[i];
            if (v.GetComponent<Player>() != null)
            {
                Destroy(v.GetComponent<Player>());
            }
            i++;
        }

        foreach (var v in enemies)
        {
            v.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }

        players[0].AddComponent<Player>();
        players[0].GetComponent<Player>().playerType = Player.PlayerType.RED;
        currentPlayerIndex = 0;
        _time = 60f;
    }






}
