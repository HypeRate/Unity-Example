using UnityEngine;
using UnityEngine.UI;

public class submitHyperateID : MonoBehaviour
{
    InputField _inputField;

    void Start()
    {
        _inputField = GetComponent<InputField>();
        _inputField.onEndEdit.AddListener(fieldValue =>
        {
            Debug.Log(fieldValue);
            _inputField.text = fieldValue = fieldValue.Trim();
            GameObject hrText = GameObject.Find("HeartRate_Text");
            hrText.GetComponent<hyperateSocket>().ChangeUserID(fieldValue.Trim());
        });
    }
}
