using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField, Min(0)]
  private float moveSpeed = 7f;
  private void Update() {
    var inputVector = new Vector2();

    if (Input.GetKey(KeyCode.W)) {
      inputVector.y += 1;
    }

    if (Input.GetKey(KeyCode.S)) {
      inputVector.y -= 1;
    }

    if (Input.GetKey(KeyCode.A)) {
      inputVector.x -= 1;
    }

    if (Input.GetKey(KeyCode.D)) {
      inputVector.x += 1;
    }

    var normalizedInputVector = inputVector.normalized;
    var movementDirection = new Vector3(normalizedInputVector.x, 0, normalizedInputVector.y);
    transform.position += movementDirection * (Time.deltaTime * moveSpeed);

    var rotationSpeed = 10f;
    transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotationSpeed);
    Debug.Log(normalizedInputVector);
  }
}
