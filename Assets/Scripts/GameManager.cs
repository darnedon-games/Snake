using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private Snake snake;

    private int currentScore = 0;
    private int currentScene;
    private int nextScene;

    //public int CurrentScore { get => currentScore; set => currentScore = value; }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        nextScene = currentScene + 1;
    }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f; // Se pausa el juego
        //music.Pause();// Se pausa la música
    }
    
    private void ChangeLevel()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void AddScore(int score)
    {
        currentScore = score + currentScore;
        if ((currentScore >= 20) && (currentScene <= 1))
        {
            ChangeLevel();
        }
        scoreText.text = "Score: " + currentScore.ToString();
    }

    public void SetGameOver()
    {
        gameOverCanvas.SetActive(true);
    }
}
