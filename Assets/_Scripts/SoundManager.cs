using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

  [SerializeField] private AudioClipRefSO audioClips;

  private void Start() {
    DeliveryManager.Instance.OnRecipeSuccess += Delivery_OnRecipeSuccess;
    DeliveryManager.Instance.OnRecipeFailed += Delivery_OnRecipeFailed;
    CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
    Player.Instance.OnPickSomething += Player_OnPickSomething;
    BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
    TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
  }

  private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e) {
    PlaySound(audioClips.objectDrop, ((TrashCounter)sender).transform.position);
  }

  private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e) {
    PlaySound(audioClips.objectDrop, ((BaseCounter)sender).transform.position);
  }

  private void Player_OnPickSomething(object sender, System.EventArgs e) {
    PlaySound(audioClips.ObjectPickup, ((Player)sender).transform.position);
  }

  private void CuttingCounter_OnAnyCut(object sender, CuttingCounter.OnAnyCutEventArgs e) {
    PlaySound(audioClips.chop.GetRandomElement(), e.position);
  }

  private void Delivery_OnRecipeFailed(object sender, System.EventArgs e) {
    PlaySound(audioClips.deliveryFail, DeliveryCounter.Instance.transform.position);
  }

  private void Delivery_OnRecipeSuccess(object sender, System.EventArgs e) {
    PlaySound(audioClips.deliverySuccess, DeliveryCounter.Instance.transform.position);
  }

  private void PlaySound(List<AudioClip> anAudioClips, Vector3 aPosition, float aVolume = 1f) {
    AudioSource.PlayClipAtPoint(anAudioClips.GetRandomElement(), aPosition, aVolume);
  }

  private void PlaySound(AudioClip anAudioClip, Vector3 aPosition, float aVolume = 1f) {
    AudioSource.PlayClipAtPoint(anAudioClip, aPosition, aVolume);
  }
}
