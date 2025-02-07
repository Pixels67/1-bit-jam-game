using Unit;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public bool IsEmpty {get; private set;}
    
    [SerializeField] private GameObject[] enemiesToSpawn;
    [SerializeField] private uint[] amountsToSpawn;
    [SerializeField] private float spawnRate;
    [SerializeField] private float waitTime;
    [SerializeField] private Vector2 direction;

    private uint m_index;
    private float m_spawnTimer;
    private float m_timer;

    private void Awake() {
        IsEmpty = false;
        m_index = 0;
        m_spawnTimer = 0f;
        m_timer = 0f;
    }

    private void Update() {
        m_timer += Time.deltaTime;
        m_spawnTimer += Time.deltaTime;
        
        if (m_index >= enemiesToSpawn.Length) {
            IsEmpty = true;
            return;
        }
        
        if (m_spawnTimer < spawnRate || m_timer < waitTime) return;
        
        m_spawnTimer = 0f;
        var enemy = Instantiate(enemiesToSpawn[m_index], transform.position, transform.rotation);
        enemy.GetComponent<Enemy>().ChangeDirection(direction);
        
        amountsToSpawn[m_index]--;
        if (amountsToSpawn[m_index] == 0) m_index++;
    }
}