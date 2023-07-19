using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : Unit
{
    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private Joystick _aimJoystick;
    public ButtonHold _button;
    private Rigidbody2D _rb2D;

    private void Start()
    {
        base.Initialize("Hunter", 100, 10);
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        Aim();
        if (_button.buttonHeld && !GameManager.Instance._inventory._isSwitched)
        {
            Shoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) // temporary
    {
        //Health health = this.gameObject.GetComponent<Health>();

        //if (health != null)
        //{
        //    health.TakeDamage(5);

        //    GameUI.Instance._hpSlider.value = (float)health.CurrentHealth / (float)health.MaxHealth;
        //    Debug.Log($"{_name} dealt damage to {collision.gameObject.name}");
        //}
    }

    private void Movement()
    {
        _rb2D.velocity = new Vector3(_moveJoystick.Horizontal * _speed, _moveJoystick.Vertical * _speed, 0);
    }

    private void Aim()
    {
        Vector3 moveVector = (Vector3.up * _aimJoystick.Vertical - Vector3.left * _aimJoystick.Horizontal);

        if (_aimJoystick.Horizontal != 0 || _aimJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
        }
    }

    public override void Shoot()
    {

        base.Shoot();
        Debug.Log($"{_name} is Shooting");
        
    }

    public override void Reload()
    {
        base.Reload();
    }

    public void SetCurrentGun(Gun gun)
    {
        _currentGun = gun;

    }
}
