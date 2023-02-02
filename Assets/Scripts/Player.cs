using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField, Min(0)]
  private float moveSpeed = 7f;
  [SerializeField]
  private GameImput gameImput;
  public bool IsWalking { get; private set; }

  private void Update() {
    var normalizedInputVector = gameImput.GetInputVectorNormalized();
    var movementDirection = new Vector3(normalizedInputVector.x, 0, normalizedInputVector.y);
    transform.position += movementDirection * (Time.deltaTime * moveSpeed);

    var rotationSpeed = 10f;
    transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotationSpeed);

    IsWalking = movementDirection != Vector3.zero;
  }
}
