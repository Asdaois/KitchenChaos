using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
  [SerializeField] Button playButton;
  [SerializeField] Button quitButton;


  private void Awake() {
    playButton.onClick.AddListener(() => {
      Loader.LoadScene(Loader.Scene.GameScene);
    });

    quitButton.onClick.AddListener(() => {
      Application.Quit();
    });

    Time.timeScale = 1.0f;
  }
}
