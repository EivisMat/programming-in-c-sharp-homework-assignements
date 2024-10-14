using UnityEngine;
public class CameraController : MonoBehaviour
{   
    public Rigidbody2D playerBody;
    public Rigidbody2D cameraBody;

    [SerializeField] private float _zAxisOffset;
    [SerializeField, Range(0f, 1f)] private float _speed;
    private void Start()
    {
        cameraBody.gravityScale = 0f;
    }

    void Update() {
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, _zAxisOffset), new Vector3(playerBody.position.x, playerBody.position.y, _zAxisOffset), _speed * Time.deltaTime);
    }
}