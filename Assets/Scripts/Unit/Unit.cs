using UnityEngine;

namespace Unit {
    [RequireComponent(typeof(Collider2D))]
    public class Unit : MonoBehaviour, IDamageable {
        public int CurrentHealth { get; protected set; }
        
        [SerializeField] protected uint maxHealth;
        [SerializeField] protected uint damagePerSecond;
        [SerializeField] protected float speed;
        
        protected float AttackTimer;

        protected virtual void Awake() {
            CurrentHealth = (int)maxHealth;
        }
        
        protected virtual void Update() {
            if (CurrentHealth <= 0)
                Destroy(gameObject);
            AttackTimer += Time.deltaTime;
        }

        public virtual void TakeDamage(uint damage) {
            CurrentHealth -= (int)damage;
        }

        protected virtual void Move(Vector2 direction, float deltaTime) {
            transform.Translate(speed * deltaTime * direction);
        }
    }
} // namespace Unit