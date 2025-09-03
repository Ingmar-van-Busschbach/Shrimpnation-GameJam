using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthSystem : MonoBehaviour, IDamageAble
{
    [SerializeField] public float maxHealth = 10f;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        Destroy(this.gameObject);
    }
}
