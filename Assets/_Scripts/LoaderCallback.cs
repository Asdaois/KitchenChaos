using UnityEngine;

public class LoaderCallback : MonoBehaviour {
  private bool isFirstUpdate = true;

  void Update() {
    if (!isFirstUpdate) { return; }

    Loader.LoaderCallback();

    isFirstUpdate = false;
  }
}
