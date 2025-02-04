using System.Collections.Generic;
using System.Linq;
using Unit;
using UnityEngine;

namespace Building {
    public class Barracks : MonoBehaviour {
        [SerializeField] private GameObject swordsmanObject;
        [SerializeField] private float spawnRate;
        [SerializeField] private uint maxUnitCount;
        [SerializeField] private Vector3 spawnPositionOffset;
        
        private float m_spawnTimer;
        private List<GameObject> m_swordsmen = new();

        private void Awake() {
            Swordsman.OnSwordsmanDeathEvent += UpdateSwordsmenList;
        }

        private void Update() {
            m_spawnTimer += Time.deltaTime;
            
            if (m_spawnTimer < 1f / spawnRate || m_swordsmen.Count >= maxUnitCount) return;
            
            m_spawnTimer = 0f;
            m_swordsmen.Add(Instantiate(swordsmanObject, transform.position + spawnPositionOffset, transform.rotation));
        }
        
        private void UpdateSwordsmenList() {
            m_swordsmen = m_swordsmen.Where(swordsman => swordsman != null).ToList();
        }
    }
} // namespace Building