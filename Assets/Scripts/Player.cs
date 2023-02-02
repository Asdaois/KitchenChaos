using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField, Min(0)]
  private float moveSpeed = 7f;
  [SerializeField]
  private GameImput gameImput;

  [SerializeField]
  private float playerRadius = 0.7f;
  [SerializeField]
  private float playerHeight = 2f;

  [SerializeField]
  private float rotationSpeed = 10f;

  [SerializeField]
  private LayerMask counterLayermask;
  public bool IsWalking { get; private set; }

  private Vector3 lastInteractiveDirection;
  private void Start() {
    gameImput.OnInteractAction += GameImput_OnInteractAction;
  }

  private void Update() {
    HandleMovement();
  }

  private void GameImput_OnInteractAction(object sender, System.EventArgs e) {
    HandleInteractions();
  }

  private void HandleInteractions() {
    var movementDirection = GetMovementDirection();

    if (movementDirection != Vector3.zero) {
      lastInteractiveDirection = movementDirection;
    }

    var interactiveDistance = 2f;

    if (Physics.Raycast(transform.position,
                        lastInteractiveDirection,
                        out RaycastHit raycastHit,
                        interactiveDistance,
                        counterLayermask)) {
      if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
        clearCounter.Interact();
      }
    }
  }

  private void HandleMovement() {
    var movementDirection = GetMovementDirection();

    var moveDistance = moveSpeed * Time.deltaTime;
    var canMove = CanMoveInDirection(movementDirection, moveDistance);

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

    RotatePlayerTowardMomevementDirection(movementDirection);

    IsWalking = movementDirection != Vector3.zero;
  }

  private Vector3 GetMovementDirection() {
    var normalizedInputVector = gameImput.GetInputVectorNormalized();
    var movementDirection = new Vector3(normalizedInputVector.x, 0, normalizedInputVector.y);
    return movementDirection;
  }

  private void RotatePlayerTowardMomevementDirection(Vector3 movementDirection) {
    transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotationSpeed);
  }

  private bool CanMoveInDirection(ref Vector3 movementDirection, float moveDistance, Vector3 movementDirectionAxis) {
    var canMove = CanMoveInDirection(movementDirectionAxis, moveDistance);

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
