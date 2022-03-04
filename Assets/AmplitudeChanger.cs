using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AmplitudeChanger : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    public float minAmplitudeGain, maxAmplitudeGain, minFrequencyGain, maxFrequencyGain;
    public float minHeartBeat, maxHeartBeat, zoomMultiplier = 1, curHeartBeat = 50;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    public void UpdateAmplitude(int heartBeat)
    {
        curHeartBeat = heartBeat;
    }

    public void UpdateZoom()
    {
        zoomMultiplier = vcam.m_Lens.FieldOfView / 60.0f;
    }

    public void Update()
    {
        float clampedHB = Mathf.Clamp(curHeartBeat, minHeartBeat, maxHeartBeat);
        float apli = Mathf.Lerp(minAmplitudeGain, maxAmplitudeGain, clampedHB / maxHeartBeat) * zoomMultiplier;
        float freq = Mathf.Lerp(minFrequencyGain, maxFrequencyGain, clampedHB / maxHeartBeat);

        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = apli;
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = freq;
    }
}
