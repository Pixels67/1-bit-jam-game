using System.Linq;
using UnityEngine;

public class Builder : MonoBehaviour {
    [SerializeField] private GameObject snowman;
    [SerializeField] private GameObject barracks;
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject collector;
    [SerializeField] private BuildingPrices buildingPrices;

    private Castle m_castle;

    private void Awake() {
        m_castle = FindFirstObjectByType<Castle>();
    }

    public void Build(Building.BuildingType buildingType, Vector3 position, float rotation = 0f) {
        if (buildingPrices.prices[buildingType] > m_castle.snow) return;
        
        var building = buildingType switch {
            Building.BuildingType.Snowman => snowman,
            Building.BuildingType.Barracks => barracks,
            Building.BuildingType.Cannon => cannon,
            Building.BuildingType.Collector => collector,
            _ => null
        };

        var buildings = FindObjectsByType<Building>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        if (buildings.Any(otherBuilding => otherBuilding.transform.position == position)) return;

        var castle = FindFirstObjectByType<Castle>();
        if (Vector2.Distance(position, castle.transform.position) < 1.5f) return;

        position.z = -1f;
        Instantiate(building, position, Quaternion.Euler(0f, 0f, rotation));
        
        m_castle.snow -= buildingPrices.prices[buildingType];
    }
}