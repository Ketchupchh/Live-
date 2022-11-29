using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, IKillable, IDamageable
{
    [SerializeField] private float _health = 20;
    [SerializeField] private float _damage;

    void IDamageable.Damage(float damage)
    {
        _health -= damage;
    }

    void IKillable.Kill()
    {
        if (_health > 0) return;

        Destroy(gameObject);
        RoundManager.Instance.Zombies.Remove(this);
    }

    private void Update()
    {
    }
}
