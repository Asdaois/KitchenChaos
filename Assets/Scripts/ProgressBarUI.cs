using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {

  [SerializeField] GameObject hasProgressGameObject;
  [SerializeField] private Image barImage;
  private IHasProgress hasProgress;

  private void Start() {
    hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
    if (hasProgress == null) {
      Debug.LogError($"The game object {hasProgressGameObject} don't implement IHasProgress");
    }
    hasProgress.OnProgressChange += HasProgress_OnProgressChange;
    barImage.fillAmount = 0;
    Hide();
  }

  private void HasProgress_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e) {
    barImage.fillAmount = e.progressNormalized;
    if (e.progressNormalized == 0 || e.progressNormalized == 1f)
      Hide();
    else
      Show();
  }

  public void Show() {
    gameObject.SetActive(true);
  }

  public void Hide() {
    gameObject.SetActive(false);
  }
}
