using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerHandler : MonoBehaviour
{
    public float lastStart = 0;
    public float redStart = 45.0f;
    bool soundPlayed = false;
    public AudioSource audioSource;
    public AudioClip clip;

    // Update is called once per frame
    void Update()
    {
        float curProgress = lastStart - Time.time * 1000;
        if (curProgress <= 0)
        {
            GetComponent<Text>().text = "Time: 00:00:000";
            GetComponent<Text>().color = new Color(1, 1, 1, 0.5f);
            if (lastStart > 0)
            {
                if (GameObject.Find("ScoreHandler"))
                {
                    GameObject.Find("ScoreHandler").SendMessage("Finish");
                }
            }
            return;
        }

        TimeSpan timeSpan = TimeSpan.FromMilliseconds(curProgress);
        string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        GetComponent<Text>().text = "Time: " + timeText;

        if (timeSpan.Seconds == 5 && timeSpan.Milliseconds <500 && timeSpan.Minutes == 0 &&  !soundPlayed)
        {
            soundPlayed = true;
            audioSource.PlayOneShot(clip, 0.5f);
        }

        if (timeSpan.Minutes == 0 && timeSpan.Seconds <= redStart)
        {
            GetComponent<Text>().color = new Color(1, timeSpan.Seconds / redStart, timeSpan.Seconds / redStart, 1);
        }
    }

    public void InitTimer()
    {
        lastStart = Time.time * 1000;
        GetComponent<Text>().color = new Color(1, 1, 1, 1);
        soundPlayed = false;
    }

    public void AddTime(float amount)
    {
        lastStart += amount;
    }
}
