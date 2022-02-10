using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerHandler : MonoBehaviour
{
    public float lastStart = 0;

    // Update is called once per frame
    void Update()
    {
        float curProgress = lastStart - Time.time * 1000;
        if (curProgress <= 0)
        {
            GetComponent<Text>().text = "Time: 00:00:000";
            return;
        }

        TimeSpan timeSpan = TimeSpan.FromMilliseconds(curProgress);
        string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        GetComponent<Text>().text = "Time: " + timeText;
    }

    public void InitTimer()
    {
        lastStart = Time.time * 1000;
    }

    public void AddTime(float amount)
    {
        lastStart += amount;
    }
}
