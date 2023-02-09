using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {

  [SerializeField] private StoveCounter stoveCounter;
  [SerializeField] private GameObject stoveGameObject;
  [SerializeField] private GameObject particlesGameObject;

  private void Start() {
    stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
  }

  private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
    var isFriendOrIsFried = e.state == StoveCounter.StoveCounterState.Frying || e.state == StoveCounter.StoveCounterState.Fried;

    stoveGameObject.SetActive(isFriendOrIsFried);
    particlesGameObject.SetActive(isFriendOrIsFried);
  }


}
