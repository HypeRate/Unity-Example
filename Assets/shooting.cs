using UnityEngine;
using UnityEngine.InputSystem;

public class shooting : MonoBehaviour
{
    [SerializeField]
    private Camera gameCamera;
    private InputAction click;

    public GameObject entryHole;
    public GameObject scoreDisplayObject;
    public float surfaceOffset = 0.1f;

    void Awake()
    {
        click = new InputAction(binding: "<Mouse>/leftButton");
        click.performed += ctx =>
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

                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddTorque(5*hit.normal);
                    rb.AddForce(hit.normal*-1+new Vector3(0,1.5f,0), ForceMode.Impulse);
                }
                else
                {
                    // only place entryholes on non-rigidbody objects
                    Instantiate(entryHole, hit.point + hit.normal * surfaceOffset, Quaternion.FromToRotation(new Vector3(0, 1, 0), hit.normal));
                }


                var scoreObj = Instantiate(scoreDisplayObject, new Vector3(0, 0, 0), Quaternion.identity);
                scoreObj.GetComponent<ScoreTextHandler>().displayText = scoreAcc + " Points!";
            }
        };
        click.Enable();
    }
}
