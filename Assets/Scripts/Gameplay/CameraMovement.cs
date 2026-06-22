using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;      // Drag your sphere here
    public float distance = 13f;
    public float rotationSpeed = 100f;

    private float yaw = 0f;
    private float pitch = 0f;

    void Update()
    {
        //Negative, otherwise spinning wrong direction
        float horizontal = -Input.GetAxis("Horizontal");
        float vertical = -Input.GetAxis("Vertical");

        yaw += horizontal * rotationSpeed * Time.deltaTime;
        pitch -= vertical * rotationSpeed * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -89f, 89f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 direction = rotation * new Vector3(0, 0, -distance);

        transform.position = target.position + direction;
        transform.LookAt(target);
    }
}
