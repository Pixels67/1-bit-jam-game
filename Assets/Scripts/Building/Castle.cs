using UnityEngine;

public class Castle : MonoBehaviour, IDamageable {
    public uint snow;
    
    [SerializeField] private uint maxHealth;

    private void Awake() {
        CurrentHealth = (int)maxHealth;
    }

    public int CurrentHealth { get; private set; }

    public void TakeDamage(uint damage) {
        CurrentHealth -= (int)damage;
    }
}