using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordManager : MonoBehaviour
{
    public TMP_Text wordDisplay;
    public TMP_InputField inputField;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text finalScoreText;
    public GameObject gameOverPanel; // Panel that appears at the end

    private List<string> wordList = new List<string>() { 
        "apple", "banana", "orange", "grape", "mango", "pear", "plum", "strawberry", "kiwi", "peach", "melon", "cherry"
    };
    private string currentWord;
    private int score = 0;
    private float timer = 60f;
    private bool gameRunning = true;

    void Start()
    {
        GenerateWord();
        inputField.onEndEdit.AddListener(OnWordTyped);  // Check word when Enter is pressed
        scoreText.text = "Score: " + score.ToString();
        timerText.text = "Time: " + Mathf.Round(timer).ToString();
        gameOverPanel.SetActive(false);  // Hide game over panel at start
    }

    void Update()
    {
        if (gameRunning)
        {
            // Update Timer
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timer).ToString();

            if (timer <= 0)
            {
                gameRunning = false;
                EndGame();
            }
        }
    }

    void GenerateWord()
    {
        // Pick a random word from the list
        currentWord = wordList[Random.Range(0, wordList.Count)];
        wordDisplay.text = currentWord;
        inputField.text = "";  // Clear input field
    }

    void OnWordTyped(string playerInput)
    {
        if (!gameRunning) return;

        if (playerInput.Equals(currentWord, System.StringComparison.OrdinalIgnoreCase))
        {
            score++;
            scoreText.text = "Score: " + score.ToString();
            GenerateWord();  // Generate a new word
        }

        inputField.ActivateInputField();  // Refocus on input field
    }

    void EndGame()
    {
        wordDisplay.text = "Game Over!";
        inputField.interactable = false;
        finalScoreText.text = "Your final score: " + score;
        gameOverPanel.SetActive(true); // Show Game Over screen
    }

    public void RestartGame()
    {
        score = 0;
        timer = 60f;
        gameRunning = true;
        inputField.interactable = true;
        gameOverPanel.SetActive(false);
        scoreText.text = "Score: " + score.ToString();
        GenerateWord();
    }
}