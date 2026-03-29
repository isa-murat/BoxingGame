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
        uiManager.HideResult();
    }

    public void OnFighterDefeated(FighterController defeated)
    {
        if (!roundActive) return;
        roundActive = false;

        if (defeated == player1)
        {
            p2Wins++;
            uiManager.ShowRoundResult("Oyuncu 2 raund kazandi!");
        }
        else
        {
            p1Wins++;
            uiManager.ShowRoundResult("Oyuncu 1 raund kazandi!");
        }

        Invoke("NextRound", 2f);
    }

    void EndRoundByTime()
    {
        roundActive = false;

        float p1Percent = player1.GetHealthPercent();
        float p2Percent = player2.GetHealthPercent();

        if (p1Percent > p2Percent)
        {
            p1Wins++;
            uiManager.ShowRoundResult("Oyuncu 1 raund kazandi!");
        }
        else if (p2Percent > p1Percent)
        {
            p2Wins++;
            uiManager.ShowRoundResult("Oyuncu 2 raund kazandi!");
        }
        else
        {
            uiManager.ShowRoundResult("Berabere!");
        }

        Invoke("NextRound", 2f);
    }

    void NextRound()
    {
        currentRound++;

        // Oyunu kazanan var mi kontrol et
        int roundsNeeded = (maxRounds / 2) + 1;

        if (p1Wins >= roundsNeeded)
        {
            uiManager.ShowGameResult("OYUNCU 1 KAZANDI!", 1);
            return;
        }
        if (p2Wins >= roundsNeeded)
        {
            uiManager.ShowGameResult("OYUNCU 2 KAZANDI!", 2);
            return;
        }

        if (currentRound > maxRounds)
        {
            if (p1Wins > p2Wins)
                uiManager.ShowGameResult("OYUNCU 1 KAZANDI!", 1);
            else if (p2Wins > p1Wins)
                uiManager.ShowGameResult("OYUNCU 2 KAZANDI!", 2);
            else
                uiManager.ShowGameResult("BERABERE!", 0);
            return;
        }

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
