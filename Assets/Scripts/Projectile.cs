using Unit;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private uint damage;
    [SerializeField] private float lifeTime;
    [SerializeField] private AudioClip hitSFX;

    private float m_timer;

    private void Awake() {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update() {
        m_timer += Time.deltaTime;
        transform.Translate(speed * Time.deltaTime * Vector2.right);
        if (m_timer >= lifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Enemy>() == null) return;
        other.GetComponent<Enemy>().TakeDamage(damage);
        SoundEffectsManager.instance.PlaySFXClip(hitSFX, transform.position);
        Destroy(gameObject);
    }
}