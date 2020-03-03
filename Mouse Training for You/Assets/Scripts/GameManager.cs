using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int FinalScore;

    [Header("Score Elements")]
    public int score;
    public Text scoreText;

    public int highscore;
    public Text highscoreText;

    [Header("GameOver Elements")]
    public GameObject gameOverPanel;

    [Header("Sounds")]
    public AudioClip[] sliceSounds;

    public AudioClip[] bombSound;

    private AudioSource audioSource;
    private AudioSource audioSource2;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource2 = GetComponent<AudioSource>();
        gameOverPanel.SetActive(false);
        GetHighscore();
        PlayerPrefs.SetInt("Highscore", 0);
    }

    private void GetHighscore()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Best: " + highscore;
    }

    public void IncreaseScore(int num)
    {
        score += num;
        scoreText.text = score.ToString();

        if (score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = "Best: " + score.ToString();

        }
    }

    public void OnBombHit()
    {
        PlayBombSound();
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        Debug.Log("Bomb Hit");
        
    }

    public void RestartGame()
    {
        score = 0;
        scoreText.text = "0";

        gameOverPanel.SetActive(false);

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(g);
        }

        GetHighscore();

        Time.timeScale = 1;
    }

    public void PlayRandomSliceSound()
    {
        AudioClip randomSound = sliceSounds[Random.Range(0, sliceSounds.Length)];
        audioSource.PlayOneShot(randomSound);
    }

    public void PlayBombSound()
    {
        AudioClip randomSound2 = bombSound[Random.Range(0, bombSound.Length)];
        audioSource2.PlayOneShot(randomSound2);
    }

    void Update()
    {
        if (score == FinalScore)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

    }
}
