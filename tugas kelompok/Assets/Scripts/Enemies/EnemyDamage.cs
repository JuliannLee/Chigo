using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
