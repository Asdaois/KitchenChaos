using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {
  public static Player Instance { get; private set; }
  public bool IsWalking { get; private set; }

  public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;

  public class OnSelectedCounterChangeEventArgs : EventArgs {
    public BaseCounter selectedCounter;
  }

  public event EventHandler OnPickSomething;

  [SerializeField, Min(0)] private float moveSpeed = 7f;
  [SerializeField] private GameImput gameImput;
  [SerializeField] private float playerRadius = 0.7f;
  [SerializeField] private float playerHeight = 2f;

  [SerializeField] private float rotationSpeed = 10f;

  [SerializeField] private LayerMask counterLayermask;

  [SerializeField] private Transform kitchenObjectHoldPoint;

  private BaseCounter selectedCounter;

  public BaseCounter SelectedCounter {
    get => selectedCounter;
    private set {
      selectedCounter = value;
      OnSelectedCounterChange?.Invoke(this, new() { selectedCounter = SelectedCounter });
    }
  }

  private Vector3 lastInteractiveDirection;
  private KitchenObject kitchenObject;

  private void Awake() {
    if (Instance != null) {
      Debug.LogError("There is more that one player instance");
    }
    Instance = this;
  }

  private void Start() {
    gameImput.OnInteractAction += GameImput_OnInteractAction;
    gameImput.OnInteractAlternativeAction += GameImput_OnInteractAlternativeAction;
  }

  private void GameImput_OnInteractAlternativeAction(object sender, EventArgs e) {
    if (selectedCounter != null) {
      SelectedCounter.InteractAlternative(this);
    }
  }

  private void Update() {
    HandleMovement();
    HandleInteractions();
  }

  private void GameImput_OnInteractAction(object sender, System.EventArgs e) {
    if (SelectedCounter != null) {
      SelectedCounter.Interact(this);
    }
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
      if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
        if (baseCounter != SelectedCounter) {
          SelectedCounter = baseCounter;
        }
        return;
      }
    }

    SelectedCounter = null;
  }

  private void HandleMovement() {
    var movementDirection = GetMovementDirection();

    var moveDistance = moveSpeed * Time.deltaTime;
    var canMove = CanMoveInDirection(movementDirection, moveDistance);

    if (!canMove) {
      canMove = movementDirection.x != 0 && CanMoveInDirection(ref movementDirection,
                                   moveDistance,
                                   new Vector3(movementDirection.x, 0, 0));

      if (!canMove) {
        canMove = movementDirection.z != 0 && CanMoveInDirection(ref movementDirection,
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

  public KitchenObject GetKitchenObject() => kitchenObject;

  public void SetKitchenObject(KitchenObject aKitchenObject) {
    if (aKitchenObject != null) {
      OnPickSomething?.Invoke(this, EventArgs.Empty);
    }
    kitchenObject = aKitchenObject;
  }

  public void ClearKitchenObject() => kitchenObject = null;

  public bool HasKitchenObject() => GetKitchenObject() != null;

  public Transform GetKitchenObjectFollowTransform() => kitchenObjectHoldPoint;
}