using System.Collections;
using UnityEngine;
public class WeaponController : MonoBehaviour
{
    // delegate
    public delegate IEnumerator ReloadDelegate(int time, int ammoToReload);

    // private variables
    private Vector3 _mousePosition;
    private Vector3 _weaponPosition;
    private Vector3 _direction;

    [SerializeField] private Bullet _bulletPrefab;
    private Bullet _currentBullet;
    
    // public variables
    public int ammo;
    public int ammoMax;

    public bool isReloading;

    public GameObject player;

    public ReloadDelegate Reload;

    public void Start()
    {
        ammo = 10;
        ammoMax = 15;
        Reload = ReloadProcess;
    }

    private void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.z = 0;
        _direction = (_mousePosition - player.transform.position).normalized;
        _weaponPosition = player.transform.position + _direction * 4.5f;
        transform.position = _weaponPosition;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        if (Input.GetMouseButtonUp(0) && !isReloading)
        {
            if (ammo <= 0) { return; }
            if (_currentBullet is not null) { _currentBullet = null; }
            ammo -= 1;
            _currentBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));
            _currentBullet.Fire(direction: _direction, magnitude: 50000f);
            _currentBullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload(time: 5, ammoToReload: ammoMax - ammo));
        }
    }

    private IEnumerator ReloadTimer(int time = 5)
    {
        yield return new WaitForSeconds(time);
    }

    private void ReloadAmmo(int ammoToReload = 1)
    {    
        ammo = Mathf.Clamp(ammo + ammoToReload, 0, ammoMax);
    }
    private IEnumerator ReloadProcess(int time = 0, int ammoToReload = 1)
    {
        isReloading = true;
        yield return StartCoroutine(ReloadTimer(time));

        isReloading = false;
        ReloadAmmo(ammoToReload);
    }
}

