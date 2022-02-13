using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterDestroy : MonoBehaviour
{
    // Update is called once per frame
    void Destroy()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        GameObject particleSpawner = (GameObject)Resources.Load("PosterParticles", typeof(GameObject));
        Instantiate(particleSpawner, transform.position, Quaternion.identity);
    }
}
