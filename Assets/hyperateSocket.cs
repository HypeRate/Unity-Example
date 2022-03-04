using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Newtonsoft.Json.Linq;
using NativeWebSocket;

public class hyperateSocket : MonoBehaviour
{
    // Put your websocket Token ID here
    public string websocketToken = "z5CAzTmsKwpVgafiTUG2jHUT8rJdSeR6Kzyx3h6yFsi4kJRyZEdk8wt2aorJYc9N";
    public string hypeRateID = "";

    // Textbox to display your heart rate in
    Text textBox;
    // Websocket for connection with Hyperate
    WebSocket websocket;
    async void StartServer()
    {
        if (hypeRateID == "")
            return;
        textBox = GetComponent<Text>();

        websocket = new WebSocket("wss://staging.frightrate.com/socket/websocket?token=" + websocketToken);
        Debug.Log("Connect!");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
            textBox.text = "Connecting to "+ hypeRateID + " ...";
            SendWebSocketMessage();
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            // getting the message as a string
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            var msg = JObject.Parse(message);

            if (msg["event"].ToString() == "hr_update")
            {
                // Change textbox text into the newly received Heart Rate (integer like "86" which represents beats per minute)
                textBox.text = (string)msg["payload"]["hr"];
                GameObject playerCam = GameObject.Find("PlayerFollowCamera");
                if (playerCam != null)
                {
                    playerCam.SendMessage("UpdateAmplitude", (int)msg["payload"]["hr"]);
                }
                GameObject.Find("Heart_Icon").SendMessage("StartHeartBeat");
            }
        };

        // Send heartbeat message every 25seconds in order to not suspended the connection
        InvokeRepeating("SendHeartbeat", 1.0f, 25.0f);

        // waiting for messages
        await websocket.Connect();
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (websocket != null)
        {
            websocket.DispatchMessageQueue();
        }
#endif
    }

    public async void ChangeUserID(string newID)
    {
        if (websocket != null)
        {
            if (newID == hypeRateID)
            {
                return;
            }
            await websocket.Close();
            websocket = null;
        }
        hypeRateID = newID;
        StartServer();
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Log into the "internal-testing" channel
            await websocket.SendText("{\"topic\": \"hr:" + hypeRateID + "\", \"event\": \"phx_join\", \"payload\": {}, \"ref\": 0}");
        }
    }
    async void SendHeartbeat()
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Send heartbeat message in order to not be suspended from the connection
            await websocket.SendText("{\"topic\": \"phoenix\",\"event\": \"heartbeat\",\"payload\": {},\"ref\": 0}");

        }
    }

    private async void OnApplicationQuit()
    {
        if (websocket != null)
        {
            await websocket.Close();
        }
    }

}

public class HyperateResponse
{
    public string Event { get; set; }
    public string Payload { get; set; }
    public string Ref { get; set; }
    public string Topic { get; set; }
}