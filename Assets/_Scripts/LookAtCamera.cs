using UnityEngine;

public class LookAtCamera : MonoBehaviour {
  private enum Mod {
    LookAtRegular,
    LookAtInverted,
    CameraForward,
    CameraForwardInverted,
  }

  [SerializeField] private Mod mode;
  private void LateUpdate() {
    switch (mode) {
      case Mod.LookAtRegular:
        transform.LookAt(Camera.main.transform);
        break;
      case Mod.LookAtInverted:
        var directionFromCamera = transform.position - Camera.main.transform.position;
        transform.LookAt(transform.position + directionFromCamera);
        break;
      case Mod.CameraForward:
        transform.forward = Camera.main.transform.forward;
        break;
      case Mod.CameraForwardInverted:
        transform.forward -= Camera.main.transform.forward;
        break;
      default:
        break;
    }
  }
}
