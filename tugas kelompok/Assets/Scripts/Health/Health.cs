using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Sounds")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Hurt logic
            anim.SetTrigger("hurt");
<<<<<<< HEAD
            StartCoroutine(Invulnerability());
=======
            StartCoroutine(Invunerability());
>>>>>>> d7cb2dc29d083070909fa8411fc448b2c77481d3
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            // Death logic
            if (!dead)
            {
                GetComponent<PlayerMovement>().enabled = false;
<<<<<<< HEAD
                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                dead = true;
=======
                StartCoroutine(DisableAfterDeath());
>>>>>>> d7cb2dc29d083070909fa8411fc448b2c77481d3
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    public void Respawn()
    {
        dead = false;
        currentHealth = startingHealth;

        anim.ResetTrigger("die");
        anim.Play("Idle");

        StartCoroutine(Invulnerability());

        // Re-enable all components
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }

        GetComponent<PlayerMovement>().enabled = true;
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
