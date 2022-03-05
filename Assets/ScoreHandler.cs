using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreHandler : MonoBehaviour
{
    public float currentScore = 0;
    public int gameDuration = 90;
    bool gameStarted = false;
    GameObject scoreText, timerText, pointDisplayText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreDisplay");
        timerText = GameObject.Find("TimerDisplay");
        pointDisplayText = (GameObject)Resources.Load("SimpleScore", typeof(GameObject));
    }

    public void DisplayMessage(string msg)
    {
        var scoreObj = Instantiate(pointDisplayText, new Vector3(0, 0, 0), Quaternion.identity);
        scoreObj.GetComponent<PointsTextHandler>().displayText = msg;
    }

    public void AddScore(float amount)
    {

        DisplayMessage(amount + " Points!");

        if (!gameStarted) { return; }

        currentScore += amount;
        scoreText.SendMessage("ChangeText", currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.SendMessage("ChangeText", currentScore);
        DisplayMessage("Score reset!");

        timerText.SendMessage("InitTimer");
        timerText.SendMessage("AddTime", 1000 * gameDuration);
        gameStarted = true;
    }

    public void Finish()
    {
        if (!gameStarted) { return; }
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("HeartRate_Text").SendMessage("SaveAvgHeartRate");

        SceneManager.LoadScene("Score Screen");
    }
    public void Update()
    {
        if (gameStarted)
        {
            if (GameObject.Find("PointsText"))
            {
                GameObject.Find("PointsText").SendMessage("SetText", currentScore+" POINTS");

                GameObject.Find("AvgText").SendMessage("SetText", hyperateSocket.avgHeartrate + " Ø bpm");
                Destroy(gameObject);
            }
        }
    }
}