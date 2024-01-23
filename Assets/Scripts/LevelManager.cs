using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    // Singleton instance of LevelManager for easy access from other scripts
    public static LevelManager manager;

    // Reference to the death screen UI GameObject
    public GameObject deathScreen;

    // References to TextMeshProUGUI components for score and highscore display
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    // Current score of the player
    public int score;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Assign this instance to the static manager variable
        manager = this;
    }

    // Method to be called when the game is over
    public void GameOver()
    {
        // Activate the death screen UI
        deathScreen.SetActive(true);

        // Update the score text to display the final score
        scoreText.text = "Score: " + score.ToString();
    }

    // Method to replay the game
    public void ReplayGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Method to increase the player's score
    public void IncreaseScore(int amount)
    {
        // Add the specified amount to the current score
        score += amount;

        // Update the score text to reflect the new score
        scoreText.text = "Score: " + score.ToString();
    }
}
