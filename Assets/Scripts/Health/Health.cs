using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHittable
{
    [SerializeField]
    private float _maxHealth = 100;
    [SerializeField]
    private float _currentHealth = 20;
    [SerializeField]
    private float _minHealth = 0;
    [SerializeField]
    private bool _startWithMax;

    public float Max
    {
        get { return _maxHealth; }
        set { 
            if (value <= _minHealth) 
                _maxHealth = _minHealth + 1f;
            else
                _maxHealth = value;
        }
    }

    public float Current
    {
        get { return _currentHealth; }
        set
        {
            if (value <= _minHealth)
                _maxHealth = _minHealth;
            else if (value > _maxHealth)
                _currentHealth = _maxHealth;
            else
                _currentHealth = value;
        }
    }

    public float Min
    {
        get { return _minHealth; }
        set
        {
            if (value < 0)
                _minHealth = 0;
            else
                _minHealth = value;
        }
    }

    private void Start()
    {
        if (_startWithMax)
            _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (Current <= Min)
            Destroy(gameObject);
    }

    private void Damage(float damage)
    {
        _currentHealth -= damage;
    }

    private void Heal(float heal)
    {
        _currentHealth += heal;
    }

    public void Hit(float impulse)
    {
        Damage(impulse);
    }
}
