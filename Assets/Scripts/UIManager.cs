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

        HideResult();
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
        if (roundResultPanel != null)
        {
            roundResultPanel.SetActive(true);
            roundResultText.text = message;
        }
    }

    public void HideResult()
    {
        if (roundResultPanel != null) roundResultPanel.SetActive(false);
        if (gameResultPanel != null) gameResultPanel.SetActive(false);
    }

    public void ShowGameResult(string message, int winner)
    {
        if (gameResultPanel != null)
        {
            gameResultPanel.SetActive(true);
            gameResultText.text = message;
        }
    }

    void OnRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }
}
