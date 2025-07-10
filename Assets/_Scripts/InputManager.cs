using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

  private bool panningStarted = false;

  void Awake()
  {
    Cursor.lockState = CursorLockMode.Confined;

    InputSystem.actions.FindAction("StartPan").performed += StartPan;
    InputSystem.actions.FindAction("StartPan").canceled += EndPan;
    InputSystem.actions.FindAction("Pan").performed += Pan;
    InputSystem.actions.FindAction("OpenBuildingMenu").performed += OpenBuildingMenu;
  }

  void StartPan(InputAction.CallbackContext ctx)
  {
    panningStarted = true;
    Cursor.lockState = CursorLockMode.Locked;
  }

  void EndPan(InputAction.CallbackContext ctx)
  {
    panningStarted = false;
    Cursor.lockState = CursorLockMode.Confined;
  }

  void Pan(InputAction.CallbackContext ctx)
  {
    InputDevice device = ctx.control.device;
    if (device is Mouse && !panningStarted) return;

    RotateCamera.Instance.Rotate(ctx.ReadValue<Vector2>(), device);
  }

  void OpenBuildingMenu(InputAction.CallbackContext ctx)
  {

  }
}
