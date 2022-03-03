using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    public void SetText(string newText)
    {
        GetComponent<Text>().text = newText;
    }
}
