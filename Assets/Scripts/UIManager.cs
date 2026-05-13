using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    [Header("Coin Counter")]
    private PlayerStatistics statistics;
    [SerializeField] private TextMeshProUGUI coinCounterText;
    
    [Header("Player Health")]
    [SerializeField] private Character character;
    [SerializeField] private Image healthbar;
    
    [Header("Canvas")]
    [SerializeField] private CanvasGroup hudCanvasGroup;
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private float fadingTIme = 2.0f;
    private bool isFadingInGameOver = false;
    
    [Header("Victory Screen")]
    [SerializeField] private CanvasGroup victoryScreen;

    private IEnumerator FadeHudElement(CanvasGroup inElement, CanvasGroup outElement)
    {

        if (inElement == gameOverCanvasGroup)
        {
            this.isFadingInGameOver = true;
        } 
        else if (outElement == gameOverCanvasGroup)
        {
            this.isFadingInGameOver = false;
        }

        float timer = 0.0f;
        while (timer < this.fadingTIme)
        {
            float percent = timer / this.fadingTIme;
            outElement.alpha = 1.0f - percent;
            inElement.alpha = percent;
            yield return null;
            timer += Time.deltaTime;
        }
        outElement.alpha = 0.0f;
        inElement.alpha = 1.0f;
    }

    private void Update()
    {
        float healthInPercent = this.character.GetCurrentHealth() / this.character.GetMaxHealth();
        this.healthbar.fillAmount = healthInPercent;

        if (healthInPercent <= 0.0f && !this.isFadingInGameOver)
        {
            this.StartCoroutine(this.FadeHudElement(gameOverCanvasGroup, hudCanvasGroup));
            statistics.coinCounter = 0;
            string coinText = $"Coins: {this.statistics.coinCounter}";
            this.coinCounterText.text = coinText;
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Awake()
    {
        instance = this;
        this.statistics = new PlayerStatistics() {coinCounter = 0};
    }

    public void OnGameVictory()
    {
        StartCoroutine(FadeHudElement(victoryScreen, hudCanvasGroup));
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HUDonRespawn()
    {
        if (isFadingInGameOver)
        {
            StartCoroutine(this.FadeHudElement(hudCanvasGroup, gameOverCanvasGroup));
        }
        else
        {
            StartCoroutine(this.FadeHudElement(hudCanvasGroup, victoryScreen));
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.healthbar.fillAmount = 1.0f;
    }

    public void CollectCoin()
    {
        this.statistics.coinCounter++;
        string coinText = $"Coins: {this.statistics.coinCounter}";
        this.coinCounterText.text = coinText;
    }

    private class PlayerStatistics
    {
        public int coinCounter = 0;
        // add more stats
    }
}
