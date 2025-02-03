using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unit {
    [RequireComponent(typeof(CircleCollider2D))]
    public class Swordsman : Unit {
        [SerializeField] private float attackRange;
        
        private List<Enemy> m_targets = new();
        private CircleCollider2D m_range;

        protected override void Awake() {
            base.Awake();
            GetComponent<CircleCollider2D>().isTrigger = true;
            m_range = GetComponent<CircleCollider2D>();
        }

        protected override void Update() {
            base.Update();
            
            if (!m_targets.Any()) {
                Vector2 displacement = m_range.offset.normalized;
                Move(displacement, Time.deltaTime);
                return;
            }

            if (Vector2.Distance(m_targets.First().transform.position, transform.position) > attackRange + 0.1f) {
                Vector2 displacement = (m_targets.First().transform.position - transform.position).normalized;
                Move(displacement, Time.deltaTime);
            }
            else if (Vector2.Distance(m_targets.First().transform.position, transform.position) < attackRange - 0.1f) {
                Vector2 displacement = -(m_targets.First().transform.position - transform.position).normalized;
                Move(displacement, Time.deltaTime);
            }

            if (AttackTimer < 1f ) return;

            AttackTimer = 0f;
            m_targets.First().TakeDamage(damagePerSecond);
            UpdateTargets();
        }

        private void UpdateTargets() {
            m_targets = m_targets.Where(target => target != null).ToList();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            UpdateTargets();
            if (other.GetComponent<Enemy>() == null) return;
            m_targets.Add(other.GetComponent<Enemy>());
        }

        private void OnTriggerExit2D(Collider2D other) {
            UpdateTargets();
            if (other.GetComponent<Enemy>() == null) return;
            m_targets.Remove(other.GetComponent<Enemy>());
        }

        protected override void Move(Vector2 displacement, float deltaTime) {
            base.Move(displacement, deltaTime);
            m_range.offset -= speed * Time.deltaTime * displacement;
        }
    }
} // namespace Unit