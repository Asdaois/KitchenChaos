using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour {
  public event EventHandler OnStateChanged;
  public event EventHandler OnGamePauseChanged;
  public static KitchenGameManager Instance { get; private set; }
  [SerializeField] float timeToPlay;

  enum GameState {
    WaitingForStart,
    CountdownToStart,
    GamePlaying,
    GameOver
  }

  [SerializeField] private Timer waitingForStartTimer;
  [SerializeField] private Timer countdownToStartTimer;
  [SerializeField] private Timer gamePlayingTimer;

  private bool isGamePaused = false;


  private GameState currentState;
  private GameState GetCurrentState() {
    return currentState;
  }


  private void GameImput_OnPauseAction(object sender, EventArgs e) {
    if (isGamePaused)
      ResumeGame();
    else
      PauseGame();

    OnGamePauseChanged?.Invoke(this, new());
  }

  private void PauseGame() {
    Time.timeScale = 0f;
    isGamePaused = true;
  }

  private void ResumeGame() {
    Time.timeScale = 1f;
    isGamePaused = false;
  }

  private void SetCurrentState(GameState value) {
    Debug.Log($"state changed from {currentState} to {value}");
    currentState = value;
    OnStateChanged?.Invoke(this, EventArgs.Empty);
  }

  private void Awake() {
    Instance = this;
    SetCurrentState(GameState.WaitingForStart);
  }

  private void Start() {
    waitingForStartTimer.OnTimeup += WaitingForStartTimer_OnTimeup;
    countdownToStartTimer.OnTimeup += CountdownToStartTimer_OnTimeup;
    gamePlayingTimer.OnTimeup += GamePlayingTimer_OnTimeup;

    waitingForStartTimer.StartTimer();
    gamePlayingTimer.SetAlarmTime(timeToPlay);

    GameImput.Instance.OnPauseAction += GameImput_OnPauseAction;
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

  internal bool IsGameOverActive() {
    return currentState == GameState.GameOver;
  }

  public float GetCurrentPlayedTimeInPercentage() {
    return gamePlayingTimer.GetCurrentTimeUntilAlarm() / gamePlayingTimer.GetAlarmTime();
  }

  public bool GetIsGamePaused() => isGamePaused;
}
