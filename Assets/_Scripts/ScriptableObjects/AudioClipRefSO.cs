using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SoundsRef")]
public class AudioClipRefSO : ScriptableObject {
  public List<AudioClip> chop;
  public List<AudioClip> deliveryFail;
  public List<AudioClip> deliverySuccess;
  public List<AudioClip> footstep;
  public List<AudioClip> objectDrop;
  public List<AudioClip> ObjectPickup;
  public List<AudioClip> stoveSizzle;
  public List<AudioClip> trash;
  public List<AudioClip> warning;
}
