using UnityEngine;

public class GameImput : MonoBehaviour {
  private PlayerInputActions playerInputActions;

  private void Awake() {
    playerInputActions = new();
    playerInputActions.Player.Enable();
  }
  public Vector2 GetInputVectorNormalized() {
    var inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

    var normalizedInputVector = inputVector.normalized;

    return normalizedInputVector;
  }
}
