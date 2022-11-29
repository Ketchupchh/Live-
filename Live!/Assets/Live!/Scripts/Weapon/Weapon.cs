using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private static Weapon _instance;
    public static Weapon Instance => _instance;

    #region References
    public Transform Hand;
    #endregion

    #region Getters & Setters
    public GameObject PrimaryGun { get { return _primaryGun; } set { _primaryGun = value; } }
    public GameObject SecondaryGun { get { return _secondaryGun; } set { _secondaryGun = value; } }
    public WeaponDataSO PrimaryWeapon { get { return _primaryWeapon; } set { _primaryWeapon = value; } }
    public WeaponDataSO SecondaryWeapon { get { return _secondaryWeapon; } set { _secondaryWeapon = value; } }
    public WeaponDataSO ActiveWeapon { get { return _activeWeapon; } set { _activeWeapon = value; } }
    #endregion

    #region Variables
    private GameObject _primaryGun;
    private GameObject _secondaryGun;

    [SerializeField] private WeaponDataSO _primaryWeapon;
    [SerializeField] private WeaponDataSO _secondaryWeapon;
    [SerializeField] private WeaponDataSO _activeWeapon = null;
    #endregion

    #region Functions

    #region Unity Built-in
    void Init()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        if(_primaryWeapon != null) _activeWeapon = _primaryWeapon;

    }

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        PrimaryGun = Instantiate(_primaryWeapon.Weapon, new Vector3(0, 0, 0), Quaternion.identity);
        PrimaryGun.transform.SetParent(Hand, false);
    }

    private void Update()
    {
        SwapWeapon();

        if (Input.GetMouseButtonDown(0) && _activeWeapon != null)
        {
            _activeWeapon.Shoot(Player.Instance.Cam);
        }

        if (Input.GetKeyDown(KeyCode.R) && _activeWeapon != null)
        {
            _activeWeapon.Reload();
        }
    }
    #endregion


    private void SwapWeapon()
    {
        //Swap Weapons
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_activeWeapon != _primaryWeapon)
            {
                _activeWeapon = _primaryWeapon;
                if (_primaryGun != null) _primaryGun.SetActive(true);
                if (_secondaryGun != null) _secondaryGun.SetActive(false);
            }
            else
            {
                if (_secondaryGun == null) return;
                _activeWeapon = _secondaryWeapon;
                if (_secondaryGun != null) _secondaryGun.SetActive(true);
                if (_primaryGun != null) _primaryGun.SetActive(false);
            }
        }
    }

    #endregion
}
