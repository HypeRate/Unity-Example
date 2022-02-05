using UnityEngine;
using UnityEngine.InputSystem;

public class shooting : MonoBehaviour
{
    [SerializeField]
    private Camera gameCamera;
    private InputAction click;

    public GameObject entryHole;
    public float surfaceOffset = 0.1f;

    void Awake()
    {
        click = new InputAction(binding: "<Mouse>/leftButton");
        click.performed += ctx => {
            RaycastHit hit;
            float x = Screen.width / 2f;
            float y = Screen.height / 2f;

            var ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Score Target")
                {
                    float dist = Vector3.Distance(hit.collider.transform.position, hit.point);
                    Debug.Log("Hit cool!"+ dist);
                }
                Instantiate(entryHole, hit.point+ hit.normal* surfaceOffset, Quaternion.FromToRotation(new Vector3(0,1,0), hit.normal));
            }
        };
        click.Enable();
    }
}
