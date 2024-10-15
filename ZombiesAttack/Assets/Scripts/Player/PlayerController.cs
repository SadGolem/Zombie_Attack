using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private GameController gameController;

    void Awake()
    {
        mainCamera = Camera.main;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            gameController.StopGame();
        }
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

                mouseWorldPos.y = transform.position.y;

                Vector3 awayFromMouseDir = transform.position - mouseWorldPos;

                transform.rotation = Quaternion.LookRotation(awayFromMouseDir, Vector3.up);
            }
        }
    }
}


