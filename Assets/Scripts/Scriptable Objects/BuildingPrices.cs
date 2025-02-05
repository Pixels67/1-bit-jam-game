using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingPrices", menuName = "Scriptable Objects/BuildingPrices")]
public class BuildingPrices : ScriptableObject {
    public PriceDictionary prices;
}

[System.Serializable]
public class PriceDictionary {
    public List<Building.BuildingType> keys;
    public List<uint> prices;
    
    public uint this[Building.BuildingType key] {
        get {
            for (int i = 0; i < keys.Count; i++) {
                if (keys[i] != key) continue;
                return prices[i];
            }
            return 0;
        }
        
        set {
            keys.Add(key);
            prices.Add(value);
        }
    }

    public void Add(Building.BuildingType key, uint price) {
        this[key] = price;
    }

    public void Remove(Building.BuildingType key) {
        prices.Remove(this[key]);
        keys.Remove(key);
    }
}