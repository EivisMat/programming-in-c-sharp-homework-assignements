using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    public Rigidbody2D bulletBody;

    private void Start()
    {
        bulletBody.gravityScale = 0f;
        bulletBody.isKinematic = false;
    }

    public void Fire(Vector3 direction, float magnitude = 1500f)
    {
        bulletBody.AddForce(magnitude * Time.deltaTime * direction.normalized);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Damageable"))
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable.TakeDamage(damage: 25);
        }
        Destroy(gameObject);
    }
}
