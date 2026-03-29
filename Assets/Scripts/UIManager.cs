using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Health Bars")]
    public Slider p1HealthBar;
    public Slider p2HealthBar;

    [Header("Round Info")]
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI timerText;

    [Header("Result")]
    public GameObject roundResultPanel;
    public TextMeshProUGUI roundResultText;
    public GameObject gameResultPanel;
    public TextMeshProUGUI gameResultText;
    public Button restartButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartClicked);
        }

        HideAllPanels();
    }

    public void UpdateHealth(int playerNumber, float current, float max)
    {
        float percent = current / max;

        if (playerNumber == 1)
        {
            if (p1HealthBar != null) p1HealthBar.value = percent;
        }
        else
        {
            if (p2HealthBar != null) p2HealthBar.value = percent;
        }
    }

    public void UpdateRound(int round)
    {
        if (roundText != null)
        {
            roundText.text = "Round " + round;
        }
    }

    public void UpdateTimer(float time)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
        }
    }

    public void ShowRoundResult(string message)
    {
        // Once hepsini kapat
        HideAllPanels();

        if (roundResultPanel != null)
        {
            roundResultPanel.SetActive(true);
            if (roundResultText != null)
                roundResultText.text = message;
        }
    }

    public void ShowGameResult(string message, int winner)
    {
        // Once hepsini kapat
        HideAllPanels();

        if (gameResultPanel != null)
        {
            gameResultPanel.SetActive(true);
            if (gameResultText != null)
                gameResultText.text = message;
        }
    }

    public void HideAllPanels()
    {
        if (roundResultPanel != null) roundResultPanel.SetActive(false);
        if (gameResultPanel != null) gameResultPanel.SetActive(false);
    }

    // Eski metod ismi icin uyumluluk
    public void HideResult()
    {
        HideAllPanels();
    }

    void OnRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }
}
