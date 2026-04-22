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

    private IEnumerator FadeInGameOver()
    {
        this.isFadingInGameOver = true;

        float timer = 0.0f;
        while (timer < this.fadingTIme)
        {
            float percent = timer / this.fadingTIme;
            this.hudCanvasGroup.alpha = 1.0f - percent;
            this.gameOverCanvasGroup.alpha = percent;
            yield return null;
            timer += Time.deltaTime;
        }
        this.hudCanvasGroup.alpha = 0.0f;
        this.gameOverCanvasGroup.alpha = 1.0f;
    }

    private void Update()
    {
        float healthInPercent = this.character.GetCurrentHealth() / this.character.GetMaxHealth();
        this.healthbar.fillAmount = healthInPercent;

        if (healthInPercent <= 0.0f && !this.isFadingInGameOver)
        {
            this.StartCoroutine(this.FadeInGameOver());
        }
    }

    private void Awake()
    {
        instance = this;
        this.statistics = new PlayerStatistics() {coinCounter = 0};
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
