using System;
using UnityEngine;

public class GameImput : MonoBehaviour {
  static public GameImput Instance { get; private set; }

  public event EventHandler OnInteractAction;

  public event EventHandler OnInteractAlternativeAction;
  public event EventHandler OnPauseAction;

  private PlayerInputActions playerInputActions;

  private void Awake() {
    playerInputActions = new();
    playerInputActions.Player.Enable();

    playerInputActions.Player.Interact.performed += Interact_performed;
    playerInputActions.Player.InteractAlternative.performed += InteractAlternative_performed;
    playerInputActions.Player.Pause.performed += Pause_performed;

    Instance = this;
  }

  private void OnDestroy() {
    playerInputActions.Player.Interact.performed += Interact_performed;
    playerInputActions.Player.InteractAlternative.performed += InteractAlternative_performed;
    playerInputActions.Player.Pause.performed += Pause_performed;

    playerInputActions.Dispose();

  }

  private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
    OnPauseAction?.Invoke(this, EventArgs.Empty);
  }

  private void InteractAlternative_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
    OnInteractAlternativeAction?.Invoke(this, EventArgs.Empty);
  }

  private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
    OnInteractAction?.Invoke(this, EventArgs.Empty);
  }

  public Vector2 GetInputVectorNormalized() {
    var inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

    var normalizedInputVector = inputVector.normalized;

    return normalizedInputVector;
  }
}