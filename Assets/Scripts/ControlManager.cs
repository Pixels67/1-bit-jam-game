using System.Linq;
using UnityEngine;

public class ControlManager : MonoBehaviour {
    public enum SelectionState {
        None,
        Snowman
    }
    
    [HideInInspector] public SelectionState selectionState = SelectionState.None;
    
    [SerializeField] private GameObject snowman;
    
    private Camera m_camera;
    private UIManager m_uiManager;

    private void Awake() {
        m_uiManager = FindFirstObjectByType<UIManager>();
        m_camera = Camera.main;
    }

    private void Update() {
        if (selectionState == SelectionState.Snowman) {
            Vector3 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            Vector3 ghostPos = m_camera.WorldToScreenPoint(SnapToGrid(mousePos, 2));
            m_uiManager.SetGhostBuildingPosition(ghostPos);
        }
        
        if (!Input.GetMouseButtonDown(0)) return;
        
        switch (selectionState) {
            case SelectionState.None:
                break;
            case SelectionState.Snowman:
                BuildSnowman();
                break;
        }
    }

    private Vector3 SnapToGrid(Vector3 vector, int gridSize) {
        return new Vector3(
            Mathf.Round(vector.x / gridSize) * gridSize,
            Mathf.Round(vector.y / gridSize) * gridSize,
            0f
        );
    }

    private void BuildSnowman() {
        Vector3 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector3 snowmanPos = SnapToGrid(mousePos, 2);
        
        Snowman[] snowmen = FindObjectsByType<Snowman>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        if (snowmen.Any(otherSnowman => otherSnowman.transform.position == snowmanPos)) return;
        
        Castle castle = FindFirstObjectByType<Castle>();
        if (Vector2.Distance(snowmanPos, castle.transform.position) < 1.5f) return;
        
        Instantiate(snowman, SnapToGrid(mousePos, 2), Quaternion.identity);
        
        selectionState = SelectionState.None;
        m_uiManager.ToggleGhostBuilding();
    }
}
