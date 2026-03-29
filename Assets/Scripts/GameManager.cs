using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public FighterController player1;
    public FighterController player2;
    public UIManager uiManager;

    public int maxRounds = 3;
    public float roundTime = 60f;

    private int currentRound = 1;
    private int p1Wins = 0;
    private int p2Wins = 0;
    private float timer;
    private bool roundActive = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartRound();
    }

    void Update()
    {
        if (!roundActive) return;

        timer -= Time.deltaTime;
        uiManager.UpdateTimer(timer);

        if (timer <= 0)
        {
            EndRoundByTime();
        }
    }

    public void StartRound()
    {
        timer = roundTime;
        roundActive = true;

        player1.ResetFighter();
        player2.ResetFighter();

        uiManager.UpdateRound(currentRound);
        uiManager.HideAllPanels();
    }

    public void OnFighterDefeated(FighterController defeated)
    {
        if (!roundActive) return;
        roundActive = false;

        if (defeated == player1)
        {
            p2Wins++;
        }
        else
        {
            p1Wins++;
        }

        // Oyun bitti mi kontrol et
        int roundsNeeded = (maxRounds / 2) + 1;
        bool gameOver = false;
        string gameResultMessage = "";
        int winner = 0;

        if (p1Wins >= roundsNeeded)
        {
            gameOver = true;
            gameResultMessage = "OYUNCU 1 KAZANDI!";
            winner = 1;
        }
        else if (p2Wins >= roundsNeeded)
        {
            gameOver = true;
            gameResultMessage = "OYUNCU 2 KAZANDI!";
            winner = 2;
        }
        else if (currentRound >= maxRounds)
        {
            gameOver = true;
            if (p1Wins > p2Wins)
            {
                gameResultMessage = "OYUNCU 1 KAZANDI!";
                winner = 1;
            }
            else if (p2Wins > p1Wins)
            {
                gameResultMessage = "OYUNCU 2 KAZANDI!";
                winner = 2;
            }
            else
            {
                gameResultMessage = "BERABERE!";
                winner = 0;
            }
        }

        if (gameOver)
        {
            // Direkt oyun sonu goster (raund sonucu gosterme)
            Invoke("ShowGameOver", 1.5f);
        }
        else
        {
            // Raund sonucu goster, sonra yeni raunda gec
            string roundMsg = defeated == player1 ?
                "Oyuncu 2 raund kazandi!" : "Oyuncu 1 raund kazandi!";
            uiManager.ShowRoundResult(roundMsg);
            Invoke("NextRound", 2.5f);
        }
    }

    void ShowGameOver()
    {
        // Once raund panelini kapat
        uiManager.HideAllPanels();

        int roundsNeeded = (maxRounds / 2) + 1;
        string msg;
        int winner;

        if (p1Wins >= roundsNeeded || p1Wins > p2Wins)
        {
            msg = "OYUNCU 1 KAZANDI!";
            winner = 1;
        }
        else if (p2Wins >= roundsNeeded || p2Wins > p1Wins)
        {
            msg = "OYUNCU 2 KAZANDI!";
            winner = 2;
        }
        else
        {
            msg = "BERABERE!";
            winner = 0;
        }

        uiManager.ShowGameResult(msg, winner);
    }

    void EndRoundByTime()
    {
        roundActive = false;

        float p1Percent = player1.GetHealthPercent();
        float p2Percent = player2.GetHealthPercent();

        string roundMsg;

        if (p1Percent > p2Percent)
        {
            p1Wins++;
            roundMsg = "Oyuncu 1 raund kazandi!";
        }
        else if (p2Percent > p1Percent)
        {
            p2Wins++;
            roundMsg = "Oyuncu 2 raund kazandi!";
        }
        else
        {
            roundMsg = "Berabere!";
        }

        // Son raund muydu kontrol et
        int roundsNeeded = (maxRounds / 2) + 1;

        if (p1Wins >= roundsNeeded || p2Wins >= roundsNeeded || currentRound >= maxRounds)
        {
            uiManager.ShowRoundResult(roundMsg);
            Invoke("ShowGameOver", 2f);
        }
        else
        {
            uiManager.ShowRoundResult(roundMsg);
            Invoke("NextRound", 2.5f);
        }
    }

    void NextRound()
    {
        currentRound++;
        StartRound();
    }

    public bool IsRoundActive()
    {
        return roundActive;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
