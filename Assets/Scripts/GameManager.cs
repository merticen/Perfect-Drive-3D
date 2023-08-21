using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public CarController carController;
    public PlaneController planeController;
    [Header("Time Settings")]
    private float restartDelay = 1f;
    private float winDelay = 3f;
    public float countDownDuration = 120.0f;
    [Header("TEXT")]
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI levelText;
    [Header("Particles")]
    public ParticleSystem deathEffect;
    public ParticleSystem swapEffect;
  

    private float currentTime;

    private void Start()
    {
        currentTime = countDownDuration;
      
    }
    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateCountdownText();
        }
        else
        {
            currentTime = 0;
            UpdateCountdownText();
            KillPlayer();

        }

    }

    private void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void KillPlayer()
    {
        Vector3 playerPosition = carController.activePlayer.transform.position;
        deathEffect.transform.position = playerPosition;
        deathEffect.Play();
        carController.activePlayer.SetActive(false);
        RestartGameDelay();
    }

    public void WinPlayer()
    {
        carController.activePlayer.SetActive(false);
        currentTime = 0f;
        UIDisable();
        winText.gameObject.SetActive(true);
        WinDelay();
    }
    
    private void UIDisable()
    {
        countdownText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
    }

    private void WinDelay()
    {
        Invoke("RestartGame", winDelay);
    }
    private void RestartGameDelay()
    {
        Invoke("RestartGame", restartDelay);
    }
    private void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

}
