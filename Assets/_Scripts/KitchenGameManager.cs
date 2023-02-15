using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour {
  public event EventHandler OnStateChanged;
  public static KitchenGameManager Instance { get; private set; }

  enum GameState {
    WaitingForStart,
    CountdownToStart,
    GamePlaying,
    GameOver
  }

  [SerializeField] private Timer waitingForStartTimer;
  [SerializeField] private Timer countdownToStartTimer;
  [SerializeField] private Timer gamePlayingTimer;


  private GameState currentState = GameState.WaitingForStart;
  private GameState GetCurrentState() {
    return currentState;
  }

  private void SetCurrentState(GameState value) {
    Debug.Log($"state changed from {currentState} to {value}");
    currentState = value;
    OnStateChanged?.Invoke(this, EventArgs.Empty);
  }

  private void Awake() {
    Instance = this;
  }

  private void Start() {
    waitingForStartTimer.OnTimeup += WaitingForStartTimer_OnTimeup;
    countdownToStartTimer.OnTimeup += CountdownToStartTimer_OnTimeup;
    gamePlayingTimer.OnTimeup += GamePlayingTimer_OnTimeup;

    waitingForStartTimer.StartTimer();
  }

  private void GamePlayingTimer_OnTimeup(object sender, System.EventArgs e) {
    if (GetCurrentState() != GameState.GamePlaying)
      return;

    SetCurrentState(GameState.GameOver);
  }

  private void CountdownToStartTimer_OnTimeup(object sender, System.EventArgs e) {
    if (GetCurrentState() != GameState.CountdownToStart)
      return;

    SetCurrentState(GameState.GamePlaying);
    gamePlayingTimer.StartTimer();
  }

  private void WaitingForStartTimer_OnTimeup(object sender, System.EventArgs e) {
    if (GetCurrentState() != GameState.WaitingForStart)
      return;

    SetCurrentState(GameState.CountdownToStart);
    countdownToStartTimer.StartTimer();
  }

  public bool IsGamePlaying() {
    return currentState == GameState.GamePlaying;
  }

  public bool IsCountdownToSTartActive() {
    return currentState == GameState.CountdownToStart;
  }

  public float GetCurrentCountDownTimerTime() {
    return countdownToStartTimer.GetAlarmTime() - countdownToStartTimer.GetCurrentTimeUntilAlarm();
  }
}
