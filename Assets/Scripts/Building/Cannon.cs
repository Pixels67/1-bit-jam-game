using Unit;
using UnityEngine;

public class Cannon : Building {
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject iceBall;
    [SerializeField] private LayerMask ignoreLayers;
    
    private float m_fireTimer;

    private void Update() {
        m_fireTimer += Time.deltaTime;

        var hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, ~ignoreLayers);
        
        if (!hit) return;
        if (hit.collider.GetComponent<Enemy>() == null) return;
        if (m_fireTimer < 1f / fireRate) return;
        
        m_fireTimer = 0f;
        Fire();
    }

    private void Fire() {
        Vector2 direction = transform.right;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 position = transform.position;
        position.z = 0f;
        Instantiate(iceBall, position, Quaternion.Euler(0f, 0f, angle));
    }
}
