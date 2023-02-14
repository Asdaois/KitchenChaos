using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSound : MonoBehaviour {

  [SerializeField] Timer timer;

  private Player player;

  private void Start() {
    player = GetComponent<Player>();
    timer.OnTimeup += Timer_OnTimeUp;
  }

  private void Timer_OnTimeUp(object sender, EventArgs e) {
    if (player.IsWalking)
      SoundManager.Instance.PlayWalkSound(this);
  }
}
