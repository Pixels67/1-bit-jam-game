using UnityEngine;

namespace Unit {
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : Unit {
        [SerializeField] private Vector2 initialDirection;

        private Castle m_castle;
        private Vector2 m_direction;

        private void Awake() {
            CurrentHealth = (int)maxHealth;
            m_castle = FindFirstObjectByType(typeof(Castle)) as Castle;
            m_direction = initialDirection.normalized;
        }

        protected override void Update() {
            base.Update();
            Move(m_direction, Time.deltaTime);

            Vector2 distance = m_castle.transform.position - transform.position;
            if (distance.magnitude > attackRange) return;
            m_castle.TakeDamage(damagePerSecond);
            Destroy(gameObject);
        }

        public void ChangeDirection(Vector2 newDirection) {
            newDirection.Normalize();
            m_direction = newDirection;
        }
    }
} // namespace Unit