using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class shooting : MonoBehaviour
{
    public float surfaceOffset = 0.1f;

    CinemachineVirtualCamera vcam;

    GameObject particleSpawner, entryHole, scoreHandler; 
    
    public AudioSource audioSource; 
    public AudioClip clip;

    void Awake()
    {
        entryHole = (GameObject)Resources.Load("entry_sprite", typeof(GameObject));
        particleSpawner = (GameObject)Resources.Load("Hit_Particles", typeof(GameObject));
        scoreHandler = GameObject.Find("ScoreHandler");
        vcam = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();

        InputAction leftClick = new InputAction(binding: "<Mouse>/leftButton");
        leftClick.performed += ctx =>
        {
            RaycastHit hit;
            float x = Screen.width / 2f;
            float y = Screen.height / 2f;

            var ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
            if (Physics.Raycast(ray, out hit))
            {
                float scoreAcc = 0;
                if (hit.collider.tag == "Score Target")
                {
                    float dist = Vector3.Distance(hit.collider.transform.position, hit.point);
                    scoreAcc = 10 - Mathf.Min(10, Mathf.Floor(dist / 0.045f));
                }
                else if (hit.collider.tag == "Reset Target")
                {
                    scoreHandler.SendMessage("ResetScore");
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Reset Position"))
                    {
                        go.SendMessage("Reset");
                    }
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("RemoveOnReset"))
                    {
                        Destroy(go);
                    }
                    return;
                }
                var bonus = hit.collider.GetComponent<BonusPoints>();
                if (bonus != null)
                {
                    scoreAcc += bonus.bonus;
                }

                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddTorque(5 * hit.normal);
                    rb.AddForce(hit.normal * -1 + new Vector3(0, 1.5f, 0), ForceMode.Impulse);
                }
                else
                {
                    // only place entryholes on non-rigidbody objects
                    Instantiate(entryHole, hit.point + hit.normal * surfaceOffset, Quaternion.FromToRotation(new Vector3(0, 1, 0), hit.normal));
                }
                Instantiate(particleSpawner, hit.point + hit.normal * surfaceOffset, Quaternion.FromToRotation(new Vector3(0, 1, 0), hit.normal));

                scoreHandler.SendMessage("AddScore", scoreAcc);
                hit.collider.SendMessage("Destroy", null, SendMessageOptions.DontRequireReceiver);

                audioSource.PlayOneShot(clip, 0.5f);
            }
        };
        leftClick.Enable();


        InputAction rightHold = new InputAction(binding: "<Mouse>/rightButton");
        rightHold.started += ctx =>
        {
            vcam.m_Lens.FieldOfView = 20;
        };
        rightHold.Enable();

        InputAction rightUp = new InputAction(binding: "<Mouse>/rightButton");
        rightUp.canceled += ctx =>
        {
            vcam.m_Lens.FieldOfView = 60;
        };
        rightUp.Enable();
    }
}
