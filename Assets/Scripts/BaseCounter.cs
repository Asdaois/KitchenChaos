using UnityEngine;

public class BaseCounter : MonoBehaviour {
  public virtual void Interact(Player aPlayer) => Debug.LogError("BaseCounter.Interact()");
}
