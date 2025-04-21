using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        // Logic to start a new game or load a saved game
        // For example, load the game scene or initialize game settings here
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        // Logic to save game state or perform cleanup if necessary
        // For example, save player progress or settings here
        Application.Quit();
        Debug.Log("Game is exiting...");
    }

    public void OpenOptions()
    {
        // Logic to open settings menu
        // For example, load the options scene or display a settings panel here
        // This could also be a separate scene or a UI panel that shows the options
        Debug.Log("Opening options menu...");
    }

    public void OpenCredits()
    {
        // Logic to open credits menu
        // For example, load the credits scene or display a credits panel here
        // This could also be a separate scene or a UI panel that shows the credits
        Debug.Log("Opening credits menu...");
    
    }
}
