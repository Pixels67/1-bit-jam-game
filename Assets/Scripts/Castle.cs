using UnityEngine;

public class Castle : MonoBehaviour, IDamageable {
    public int CurrentHealth { get; private set; }
    
    [SerializeField] private uint maxHealth;

    private void Awake() {
        CurrentHealth = (int) maxHealth;
    }

    private void Update() {
        //Debug.Log(CurrentHealth);
    }

    public void TakeDamage(uint damage) {
        CurrentHealth -= (int) damage;
    }
}