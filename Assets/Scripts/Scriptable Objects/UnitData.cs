using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class UnitData : ScriptableObject {
    public StatsDictionary enemiesStats;
    public Unit.UnitStats swordsmanStats;
}

[System.Serializable]
public class StatsDictionary {
    public List<string> keys;
    public List<Unit.UnitStats> stats;
    
    public Unit.UnitStats this[string key]
    {
        get {
            for (int i = 0; i < keys.Count; i++) {
                if (keys[i] != key) continue;
                return stats[i];
            }
            return stats[0];
        }
        
        set {
            keys.Add(key);
            stats.Add(value);
        }
    }

    public void Add(string key, Unit.UnitStats statsValue) {
        this[key] = statsValue;
    }

    public void Remove(string key) {
        stats.Remove(this[key]);
        keys.Remove(key);
    }
}