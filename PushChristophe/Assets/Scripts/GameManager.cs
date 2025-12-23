using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI; // Glisse le Panel "GameOverPanel" ici

    public void GameOver()
    {
        // 1. On affiche l'interface
        gameOverUI.SetActive(true);

        // 2. On arrête le temps (pour que les ennemis arrêtent de bouger)
        Time.timeScale = 0f;

        // 3. IMPORTANT : On débloque la souris pour pouvoir cliquer sur le bouton
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        // On remet le temps normal avant de relancer
        Time.timeScale = 1f;

        // On recharge la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}