using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private uint damage;

    private void Awake() {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update() {
        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Enemy>() == null) return;
        other.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
