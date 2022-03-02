using UnityEngine;
using UnityEngine.UI;

public class AmmoHandler : MonoBehaviour
{
    float lastShot = 0;
    float shootingCooldown = 0;

    // Update is called once per frame
    void Update()
    {
        float diff = lastShot + shootingCooldown - Time.time;
        if (diff <= 0)
        {
            GameObject.Find("AmmoImage_Border").GetComponent<RawImage>().color = Color.white;
            return;
        }
        GameObject.Find("AmmoImage_Border").GetComponent<RawImage>().color = Color.gray;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, -GetComponent<RectTransform>().rect.height * (diff/ shootingCooldown));

    }

    public void Shoot(float cooldown)
    {
        shootingCooldown = cooldown;
        lastShot = Time.time;
    }
}
