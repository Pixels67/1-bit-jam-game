using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unit {
    [RequireComponent(typeof(CircleCollider2D))]
    public class Swordsman : Unit {
        public delegate void OnSwordsmanDeath();

        [SerializeField] private float attackRange;
        
        private CircleCollider2D m_range;
        private List<Enemy> m_targets = new();

        protected override void Awake() {
            base.Awake();
            GetComponent<CircleCollider2D>().isTrigger = true;
            m_range = GetComponent<CircleCollider2D>();
            Enemy.OnEnemyDeathEvent += UpdateTargets;
        }

        protected override void Update() {
            base.Update();

            if (CurrentHealth <= 0)
                OnSwordsmanDeathEvent?.Invoke();

            if (!m_targets.Any()) {
                var displacement = m_range.offset.magnitude > 0.1f ? m_range.offset.normalized : Vector2.zero;
                Move(displacement, Time.deltaTime);
                return;
            }

            if (Vector2.Distance(m_targets.First().transform.position, transform.position) > attackRange + 0.1f) {
                Vector2 displacement = (m_targets.First().transform.position - transform.position).normalized;
                Move(displacement, Time.deltaTime);
                return;
            }

            if (Vector2.Distance(m_targets.First().transform.position, transform.position) < attackRange - 0.1f) {
                Vector2 displacement = -(m_targets.First().transform.position - transform.position).normalized;
                Move(displacement, Time.deltaTime);
            }

            if (AttackTimer < 1f) return;

            AttackTimer = 0f;
            m_targets.First().TakeDamage(unitStats.damagePerSecond);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.GetComponent<Enemy>() == null) return;
            m_targets.Add(other.GetComponent<Enemy>());
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.GetComponent<Enemy>() == null) return;
            m_targets.Remove(other.GetComponent<Enemy>());
        }

        public static event OnSwordsmanDeath OnSwordsmanDeathEvent;

        private void UpdateTargets() {
            m_targets = m_targets.Where(target => target != null).ToList();
        }

        protected override void Move(Vector2 displacement, float deltaTime) {
            base.Move(displacement, deltaTime);
            m_range.offset -= unitStats.speed * Time.deltaTime * displacement;
        }
    }
} // namespace Unit