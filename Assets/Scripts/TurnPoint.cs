using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TurnPoint : MonoBehaviour {
    public Vector2 direction;
    public float radius = 0.1f;

    private void Awake() {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if (other.GetComponent<Enemy>() == null) return;
        if ((other.transform.position - transform.position).magnitude > radius) return;
        other.GetComponent<Enemy>().ChangeDirection(direction);
    }
}
