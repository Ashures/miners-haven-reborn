using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCamera : MonoBehaviour
{
  public static RotateCamera Instance { get; private set; }
  [SerializeField] private GameObject grid;

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

    transform.RotateAround(grid.transform.position, Vector3.up, input.x * adjustedRotationSpeed * Time.deltaTime);
  }
}
