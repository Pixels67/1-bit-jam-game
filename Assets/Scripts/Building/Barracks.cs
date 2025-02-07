using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;
using Random = UnityEngine.Random;

public class Barracks : Building {
    [SerializeField] private GameObject swordsmanObject;
    [SerializeField] private float spawnRate;
    [SerializeField] private uint maxUnitCount;

    private float m_spawnTimer;
    private List<GameObject> m_swordsmen = new();

    private void Awake() {
        Swordsman.OnSwordsmanDeathEvent += UpdateSwordsmenList;
    }

    private void Update() {
        m_spawnTimer += Time.deltaTime;

        if (m_spawnTimer < 1f / spawnRate || m_swordsmen.Count >= maxUnitCount) return;

        m_spawnTimer = 0f;
        m_swordsmen.Add(
            Instantiate(swordsmanObject, transform.position + GetRandomOffset(-2f, 2f), transform.rotation)
        );
    }

    private Vector3 GetRandomOffset(float min, float max) {
        return new Vector3(Random.Range(min, max), Random.Range(min, max), 0f);
    }

    private void UpdateSwordsmenList() {
        m_swordsmen = m_swordsmen.Where(swordsman => swordsman != null).ToList();
    }
}