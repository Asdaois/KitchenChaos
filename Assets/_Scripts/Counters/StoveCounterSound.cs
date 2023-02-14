using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StoveCounterSound : MonoBehaviour {
  [SerializeField] private StoveCounter stoveCounter;
  private AudioSource audioSource;

  private void Awake() {
    audioSource = GetComponent<AudioSource>();
  }


  private void Start() {
    stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
  }

  private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
    switch (e.state) {
      case StoveCounter.StoveCounterState.Idle:
      case StoveCounter.StoveCounterState.Burned:
        audioSource.Stop();
        break;
      case StoveCounter.StoveCounterState.Frying:
      case StoveCounter.StoveCounterState.Fried:
        audioSource.Play();
        break;

    }
  }
}
