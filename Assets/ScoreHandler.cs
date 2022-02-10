using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public float currentScore = 0;
    GameObject scoreText, pointDisplayText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreDisplay");
        pointDisplayText = (GameObject)Resources.Load("SimpleScore", typeof(GameObject));
    }

    public void DisplayMessage(string msg)
    {
        var scoreObj = Instantiate(pointDisplayText, new Vector3(0, 0, 0), Quaternion.identity);
        scoreObj.GetComponent<PointsTextHandler>().displayText = msg;
    }

    public void AddScore(float amount)
    {
        currentScore += amount;

        DisplayMessage(amount + " Points!");

        scoreText.SendMessage("ChangeText", currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.SendMessage("ChangeText", currentScore);
        DisplayMessage("Score reset!");
    }
}
