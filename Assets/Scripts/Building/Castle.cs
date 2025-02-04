using UnityEngine;

public class Castle : MonoBehaviour, IDamageable {
    [SerializeField] private uint maxHealth;

    private void Awake() {
        CurrentHealth = (int)maxHealth;
    }

    private void Update() {
        //Debug.Log(CurrentHealth);
    }

    public int CurrentHealth { get; private set; }

    public void TakeDamage(uint damage) {
        CurrentHealth -= (int)damage;
    }
}