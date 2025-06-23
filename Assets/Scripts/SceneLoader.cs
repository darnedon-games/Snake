using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadInitialLevel()
    {
        Time.timeScale = 1f; // Se reanuda el juego
        //music.UnPause();// Se reanuda la m�sica
        SceneManager.LoadScene("Level1");
    }

    public void LoadMenuScene()
    {
        if (Time.timeScale == 0f)
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1f; // Se reanuda el juego
            //music.UnPause();// Se reanuda la m�sica
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1f; // Se reanuda el juego
        //music.UnPause();// Se reanuda la m�sica
        EventSystem.current.SetSelectedGameObject(null); // Se limpia la selecci�n del bot�n para que no quede en estado "Pressed"
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
