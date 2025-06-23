using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject pauseCanvas;
    private int currentScore = 0;

    //public int CurrentScore { get => currentScore; set => currentScore = value; }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f; // Se pausa el juego
        //music.Pause();// Se pausa la música
    }
    
    private void ChangeLevel()
    {
        
    }

    public void AddScore(int score)
    {
        currentScore = score + currentScore;
        scoreText.text = currentScore.ToString();
    }

    public void SetGameOver()
    {
        gameOverCanvas.SetActive(true);
    }
}
