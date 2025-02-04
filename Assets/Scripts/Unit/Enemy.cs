using UnityEngine;

namespace Unit {
    public class Enemy : Unit {
        public delegate void OnEnemyDeath();
        public Vector2 Direction { get; private set; }
        
        [SerializeField] private UnitData unitData;

        protected override void Awake() {
            UnitStats = unitData.enemiesStats["Enemy 1"];
            base.Awake();
            if (CurrentHealth <= 0)
                OnEnemyDeathEvent?.Invoke();
        }

        protected override void Update() {
            base.Update();
            Move(Direction, Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.collider.GetComponent<Castle>() == null) return;
            other.collider.GetComponent<Castle>().TakeDamage(UnitStats.damagePerSecond);
            Destroy(gameObject);
        }

        private void OnCollisionStay2D(Collision2D other) {
            if (other.collider.GetComponent<Swordsman>() == null || AttackTimer < 1f) return;
            other.collider.GetComponent<Swordsman>().TakeDamage(UnitStats.damagePerSecond);
            AttackTimer = 0f;
        }

        public static event OnEnemyDeath OnEnemyDeathEvent;

        public void ChangeDirection(Vector2 newDirection) {
            newDirection.Normalize();
            Direction = newDirection;
        }
    }
} // namespace Unit