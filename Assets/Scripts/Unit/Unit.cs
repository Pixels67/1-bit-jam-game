using UnityEngine;

namespace Unit {
    public class Unit : MonoBehaviour, IDamageable {
        [SerializeField] protected uint maxHealth;
        [SerializeField] protected uint damagePerSecond;
        [SerializeField] protected float attackRange;
        [SerializeField] protected float speed;

        protected virtual void Update() {
            if (CurrentHealth <= 0)
                Destroy(gameObject);
        }

        public int CurrentHealth { get; protected set; }

        public virtual void TakeDamage(uint damage) {
            CurrentHealth -= (int)damage;
        }

        protected virtual void Move(Vector2 direction, float deltaTime) {
            transform.Translate(speed * deltaTime * direction);
        }
    }
} // namespace Unit