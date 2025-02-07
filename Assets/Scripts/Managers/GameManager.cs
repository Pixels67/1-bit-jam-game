using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager I { get; private set; }

    private enum GameState {
        None,
        Won,
        Lost
    }
    
    private List<EnemySpawner> m_spawners;
    private GameState m_gameState = GameState.None;
    private Castle m_castle;

    private void Awake() {
        if (I == null) {
            I = this;
        }
        else {
            Destroy(gameObject);
        }
        
        m_castle = FindFirstObjectByType<Castle>();
        m_spawners = FindObjectsByType<EnemySpawner>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
        Time.timeScale = 1f;
    }

    private void Update() {
        if (m_castle.CurrentHealth <= 0 && m_gameState == GameState.None) {
            Time.timeScale = 0f;
            StopAllCoroutines();
            StartCoroutine(UIManager.I.ShowLosePanel());
            m_gameState = GameState.Lost;
        }
        
        if (!AllSpawnersEmpty() || !AllEnemiesDead() || m_gameState != GameState.None) return;
        Time.timeScale = 0f;
        StopAllCoroutines();
        StartCoroutine(UIManager.I.ShowWinPanel());
        m_gameState = GameState.Won;
    }

    private bool AllEnemiesDead() {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        return enemies == null || enemies.Length == 0;
    }

    private bool AllSpawnersEmpty() {
        return m_spawners.All(spawner => spawner.IsEmpty);
    }
}
