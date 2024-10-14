using UnityEngine;
public class PlayerController : MonoBehaviour, IDamageable
{
    // private variables
    private float _horizontalMovement;
    private float _verticalMovement;
    private float _walkSpeedModifier;
    private float _currentWalkSpeed;
    [SerializeField] private float _defaultWalkSpeed;
    
    private bool _sprinting;
    [SerializeField] private bool _gravity;
    
    private Vector2 _movementVector;

    // public variables
    public float health;
    public float healthMax;
    public float stamina;
    public float staminaMax;

    public SpriteRenderer playerSprite;

    public Rigidbody2D playerBody;

    private void Start()
    {
        playerBody.freezeRotation = true;
        _gravity = true;
        _sprinting = false;
        _defaultWalkSpeed = 250;
        playerBody.gravityScale = 0;
        stamina = 100;
        staminaMax = 100;
        health = 100f;
        healthMax = 100f;
    }

    private void Update()
    {
        if (_gravity) { _walkSpeedModifier = 1f; }
        else { _walkSpeedModifier = 0.7f; }
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0f) { _sprinting = true; }
        if (_horizontalMovement == 0 && _verticalMovement == 0) { _sprinting = false; }
        if (Input.GetKeyUp(KeyCode.LeftShift) || stamina == -50) { _sprinting = false; }
        if (stamina <= 0f) { _walkSpeedModifier -= 0.3f; }
    }
    private void FixedUpdate()
    {
        // Get movement axes
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");

        if (_sprinting) { 
            _walkSpeedModifier += 0.5f;
            stamina -= Time.deltaTime * 11.5f;
            stamina = Mathf.Clamp(stamina, -50, 100);
        }
        else
        {
            stamina += Time.deltaTime * 4.5f;
            stamina = Mathf.Clamp(stamina, -50, 100);
        }

        _currentWalkSpeed = _defaultWalkSpeed * _walkSpeedModifier;

        _movementVector = new Vector2(_horizontalMovement * _currentWalkSpeed * Time.deltaTime, _verticalMovement * _currentWalkSpeed * Time.deltaTime);

        if (_horizontalMovement < 0)
        {  // When moving left, flip sprite to face left
            playerSprite.flipX = true;
        }
        else if (_horizontalMovement > 0)
        { // When moving right, flip sprite to face right
            playerSprite.flipX = false;
        }

        if (_gravity)
        {
            playerBody.velocity = _movementVector;
        }
        else
        {
            playerBody.velocity += _movementVector / 200;
            playerBody.velocity = Vector2.ClampMagnitude(playerBody.velocity, 5f);
        }
    }
    public void TakeDamage(float damage = 0)
    {
        Debug.Log($"Player took {damage} damage!");
        health -= damage;
        if (health <= 0) { Die(); }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D()
    {
        _gravity = false;
    }

    private void OnTriggerExit2D()
    {
        _gravity = true;
    }
}