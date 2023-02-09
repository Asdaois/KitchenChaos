using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {

  [SerializeField] private CuttingCounter cuttingCounter;
  [SerializeField] private Image barImage;

  private void Start() {
    cuttingCounter.OnProgressChange += CuttingCounter_OnProgressChange;
    barImage.fillAmount = 0;
    Hide();
  }

  private void CuttingCounter_OnProgressChange(object sender, CuttingCounter.OnProgressChangeEventArgs e) {
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
