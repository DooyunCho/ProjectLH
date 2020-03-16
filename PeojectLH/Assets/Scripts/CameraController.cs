using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour {
    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    public bool LookAtPlayer = false;

    public bool RotateAroundPlayer = true;

    public float RotationsSpeed = 5.0f;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    // Use this for initialization
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    // FixedUpdate is called after Update methods
    void LateUpdate()
    {
        Quaternion camTurnAngleRightandLeft = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);
        Quaternion camTurnAngleUpandDown = Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * RotationsSpeed, Vector3.right);

        _cameraOffset = camTurnAngleUpandDown * camTurnAngleRightandLeft * _cameraOffset;

        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        transform.LookAt(PlayerTransform);

        PlayerTransform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
    }
}
