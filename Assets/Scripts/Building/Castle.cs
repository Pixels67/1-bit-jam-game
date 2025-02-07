using UnityEngine;

public class Castle : MonoBehaviour, IDamageable {
    public uint snow;
    public uint maxHealth;
    
    [SerializeField] private AudioClip hitSFX;

    private void Awake() {
        CurrentHealth = (int)maxHealth;
    }

    public int CurrentHealth { get; private set; }

    public void TakeDamage(uint damage) {
        CurrentHealth -= (int)damage;
        SoundEffectsManager.instance.PlaySFXClip(hitSFX, transform.position);
    }
}