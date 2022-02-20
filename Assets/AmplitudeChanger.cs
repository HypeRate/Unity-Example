using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AmplitudeChanger : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    public float minAmplitudeGain, maxAmplitudeGain, minFrequencyGain, maxFrequencyGain;
    public float minHeartBeat, maxHeartBeat;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    public void UpdateAmplitude(int heartBeat)
    {
        float clampedHB = Mathf.Clamp(heartBeat, minHeartBeat, maxHeartBeat);
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.Lerp(minAmplitudeGain, maxAmplitudeGain, clampedHB / maxHeartBeat);
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Mathf.Lerp(minFrequencyGain, maxFrequencyGain, clampedHB / maxHeartBeat);
    }
}
