using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _health;
    protected float _damage;
    protected float _range;
    
    protected int _cooldown;

    protected bool _hasLineOfSight;

    protected GameObject _player;

    public SpriteRenderer enemySprite;

    public Rigidbody2D enemyRigidBody;

    public Bullet bulletPrefab;

    protected Bullet _currentBullet;

    protected void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    public virtual void TakeDamage(float damage = 0)
    {
        Debug.Log("Enemy took damage!");
        _health -= damage;
        if(_health <= 0) { Die(); }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected float CalculateDistance(Vector3 position)
    {
        return Vector3.Distance(transform.position, position);
    }

    protected void CalculateLineOfSight(Vector3 position) {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, position - transform.position);
        if(ray.collider is not null)
        {
            _hasLineOfSight = ray.collider.CompareTag("Player") && CalculateDistance(position) <= _range;
            Debug.DrawRay(transform.position, position - transform.position, _hasLineOfSight ? Color.green : Color.red);
        }
    }
    public abstract void Attack();

    protected void Update()
    {
        Attack();
    }
    protected void FixedUpdate()
    {
        CalculateLineOfSight(_player.transform.position);
    }
}