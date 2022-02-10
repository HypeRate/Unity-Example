using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Texture m_MainTexture;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.mainTexture = m_MainTexture;
    }
}
