using UnityEngine;

namespace Unit {
    public class Enemy : Unit {
        [SerializeField] private Vector2 initialDirection;
        
        private Vector2 m_direction;

        protected override void Awake() {
            base.Awake();
            m_direction = initialDirection.normalized;
        }

        protected override void Update() {
            base.Update();
            Move(m_direction, Time.deltaTime);
        }

        public void ChangeDirection(Vector2 newDirection) {
            newDirection.Normalize();
            m_direction = newDirection;
        }

        private void OnCollisionEnter2D(Collision2D other) {
            Debug.Log(other.gameObject.name);
            if (other.collider.GetComponent<Castle>() == null) return;
            other.collider.GetComponent<Castle>().TakeDamage(damagePerSecond);
            Destroy(gameObject);
        }

        private void OnCollisionStay2D(Collision2D other) {
            if (other.collider.GetComponent<Swordsman>() == null || AttackTimer < 1f) return;
            other.collider.GetComponent<Swordsman>().TakeDamage(damagePerSecond);
            AttackTimer = 0f;
        }
    }
} // namespace Unit