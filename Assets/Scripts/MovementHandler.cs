using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float RotationSpeed = 180;
    [SerializeField] private Camera cam;

    public GameObject BodyMesh;

    private Vector3 rotation;

    private float Horizontal { get; set; }
    private float Vertical { get; set; }

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        rotation = new Vector3(Horizontal * RotationSpeed * Time.deltaTime, 0, 0);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(Horizontal * MovementSpeed, 0, Vertical * MovementSpeed) * Time.deltaTime;
        

        if (Vertical < 0)
        {
            BodyMesh.transform.rotation = Quaternion.Euler(BodyMesh.transform.eulerAngles.x, cam.transform.eulerAngles.y - 90, BodyMesh.transform.eulerAngles.z);
        }
        else
        {
            BodyMesh.transform.rotation = Quaternion.Euler(BodyMesh.transform.eulerAngles.x, cam.transform.eulerAngles.y + 90, BodyMesh.transform.eulerAngles.z);
        }
    }

}
