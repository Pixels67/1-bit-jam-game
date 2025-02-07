using UnityEngine;

public class Castle : MonoBehaviour, IDamageable {
    public int CurrentHealth { get; private set; }
    
    public uint snow;
    public uint maxHealth;

    [SerializeField] private float snowGainRate;
    [SerializeField] private AudioClip hitSFX;
    
    private float m_snowTimer;

    private void Awake() {
        CurrentHealth = (int)maxHealth;
    }

    private void Update() {
        m_snowTimer += Time.deltaTime;
        if (m_snowTimer >= 1f / snowGainRate) {
            snow += 1;
            m_snowTimer = 0f;
        }
    }

    public void TakeDamage(uint damage) {
        CurrentHealth -= (int)damage;
        SoundEffectsManager.instance.PlaySFXClip(hitSFX, transform.position);
    }
}