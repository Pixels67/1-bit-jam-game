using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Building : MonoBehaviour {
    public static Action<Building> OnBuildFail;
    
    public enum BuildingType {
        None,
        Snowman,
        Barracks,
        Cannon,
        Collector
    }
    
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.gameObject.CompareTag("Tilemap")) return;
        OnBuildFail?.Invoke(this);
        Destroy(gameObject);
    }
}