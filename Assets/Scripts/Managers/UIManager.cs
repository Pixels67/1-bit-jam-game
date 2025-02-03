using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject buildingPanel;
    [SerializeField] private GameObject ghostBuilding;

    private void Awake() {
        buildingPanel.SetActive(false);
        ghostBuilding.SetActive(false);
    }

    public void ToggleBuildingPanel() {
        buildingPanel.SetActive(!buildingPanel.activeSelf);
    }

    public void ToggleGhostBuilding() {
        ghostBuilding.SetActive(!ghostBuilding.activeSelf);
    }

    public void SetGhostBuildingPosition(Vector3 position) {
        ghostBuilding.transform.position = position;
    }

    public void OnSnowmanButtonClicked() {
        var controlManager = FindFirstObjectByType<ControlManager>();
        controlManager.selectionState = ControlManager.SelectionState.Snowman;
        ToggleGhostBuilding();
    }
}