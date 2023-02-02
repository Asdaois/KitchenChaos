using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField, Min(0)]
  private float moveSpeed = 7f;
  [SerializeField]
  private GameImput gameImput;

  [SerializeField]
  float playerRadius = 0.7f;
  [SerializeField]
  float playerHeight = 2f;
  public bool IsWalking { get; private set; }

  private void Update() {
    var normalizedInputVector = gameImput.GetInputVectorNormalized();
    var movementDirection = new Vector3(normalizedInputVector.x, 0, normalizedInputVector.y);

    var moveDistance = moveSpeed * Time.deltaTime;
    bool canMove = CanMoveInDirection(movementDirection, moveDistance);

    if (!canMove) {
      canMove = CanMoveInDirection(ref movementDirection,
                                   moveDistance,
                                   new Vector3(movementDirection.x, 0, 0));

      if (!canMove) {
        canMove = CanMoveInDirection(ref movementDirection,
                                     moveDistance,
                                     new Vector3(0, 0, movementDirection.z));
      }
    }
    if (canMove) {
      transform.position += movementDirection * (Time.deltaTime * moveSpeed);
    }

    var rotationSpeed = 10f;
    transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotationSpeed);

    IsWalking = movementDirection != Vector3.zero;
  }

  private bool CanMoveInDirection(ref Vector3 movementDirection, float moveDistance, Vector3 movementDirectionAxis) {
    bool canMove;
    canMove = CanMoveInDirection(movementDirectionAxis, moveDistance);

    if (canMove) {
      movementDirection = movementDirectionAxis.normalized;
    }

    return canMove;
  }

  private bool CanMoveInDirection(Vector3 movementDirection, float moveDistance) {
    return !Physics.CapsuleCast(transform.position,
                                       transform.position + Vector3.up * playerHeight,
                                       playerRadius,
                                       movementDirection,
                                       moveDistance);
  }
}
