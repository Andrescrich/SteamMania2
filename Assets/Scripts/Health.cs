using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float maxHealth;

    public float CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value > maxHealth ? maxHealth : value;
    }

    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            if (value < 0)
                maxHealth = 0;
            else
                maxHealth = value;
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
    }
}
