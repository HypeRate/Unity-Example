using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class SocialShare : MonoBehaviour
{
    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWEET_LANGUAGE = "en";
    private string appStoreLink = "http://tiny.cc/HypeRange";

    public void ShareToTW()
    {
        string nameParameter = "I got "+ GetScore() + " points on the Hype Range!";
        Application.OpenURL(TWITTER_ADDRESS +
           "?text=" + UnityWebRequest.EscapeURL(nameParameter + "\n") +
           "&url=" + appStoreLink +
           "&hashtags=HypeRate,HypeRange,SteadyPulse" +
           "&related=Hyperate_io" +
           "&via=Hyperate_io");
    }

    private int GetScore()
    {
        return Int32.Parse(GameObject.Find("PointsText").GetComponent<Text>().text.Replace(" POINTS",""));
    }

    // Use this for initialization
    public void Screenshot()
    {
        StartCoroutine(UploadPNG());
    }

    IEnumerator UploadPNG()
    {
        // We should only read the screen after all rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);

        //string ToBase64String byte[]
        string encodedText = System.Convert.ToBase64String(bytes);

        var image_url = "data:image/png;base64," + encodedText;

        Debug.Log(image_url);
#if !UNITY_EDITOR
        openWindow(image_url);
#endif
    }

    [DllImport("__Internal")]
    private static extern void openWindow(string url);


}
