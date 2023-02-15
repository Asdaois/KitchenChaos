using UnityEngine.SceneManagement;

public static class Loader {
  public enum Scene {
    MainMenuScene,
    GameScene,
    LoadingScene
  }
  private static Scene targetScene;

  public static void LoadScene(Scene aTargetScene) {
    targetScene = aTargetScene;
    SceneManager.LoadScene(Scene.LoadingScene.ToString());
  }

  public static void LoaderCallback() {
    SceneManager.LoadScene(targetScene.ToString());
  }
}
