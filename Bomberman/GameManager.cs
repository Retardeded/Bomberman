using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text scoreText;
    public static GameManager Instance = null;
    public static bool gameOver = false;
    public PlayerMovement player1;
    public PlayerMovement player2;

    public Transform player1StartPos;
    public Transform player2StartPos;

    public GameObject[] floors;
    public GameObject[] outerWalls;
    public GameObject[] walls;
    public GameObject reward;
    public int width = 15;
    public int height = 15;

    int startSquareLength = 5;
    int player1Score = 0;
    int player2Score = 0;
    public int chanceForReward = 80;

    public Transform boardHolder;
    // Use this for initialization

    public void RestartTheGame(int playerNumber)
    {
        gameOver = true;

        if (playerNumber == 1)
            player2Score++;
        else
            player1Score++;

        scoreText.text = "SCORE " + player1Score + " : " + player2Score;
        Application.LoadLevel(Application.loadedLevel);
    }

    void Awake ()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
        Destroy(gameObject);
        DontDestroyOnLoad(Instance);

        CreateBoard();
        LoadPlayers();
        gameOver = false;
        scoreText.text = "SCORE " + player1Score + " : " + player2Score;

    }
    public void LoadPlayers()
    {
        Instantiate(player1, player1StartPos.position, Quaternion.identity);
        Instantiate(player2, player2StartPos.position, Quaternion.identity);
    }
    private void CreateBoard()
    {
        boardHolder = new GameObject("Board").transform;

        for (float i = 0; i < height; i++)
        {
            for (float j = 0; j < width; j++)
            {
                if (i == 0f || j == 0f || i == height - 1 || j == width - 1)
                {
                    int randomIndex = Random.Range(0, outerWalls.Length);
                    GameObject instance = Instantiate(outerWalls[randomIndex], new Vector3(i, j, 0f), Quaternion.identity);
                    instance.transform.SetParent(boardHolder);
                }
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    int randomIndex = Random.Range(0, walls.Length);
                    GameObject instance = Instantiate(walls[randomIndex], new Vector3(i, j, 0f), Quaternion.identity);
                    instance.transform.SetParent(boardHolder);
                }
                else
                {
                    int success = Random.Range(0, 100);
                    if (success <= chanceForReward && !((i < startSquareLength && j < startSquareLength) || (i > height - startSquareLength && j > width - startSquareLength)))
                    {
                        GameObject holder = Instantiate(reward, new Vector3(i, j, 0f), Quaternion.identity);
                        holder.transform.SetParent(boardHolder);
                    }
                    int randomIndex = Random.Range(0, floors.Length);
                    GameObject instance = Instantiate(floors[randomIndex], new Vector3(i, j, 0f), Quaternion.identity);
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
