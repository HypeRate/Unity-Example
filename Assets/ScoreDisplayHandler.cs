using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayHandler : MonoBehaviour
{

    public void ChangeText(float amount)
    {
        GetComponent<Text>().text = "Score: " + amount;
    }
}
