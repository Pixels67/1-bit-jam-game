using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour, IDamageable {
    public int CurrentHealth { get; private set; }
    
    [SerializeField] private uint maxHealth;
    [SerializeField] private uint damagePerSecond;
    [SerializeField] private float attackRange;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 initialDirection;

    private Castle m_castle;
    private Vector2 m_direction;

    private void Awake() {
        CurrentHealth = (int) maxHealth;
        m_castle = FindFirstObjectByType(typeof(Castle)) as Castle;
        m_direction = initialDirection.normalized;
    }

    private void Update() {
        transform.Translate(speed * Time.deltaTime * m_direction);
        
        if (CurrentHealth <= 0)
            Destroy(gameObject);
        
        Vector2 distance = m_castle.transform.position - transform.position;
        if (distance.magnitude > attackRange) return;
        m_castle.TakeDamage(damagePerSecond);
        Destroy(gameObject);
    }

    public void TakeDamage(uint damage) {
        CurrentHealth -= (int) damage;
    }

    public void ChangeDirection(Vector2 newDirection) {
        newDirection.Normalize();
        m_direction = newDirection;
    }
}
