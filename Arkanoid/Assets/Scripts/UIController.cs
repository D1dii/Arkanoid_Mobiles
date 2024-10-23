using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class UIController : MonoBehaviour
{

    static public UIController instance;
    public TextMeshProUGUI scoreValue;
    public TextMeshProUGUI highScoreValue;

    
    public BallMovement ball;
    public GameObject lifePrefab;

    public GameObject pauseMenu;

    public GameObject horizontalCanvas;
    public GameObject verticalCanvas;

    public int scoreMultiplier = 1;

    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //if (GameManager.instance.isHorizontal)
        //{
        //    float aspect = 16f / 9f;
        //    Camera.main.aspect = aspect;
        //    Camera.main.ResetAspect();
        //}
        //else if (!GameManager.instance.isHorizontal)
        //{
        //    float aspect = 9f / 16f;
        //    Camera.main.aspect = aspect;
        //    Camera.main.ResetAspect();
        //}
        CheckAspect();
        if (GameManager.instance.lifesList.Count == 0)
        {
            for (int i = 0; i < GameManager.instance.lifes; i++)
            {
                var life = Instantiate(lifePrefab);
                if(GameManager.instance.isHorizontal)
                {
                    life.transform.SetParent(horizontalCanvas.transform, false);
                } 
                else if (!GameManager.instance.isHorizontal)
                {
                    life.transform.SetParent(verticalCanvas.transform, false);
                }
                
                life.transform.position = new Vector3(life.transform.position.x + 20 * i, life.transform.position.y, life.transform.position.z);
                GameManager.instance.lifesList.Add(life);

            }
        }
        else
        {
            
            for (int i = 0; i < GameManager.instance.lifes; i++)
            {
                GameManager.instance.lifesList.RemoveAt(i);
                var life = Instantiate(lifePrefab);
                if (GameManager.instance.isHorizontal)
                {
                    life.transform.SetParent(horizontalCanvas.transform, false);
                }
                else if (!GameManager.instance.isHorizontal)
                {
                    life.transform.SetParent(verticalCanvas.transform, false);
                }
                life.transform.position = new Vector3(life.transform.position.x + 20 * i, life.transform.position.y, life.transform.position.z);
                GameManager.instance.lifesList.Add(life);

            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        scoreValue.text = GameManager.instance.score.ToString();
        highScoreValue.text = GameManager.instance.highScore.ToString();

        if (Input.GetKeyUp(KeyCode.Escape) && GameManager.instance.currentGameState == GameState.inGame)
        {
            PauseGame();
            GameManager.instance.currentGameState = GameState.pause;
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && GameManager.instance.currentGameState == GameState.pause)
        {
            ResumeGame();
            GameManager.instance.currentGameState = GameState.inGame;
        }
    }

    public void AddPoints()
    {
        GameManager.instance.score += 100 * scoreMultiplier;
        if (GameManager.instance.score >= GameManager.instance.highScore)
        {
            GameManager.instance.highScore = GameManager.instance.score;
        }
    }

    public void LoseLife()
    {
        GameManager.instance.lifesList[GameManager.instance.lifesList.Count - 1].gameObject.SetActive(false);
        GameManager.instance.lifesList.RemoveAt(GameManager.instance.lifesList.Count - 1);
        GameManager.instance.lifes--;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void CheckAspect()
    {
        if (Screen.width >= Screen.height)
        {
            horizontalCanvas.SetActive(true);
            verticalCanvas.SetActive(false);
        }
        else if (Screen.width < Screen.height)
        {
            horizontalCanvas.SetActive(false);
            verticalCanvas.SetActive(true);
        }
    }
}
