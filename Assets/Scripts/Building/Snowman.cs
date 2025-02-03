using Unit;
using UnityEngine;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;

namespace Building {
    public class Snowman : MonoBehaviour {
        [SerializeField] private float fireRate;
        //[SerializeField] private float range;
        [SerializeField] private GameObject snowball;
        private float m_fireTimer;

        private List<Enemy> m_enemyList = new();
        private Enemy m_target;

        private void Update() {
            m_fireTimer += Time.deltaTime;

            if (m_target == null) {
                FindNewTarget();
                return;
            }

            //var targetDistance = Vector2.Distance(m_target.transform.position, transform.position);
            //;
            //if (targetDistance > range) FindNewTarget();

            if (m_fireTimer < 1f / fireRate) return;

            m_fireTimer = 0f;
            Fire();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var newEnemy = collision.GetComponent<Enemy>();
            if (newEnemy != null) m_enemyList.Add(newEnemy);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var newEnemy = collision.GetComponent<Enemy>();
            if (m_enemyList.Contains(newEnemy)) m_enemyList.Remove(newEnemy);
        }

        void CleanList()
        {
            //clears list of null gameobjects
            m_enemyList = m_enemyList.Where(obj => obj != null).ToList();
        }

        private void Fire() {
            CleanList();
            if (m_target == null) return;
            Vector2 direction = (m_target.transform.position - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Instantiate(snowball, transform.position, Quaternion.Euler(0, 0, angle));
        }

        private void FindNewTarget() {
            m_target = m_enemyList.First();
        }
    }
} // namespace Building