using UnityEngine;
using UnityEngine.SceneManagement; // Indispensable pour changer de scène

public class MainMenu : MonoBehaviour
{
    // Fonction pour le bouton JOUER
    public void PlayGame()
    {
        // Charge la scène de jeu (Assure-toi qu'elle s'appelle bien "Game" ou change le nom ici)
        SceneManager.LoadScene("Game");
    }

    // Fonction pour le bouton QUITTER
    public void QuitGame()
    {
        Debug.Log("On quitte le jeu !");
        Application.Quit();
    }
}