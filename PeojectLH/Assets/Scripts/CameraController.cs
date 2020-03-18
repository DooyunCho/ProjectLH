using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour {
    const float minCameraAxisY = -5.0f;
    const float maxCameraAxisY = 80.0f;
    const float minZoomDistance = 5.0f;
    const float maxZoomDistance = 15.0f;

    public Transform Player;
    public Transform CameraTarget;
    private float Distance = 5;
    private float currentX = 4.0f;
    private float currentY = 4.0f;
    private float sensivityX = 4.0f;
    private float sensivityY = 1.0f;
    public float RotationsSpeed = 5.0f;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    // Use this for initialization
    void Start()
    {
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, minCameraAxisY, maxCameraAxisY);

        Distance -= Input.GetAxis("Mouse ScrollWheel") * 5;
        Distance = Mathf.Clamp(Distance, minZoomDistance, maxZoomDistance);
    }

    // FixedUpdate is called after Update methods
    void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 0, -Distance);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = Player.position + rotation * direction;
        transform.LookAt(CameraTarget);

        Player.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
    }
}
