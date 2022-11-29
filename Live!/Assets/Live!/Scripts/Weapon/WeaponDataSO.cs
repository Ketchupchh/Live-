using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponDataSo")]
public class WeaponDataSO : ScriptableObject
{
    #region References
    public GameObject Weapon;
    #endregion

    #region Getters & Setters

    public int CurrentClipSize => _currentClipSize;
    public int AmmoReserve => _ammoReserve;

    #endregion

    #region Variables
    [Header("Default Values")]
    [Tooltip("How fast gun can shoot.")]
    [SerializeField] private float _defaultFireRate;
    [Tooltip("How big the max clip size can be.")]
    [SerializeField] private int _defaultMaxClipSize;
    [Tooltip("The Max amount of ammo in our reserve.")]
    [SerializeField] private int _defaultAmmoReserve;
    [Tooltip("Clip currently loaded in gun.")]
    [SerializeField] private int _defaultCurrentClipSize;
    [Tooltip("The range gun can shoot.")]
    [SerializeField] private int _defaultRange;
    [Tooltip("Damage fun deals")]
    [SerializeField] private float _defaultDamage;

    [Header("Current instance values")]
    [SerializeField] private float _fireRate;
    [SerializeField] private int _maxClipSize;
    [SerializeField] private int _ammoReserve;
    [SerializeField] private int _currentClipSize;
    [SerializeField] private float _damage;
    #endregion

    #region Functions

    #region Unity Built-in
    private void OnEnable()
    {
       ResetValues();
    }
    #endregion
    private void ResetValues()
    {
        _maxClipSize = _defaultMaxClipSize;
        _ammoReserve = _defaultAmmoReserve;
        _currentClipSize = _defaultCurrentClipSize;
        _damage = _defaultDamage;
    }

    public void Shoot(Camera cam)
    {
        if (_currentClipSize <= 0) return;

        --_currentClipSize;
        
        //Check for what we hit
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, _defaultRange))
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.TryGetComponent(out IDamageable obj))
            {
                obj.Damage(_damage);
            }
            if(hit.transform.TryGetComponent(out IKillable obj2))
            {
                obj2.Kill();
            }
        }
    }

    public void Reload()
    {
        if (_ammoReserve <= 0) return;

        //Calculate how much to reload by
        int reloadAmount = Mathf.Abs(_currentClipSize - _maxClipSize);

        //Reload amount is more than whats in reserve
        if(reloadAmount > _ammoReserve)
        {
            _currentClipSize += _ammoReserve;
            _ammoReserve = 0;
            return;
        }

        //How much we should have in our clip is more than max
        if(_currentClipSize + reloadAmount > _maxClipSize)
        {
            //Calculate how much is extra
            int extra = Mathf.Abs(_maxClipSize - (_currentClipSize + reloadAmount));
            _ammoReserve += extra;
            _ammoReserve -= _maxClipSize;
            _currentClipSize = _maxClipSize;
            return;
        }

        _ammoReserve -= reloadAmount;
        _currentClipSize += reloadAmount;
    }
    #endregion
}