using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane mousePlane = new Plane(mainCamera.transform.forward, transform.position);
            float rayDistance;
            if (mousePlane.Raycast(mouseRay, out rayDistance))
            {
                Vector3 mouseWorldPos = mouseRay.GetPoint(rayDistance);

                // make the z component of mouseWorldPos the same as transform.position
                mouseWorldPos.y = transform.position.y;

                Vector3 awayFromMouseDir = transform.position - mouseWorldPos;

                transform.rotation = Quaternion.LookRotation(awayFromMouseDir, Vector3.up);
            }
        }
    }
}


