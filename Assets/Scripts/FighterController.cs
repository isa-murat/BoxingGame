using UnityEngine;

public class FighterController : MonoBehaviour
{
    public int playerNumber = 1; // 1 veya 2
    public float maxHealth = 100f;
    public float attackDamage = 10f;
    public float attackCooldown = 0.5f;

    public FighterController opponent; // Karsi oyuncu

    private float currentHealth;
    private bool isDefending = false;
    private float lastAttackTime = 0f;
    private Animator animator;

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
            animator.SetTrigger("idle");
        }

        // UI guncelle
        UpdateHealthUI();
    }

    // Butondan cagirilacak
    public void Attack()
    {
        if (!GameManager.Instance.IsRoundActive()) return;
        if (Time.time - lastAttackTime < attackCooldown) return;

        lastAttackTime = Time.time;

        // Animasyon tetikle
        if (animator != null)
        {
            animator.SetTrigger("punch");
        }

        // Rakibe hasar ver
        opponent.TakeDamage(attackDamage);
    }

    // Butondan cagirilacak (basili tutma)
    public void StartDefend()
    {
        if (!GameManager.Instance.IsRoundActive()) return;

        isDefending = true;

        if (animator != null)
        {
            animator.SetBool("isDefending", true);
        }
    }

    // Buton birakilinca
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
            // Defans varsa hasar cok azalir
            damage *= 0.1f;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        // Hit animasyonu
        if (animator != null && !isDefending)
        {
            animator.SetTrigger("hit");
        }

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
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
