using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class Unit : MonoBehaviour
{
    public Gun _currentGun;
    public List<Unit> targetList = new List<Unit>();

    [SerializeField] protected Health _health;

    [SerializeField] protected string _name;
    [SerializeField] protected float _speed;

    [SerializeField] protected List<GameObject> _guns;
    [SerializeField] private GameObject _enemyHealthBar;
    [SerializeField] private Slider _enemyHpSlider;
    [SerializeField] protected float patrolRadius;
    private GameObject _enemyHealthBarHolder;

    protected Vector2 targetDestination;
    protected float _patrolWaitTime;
    protected float _switchDirectionTime;
    protected Quaternion targetRotation;

    protected Unit unit;
    protected bool isWithinRange;

    private void Update()
    {
        if (_enemyHealthBarHolder != null)
        {
            _enemyHealthBarHolder.transform.position = transform.position + new Vector3(0, 1, 0);
            _enemyHealthBarHolder.transform.rotation = Quaternion.identity;
        }
    }

    public void Initialize(string name, int maxHealth, float speed)
    {
        _name = name;
        gameObject.name = _name;

        _health = gameObject.GetComponent<Health>();
        _health.Initialize(maxHealth);

        _speed = speed;

        Debug.Log($"{name} has been initialized");
    }

    public virtual void Shoot()
    {
        if (_currentGun != null)
        {
            Debug.Log($"{_name} is shooting");
            _currentGun.Shoot();
        }
    }

    public virtual void Reload()
    {
        _currentGun.Reload();
    }

    public void ManageEnemyHealth()
    {
        if (_enemyHealthBar != null)
        {
            _enemyHpSlider.enabled = false;
            _enemyHealthBarHolder.SetActive(true);
            _enemyHpSlider.value = (float)_health.CurrentHealth / (float)_health.MaxHealth;
            if(_health.CurrentHealth <= 0)
            {
                GameUI.Instance.spawner._enemies.Remove(this);
                GameUI.Instance.UpdateEnemyCount();
            }
        }
    }

    protected void Patrol()
    {
        if (_switchDirectionTime > 0)
        {
            _switchDirectionTime -= Time.deltaTime;
        }

        if (_switchDirectionTime <= 0)
        {
            Vector2 randomPoint = Random.insideUnitCircle * patrolRadius;
            targetDestination = (Vector2)transform.position + randomPoint;
            _switchDirectionTime = 10;
        }

        if (_patrolWaitTime > 0)
        {
            _patrolWaitTime -= Time.deltaTime;
        }

        if (Vector2.Distance(transform.position, targetDestination) < 0.1f)
        {
            if (_patrolWaitTime <= 0)
            {
                Vector2 randomPoint = Random.insideUnitCircle * patrolRadius;
                targetDestination = (Vector2)transform.position + randomPoint;

                _patrolWaitTime = 3;
                _switchDirectionTime = 10;

                Vector2 direction = ((Vector2)transform.position - targetDestination).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                targetRotation = Quaternion.Euler(0f, 0f, angle);
                targetRotation *= Quaternion.Euler(0f, 0f, -180f);
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        transform.position = Vector2.MoveTowards(transform.position, targetDestination, _speed * Time.deltaTime);
    }

    protected void MoveTowardsTarget(float attackRange)
    {
        if (!isWithinRange)
        {
            Patrol();
        }
        else if (targetList != null && isWithinRange)
        {
            if (targetList.Count <= 0)
            {
                isWithinRange = false;
                return;
            }

            if (Vector2.Distance(targetList[0].transform.position, this.transform.position) >= attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetList[0].transform.position, _speed * Time.deltaTime);
            }
            else
            {
                Shoot();
            }
            transform.right = targetList[0].transform.position - transform.position;
        }
    }

    protected void InsantiateEnemyHealthBar()
    {
        _enemyHealthBarHolder = Instantiate(_enemyHealthBar, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
        _enemyHealthBarHolder.SetActive(false);
        _enemyHpSlider = _enemyHealthBarHolder.transform.GetChild(0).GetComponentInChildren<Slider>();
    }
}
