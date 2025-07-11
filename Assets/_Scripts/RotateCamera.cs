using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCamera : MonoBehaviour
{
  public static RotateCamera Instance { get; private set; }
  [SerializeField] private GameObject rotateAroundTarget;

  [SerializeField] private float rotationSpeed = 2.0f;
  [SerializeField] private float gamepadSpeedMod = 10.0f;

  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
  }

  public void Rotate(Vector2 input, InputDevice device)
  {
    float adjustedRotationSpeed = rotationSpeed;
    if (device is Gamepad) adjustedRotationSpeed *= gamepadSpeedMod;

    Vector3 targetPos = rotateAroundTarget.transform.position;

    // Horizontal rotation around world up axis
    transform.RotateAround(targetPos, Vector3.up, input.x * adjustedRotationSpeed * Time.deltaTime);

    // Get current direction from grid to camera and right axis between world up and direction
    Vector3 direction = (transform.position - targetPos).normalized;
    Vector3 right = Vector3.Cross(Vector3.up, direction).normalized;

    Vector3 oldPosition = transform.position;
    Quaternion oldRotation = transform.rotation;

    // Vertical rotation around the new right axis
    transform.RotateAround(targetPos, right, -input.y * adjustedRotationSpeed * Time.deltaTime);

    // Clamp vertical angle to avoid weird camera angles
    direction = (transform.position - targetPos).normalized;
    float verticalAngle = Vector3.Angle(Vector3.ProjectOnPlane(direction, Vector3.up), direction);
    if (verticalAngle < 10f || verticalAngle > 80f)
      transform.SetPositionAndRotation(oldPosition, oldRotation);

    // Always look at the grid
    transform.LookAt(targetPos);
  }

}
