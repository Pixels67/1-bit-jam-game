using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;

public class Snowman : Building {
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject snowball;
    private float m_fireTimer;

    private List<Enemy> m_targets = new();

    private void Awake() {
        Enemy.OnEnemyDeathEvent += UpdateTargets;
    }

    private void Update() {
        m_fireTimer += Time.deltaTime;

        if (!m_targets.Any() || m_fireTimer < 1f / fireRate) return;

        m_fireTimer = 0f;
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var newEnemy = collision.GetComponent<Enemy>();
        if (newEnemy != null) m_targets.Add(newEnemy);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        var newEnemy = collision.GetComponent<Enemy>();
        if (m_targets.Contains(newEnemy)) m_targets.Remove(newEnemy);
    }

    private void UpdateTargets() {
        m_targets = m_targets.Where(target => target != null).ToList();
    }

    private void Fire() {
        Vector2 direction = (m_targets.First().transform.position - transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Instantiate(snowball, transform.position, Quaternion.Euler(0f, 0f, angle));
    }
}