using UnityEngine;

public class Collector : Building {
    [SerializeField] private float snowPerSecond;
    
    private float m_collectTimer;
    private float m_accumulator;
    private Castle m_castle;

    private void Awake() {
        m_castle = FindFirstObjectByType<Castle>();
    }

    private void Update() {
        m_collectTimer += Time.deltaTime;
        
        if (IsInteger(snowPerSecond) && m_collectTimer >= 1f) {
            m_castle.snow += (uint) snowPerSecond;
            m_collectTimer = 0f;
            return;
        }
        
        m_accumulator += snowPerSecond * Time.deltaTime;
        if (m_accumulator < 1f || m_collectTimer < 1f) return;
        
        m_castle.snow += 1;
        m_accumulator -= 1f;
        m_collectTimer = 0f;
    }

    private bool IsInteger(float number) {
        return Mathf.FloorToInt(number) == Mathf.CeilToInt(number);
    }
}
