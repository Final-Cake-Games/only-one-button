using UnityEngine;

public class CameraPerspective : MonoBehaviour
{
    public Transform cameraHolder;    // Yaw (horizontal)
    public Transform cameraTransform; // Pitch (vertical)
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        cameraHolder = transform;
        cameraTransform = Camera.main.transform; 
        Cursor.lockState = CursorLockMode.Locked; // Hide & lock mouse

    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // --- Pitch (up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // --- Yaw (left/right)
        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -75f, 75f); // Limit to ±75 degrees
        cameraHolder.localRotation = Quaternion.Euler(0f, yRotation, 0f);

        if (Input.GetMouseButtonDown(0)) // 0 = left click
        {
            cameraTransform.gameObject.GetComponent<Camera>().fieldOfView = 30; // Zoom in
        }
        if (Input.GetMouseButtonUp(0)) // 0 = left click
        {
            cameraTransform.gameObject.GetComponent<Camera>().fieldOfView = 60; // Zoom out
        }
    }
}
