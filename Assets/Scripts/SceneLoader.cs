using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    

    public void LoadGame()
    {
        SceneManager.LoadScene(GameManager.Scenes.GameScene.ToString());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(GameManager.Scenes.MainMenueScene.ToString());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}