using Unit;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private float spawnRate;
    [SerializeField] private Vector2 direction;

    private float m_spawnTimer;

    private void Update() {
        m_spawnTimer += Time.deltaTime;
        if (m_spawnTimer < spawnRate) return;
        m_spawnTimer -= spawnRate;
        var enemy = Instantiate(enemyObject, transform.position, transform.rotation);
        enemy.GetComponent<Enemy>().ChangeDirection(direction);
    }
}