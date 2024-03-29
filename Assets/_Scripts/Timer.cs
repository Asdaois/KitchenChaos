using System;
using UnityEngine;

public class Timer : MonoBehaviour {
  public event EventHandler OnTimeup;
  [SerializeField] private String timerName;

  [SerializeField] private bool isActive;
  [SerializeField] private bool oneshoot;
  [SerializeField, Min(0.1f)] float time = 0.1f;
  float currentTime;


  private void Update() {
    if (!isActive)
      return;

    if (currentTime < time) {
      currentTime += Time.deltaTime;
    }

    if (currentTime > time) {
      currentTime = 0;

      if (oneshoot) {
        StopTimer();
      }

      OnTimeup?.Invoke(this, EventArgs.Empty);
    }
  }

  public void StartTimer() {
    isActive = true;
    currentTime = 0;
  }

  public void StopTimer() {
    isActive = false;
  }

  public void SetOneShot(bool v) {
    oneshoot = v;
  }

  public float GetCurrentTimeUntilAlarm() {
    return currentTime;
  }

  public void SetAlarmTime(float aTimerTime) {
    time = aTimerTime;
  }

  public float GetAlarmTime() {
    return time;
  }
}
