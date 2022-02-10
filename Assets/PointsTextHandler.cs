using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsTextHandler : MonoBehaviour
{
    GameObject grandChild;
    public float moveAmount = 10;
    public Color fadeColor;
    public float fadeTime = 4.0f;
    public string displayText = "";
    // Start is called before the first frame update
    void Start()
    {
        grandChild = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        grandChild.GetComponent<Text>().text = displayText;
        Object.Destroy(this.gameObject, fadeTime);
    }

    // Update is called once per frame
    void Update()
    {
        grandChild.transform.position += new Vector3(0, moveAmount * Time.deltaTime, 0);
        Color objColor = grandChild.GetComponent<Text>().color;
        grandChild.GetComponent<Text>().color = Color.Lerp(objColor, fadeColor, fadeTime * Time.deltaTime);
    }
}
