using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [Header("UI Elements")]
    [SerializeField] private GameObject buildingPanel;
    [SerializeField] private GameObject ghostBuilding;
    [SerializeField] private TMP_Text castleHPText;
    [SerializeField] private TMP_Text snowText;
    [Header("Sprites")]
    [SerializeField] private Sprite snowmanSprite;
    [SerializeField] private Sprite barracksSprite;
    [SerializeField] private Sprite cannonSprite;
    [SerializeField] private Sprite collectorSprite;
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private float transitionTime;
    [Header("Input")]
    [SerializeField] private KeyCode pauseKey;
    [SerializeField] private KeyCode rotateKey;
    [SerializeField] private KeyCode cancelBuildingKey;
    [Header("References")]
    [SerializeField] private Builder builder;
    [SerializeField] private Castle castle;
    
    [HideInInspector] public Building.BuildingType selectionState = Building.BuildingType.None;

    private float m_buildingAngle;
    private Camera m_camera;
    private Image m_ghostBuildingImage;
    private CanvasGroup m_pauseOverlayCanvasGroup;
    private bool m_isPaused;

    private void Awake() {
        buildingPanel.SetActive(false);
        ghostBuilding.SetActive(false);
        m_ghostBuildingImage = ghostBuilding.GetComponent<Image>();
        m_camera = Camera.main;
        
        pauseOverlay.SetActive(false);
        m_pauseOverlayCanvasGroup = pauseOverlay.GetComponent<CanvasGroup>();
        m_pauseOverlayCanvasGroup.alpha = 0f;
    }

    private void Update() {
        castleHPText.text = "HP: " + castle.CurrentHealth;
        snowText.text = "Snow: " + castle.snow;
        
        if (Input.GetKeyDown(pauseKey) && selectionState == Building.BuildingType.None)
            OnPausePressed();
        
        HandleSelection();

        if (Input.GetKeyDown(rotateKey)) {
            m_buildingAngle += 90f;
            m_buildingAngle %= 360f;
        }

        if (selectionState == Building.BuildingType.Cannon) {
            ghostBuilding.transform.localEulerAngles = new Vector3(0f, 0f, m_buildingAngle);
        }
        else {
            ghostBuilding.transform.localEulerAngles = Vector3.zero;
        }

        if (!Input.GetMouseButtonDown(0) || selectionState == Building.BuildingType.None) return;

        builder.Build(
            selectionState,
            GetMouseGridPosition(),
            selectionState == Building.BuildingType.Cannon ? m_buildingAngle : 0f
        );
        
        selectionState = Building.BuildingType.None;
        ToggleGhostBuilding();
    }

    private void HandleSelection() {
        if (selectionState == Building.BuildingType.None) return;
        
        SetGhostBuildingSprite(selectionState);
        
        var mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        var ghostPos = m_camera.WorldToScreenPoint(SnapToGrid(mousePos, 2));
        ghostBuilding.transform.position = ghostPos;

        if (!Input.GetKeyDown(cancelBuildingKey)) return;
        
        selectionState = Building.BuildingType.None;
        ToggleGhostBuilding();
    }

    private Vector3 SnapToGrid(Vector3 vector, int gridSize) {
        return new Vector3(
            Mathf.Round(vector.x / gridSize) * gridSize,
            Mathf.Round(vector.y / gridSize) * gridSize,
            0f
        );
    }

    private Vector3 GetMouseGridPosition() {
        var mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        return SnapToGrid(mousePos, 2);
    }

    public void ToggleBuildingPanel() {
        buildingPanel.SetActive(!buildingPanel.activeSelf);
    }

    private void ToggleGhostBuilding() {
        ghostBuilding.SetActive(!ghostBuilding.activeSelf);
    }
    
    private void SetGhostBuildingSprite(Building.BuildingType sprite) {
        m_ghostBuildingImage.sprite = sprite switch {
            Building.BuildingType.Snowman => snowmanSprite,
            Building.BuildingType.Barracks => barracksSprite,
            Building.BuildingType.Cannon => cannonSprite,
            Building.BuildingType.Collector => collectorSprite,
            _ => m_ghostBuildingImage.sprite
        };
    }
    
    private void OnPausePressed() {
        StopAllCoroutines();
        StartCoroutine(m_isPaused ? Unpause() : Pause());
    }
    
    private IEnumerator Pause() {
        m_isPaused = true;

        Time.timeScale = 0f;
        pauseOverlay.SetActive(true);

        for (var f = 0f; f < transitionTime; f += Time.unscaledDeltaTime) {
            var value = f / transitionTime;

            m_pauseOverlayCanvasGroup.alpha = Mathf.Lerp(m_pauseOverlayCanvasGroup.alpha, 1f, value);
            yield return null;
        }
    }

    private IEnumerator Unpause() {
        m_isPaused = false;
        Time.timeScale = 1f;

        for (var f = 0f; f < transitionTime; f += Time.unscaledDeltaTime) {
            var value = f / transitionTime;

            m_pauseOverlayCanvasGroup.alpha = Mathf.Lerp(m_pauseOverlayCanvasGroup.alpha, 0f, value);
            yield return null;
        }

        pauseOverlay.SetActive(false);
    }

    public void OnSnowmanButtonClicked() {
        selectionState = Building.BuildingType.Snowman;
        ToggleGhostBuilding();
    }

    public void OnBarracksButtonClicked() {
        selectionState = Building.BuildingType.Barracks;
        ToggleGhostBuilding();
    }
    
    public void OnCannonButtonClicked() {
        selectionState = Building.BuildingType.Cannon;
        ToggleGhostBuilding();
    }
    
    public void OnCollectorButtonClicked() {
        selectionState = Building.BuildingType.Collector;
        ToggleGhostBuilding();
    }
}