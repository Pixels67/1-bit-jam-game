using UnityEngine;

namespace Unit {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Unit : MonoBehaviour, IDamageable {
        [SerializeField] protected UnitStats unitStats;

        protected float AttackTimer;

        protected virtual void Awake() {
            CurrentHealth = (int) unitStats.maxHealth;
        }

        protected virtual void Update() {
            if (CurrentHealth <= 0)
                Destroy(gameObject);
            AttackTimer += Time.deltaTime;
        }

        public int CurrentHealth { get; protected set; }

        public virtual void TakeDamage(uint damage) {
            CurrentHealth -= (int)damage;
        }

        protected virtual void Move(Vector2 direction, float deltaTime) {
            transform.Translate(unitStats.speed * deltaTime * direction);
        }
    }
    
    [System.Serializable]
    public class UnitStats {
        public uint maxHealth;
        public uint damagePerSecond;
        public float speed;
    }
} // namespace Unit