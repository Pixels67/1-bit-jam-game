using System.Linq;
using UnityEngine;

public class Builder : MonoBehaviour {
    [SerializeField] private GameObject snowman;
    [SerializeField] private GameObject barracks;
    [SerializeField] private GameObject cannon;
    [SerializeField] private GameObject collector;
    [SerializeField] private BuildingPrices buildingPrices;
    [SerializeField] private AudioClip buildSFX;

    private Castle m_castle;

    private void Awake() {
        m_castle = FindFirstObjectByType<Castle>();
        Building.OnBuildFail += OnBuildFailed;
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
        if (buildings.Any(otherBuilding => (otherBuilding.transform.position - position).magnitude < 1.1f)) return;

        var castle = FindFirstObjectByType<Castle>();
        if (Vector2.Distance(position, castle.transform.position) < 1.5f) return;

        position.z = -1f;
        Instantiate(building, position, Quaternion.Euler(0f, 0f, rotation));
        
        m_castle.snow -= buildingPrices.prices[buildingType];
        
        SoundEffectsManager.instance.PlaySFXClip(buildSFX, position);
    }

    private void OnBuildFailed(Building building) {
        Building.BuildingType buildingType = GetBuildingType(building);
        m_castle.snow += buildingPrices.prices[buildingType];
    }

    private Building.BuildingType GetBuildingType(Building building) {
        if (building.GetComponent<Snowman>()   != null) return Building.BuildingType.Snowman;
        if (building.GetComponent<Barracks>()  != null) return Building.BuildingType.Barracks;
        if (building.GetComponent<Cannon>()    != null) return Building.BuildingType.Cannon;
        if (building.GetComponent<Collector>() != null) return Building.BuildingType.Collector;
        
        return 0;
    }
}