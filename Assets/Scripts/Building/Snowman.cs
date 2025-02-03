using Unit;
using UnityEngine;

namespace Building {
    public class Snowman : MonoBehaviour {
        [SerializeField] private float fireRate;
        [SerializeField] private float range;
        [SerializeField] private GameObject snowball;
        private float m_fireTimer;

        private Enemy m_target;

        private void Update() {
            m_fireTimer += Time.deltaTime;

            if (m_target == null) {
                FindNewTarget();
                return;
            }

            var targetDistance = Vector2.Distance(m_target.transform.position, transform.position);
            ;
            if (targetDistance > range) FindNewTarget();

            if (m_fireTimer < 1f / fireRate) return;

            m_fireTimer = 0f;
            Fire();
        }

        private void Fire() {
            if (m_target == null) return;
            Vector2 direction = (m_target.transform.position - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Instantiate(snowball, transform.position, Quaternion.Euler(0, 0, angle));
        }

        private void FindNewTarget() {
            var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            foreach (var enemy in enemies) {
                var distance = Vector2.Distance(enemy.transform.position, transform.position);
                if (distance > range) continue;
                m_target = enemy;
            }
        }
    }
} // namespace Building