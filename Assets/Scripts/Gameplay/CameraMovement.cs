using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public Transform pivotTransform;
    public float distance;
    public float rotationSpeed;
    public InputActionProperty scrollAction;

    private float yaw = 0f;
    private float pitch = 0f;
    private float zoom = 1f;

    void Start()
    {
        scrollAction.action.Enable();
    }

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
        
        float z = scrollAction.action.ReadValue<float>();
        if (z > 0 && zoom > -0.75f)
        {
            zoom -= 0.05f;
        }
        else if (z < 0 && zoom < 1f) 
        {
            zoom += 0.05f;
        }

        Vector3 targetPosition = pivotTransform.position + direction + direction.normalized * zoom;
        transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime);
        transform.LookAt(pivotTransform);
    }
}
