using UnityEngine;

public class FighterController : MonoBehaviour
{
    public int playerNumber = 1;
    public float maxHealth = 100f;
    public float attackDamage = 10f;
    public float attackCooldown = 0.8f;

    public FighterController opponent;

    private float currentHealth;
    private bool isDefending = false;
    private float lastAttackTime = 0f;
    private Animator animator;

    // Saldiri animasyonlari listesi
    private string[] attackAnims = { "punch" };
    // Hit reaction rastgele secimi icin
    private bool useHeadHit = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void ResetFighter()
    {
        currentHealth = maxHealth;
        isDefending = false;
        lastAttackTime = 0f;

        if (animator != null)
        {
            animator.SetBool("isDefending", false);
            animator.ResetTrigger("punch");
            animator.ResetTrigger("hit");
            animator.ResetTrigger("knockout");
            animator.ResetTrigger("victory");
            animator.Play("boxing idle", 0, 0f);
        }

        UpdateHealthUI();
    }

    public void Attack()
    {
        if (!GameManager.Instance.IsRoundActive()) return;
        if (Time.time - lastAttackTime < attackCooldown) return;
        if (isDefending) return;

        lastAttackTime = Time.time;

        if (animator != null)
        {
            animator.SetTrigger("punch");
        }

        // Kisa gecikme ile hasar ver (yumruk animasyonu ile senkron)
        Invoke("DealDamage", 0.2f);
    }

    void DealDamage()
    {
        if (opponent != null)
        {
            opponent.TakeDamage(attackDamage);
        }
    }

    public void StartDefend()
    {
        if (!GameManager.Instance.IsRoundActive()) return;

        isDefending = true;

        if (animator != null)
        {
            animator.SetBool("isDefending", true);
        }
    }

    public void StopDefend()
    {
        isDefending = false;

        if (animator != null)
        {
            animator.SetBool("isDefending", false);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDefending)
        {
            damage *= 0.1f;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        if (animator != null && !isDefending)
        {
            animator.SetTrigger("hit");
        }

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            // Knockout animasyonu
            if (animator != null)
            {
                animator.SetTrigger("knockout");
            }

            // Rakibe victory animasyonu
            if (opponent != null && opponent.animator != null)
            {
                opponent.animator.SetTrigger("victory");
            }

            GameManager.Instance.OnFighterDefeated(this);
        }
    }

    void UpdateHealthUI()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateHealth(playerNumber, currentHealth, maxHealth);
        }
    }

    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }
}
