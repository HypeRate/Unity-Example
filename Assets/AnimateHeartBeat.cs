using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateHeartBeat : MonoBehaviour
{
    float animationProgress = 0;

    void Update()
    {
        float interpScale = Mathf.Lerp(0.35f, 0.5f, animationProgress);
        transform.localScale = new Vector3(interpScale, interpScale, interpScale);
        animationProgress -= 0.75f * Time.deltaTime;
    }

    public void StartHeartBeat()
    {
        animationProgress = 1;
    }
}
