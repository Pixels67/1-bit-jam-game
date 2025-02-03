using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private float spawnRate;
    [SerializeField] private Vector2 direction;
    
    private float m_spawnTimer;

    private void Update() {
        m_spawnTimer += Time.deltaTime;
        if (m_spawnTimer < spawnRate) return;
        m_spawnTimer -= spawnRate;
        GameObject enemy = Instantiate(objectToSpawn, transform.position, transform.rotation);
        enemy.GetComponent<Enemy>().ChangeDirection(direction);
    }
}
