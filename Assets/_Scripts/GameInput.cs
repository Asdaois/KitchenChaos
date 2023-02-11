using System;
using UnityEngine;

public class GameImput : MonoBehaviour {

  public event EventHandler OnInteractAction;

  public event EventHandler OnInteractAlternativeAction;

  private PlayerInputActions playerInputActions;

  private void Awake() {
    playerInputActions = new();
    playerInputActions.Player.Enable();

    playerInputActions.Player.Interact.performed += Interact_performed;
    playerInputActions.Player.InteractAlternative.performed += InteractAlternative_performed;
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