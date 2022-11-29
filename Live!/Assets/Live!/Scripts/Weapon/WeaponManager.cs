using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class WeaponManager : MonoBehaviour
{
    #region References
    [SerializeField] private WeaponDataSO _weapon;
    #endregion

    #region Functions

    #region Unity Built-in
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Later give ammo instead of ignoring
            if (Weapon.Instance.PrimaryWeapon == _weapon || Weapon.Instance.SecondaryWeapon == _weapon) return;
            EquipWeapon();
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //If the item is a pickup ground item then show it
        GameObject gun = Instantiate(_weapon.Weapon, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        gun.transform.parent = this.transform;
    }
    #endregion

    private void CreateWeapon()
    {
        //Create weapon model for picked up/created weapon
        void ShowModel(GameObject weapon)
        {
            weapon = Instantiate(_weapon.Weapon, new Vector3(0, 0, 0), Quaternion.identity);
            weapon.transform.SetParent(Weapon.Instance.Hand, false);
        }

        if(Weapon.Instance.ActiveWeapon == Weapon.Instance.PrimaryWeapon)
        {
            ShowModel(Weapon.Instance.PrimaryGun);
        }
        else
        {
            ShowModel(Weapon.Instance.SecondaryGun);
        }
    }

    public void EquipWeapon()
    {

        //Set picked up weapon to secondary weapon if we dont have one already
        if (Weapon.Instance.ActiveWeapon == Weapon.Instance.PrimaryWeapon && Weapon.Instance.SecondaryWeapon != null)
        {
            Weapon.Instance.PrimaryWeapon = _weapon;
            Weapon.Instance.ActiveWeapon = _weapon;
            Destroy(Weapon.Instance.PrimaryGun);
            CreateWeapon();
        }
        else
        {
            //This really only applies to the start of the game when you don't have a secondary weapon
            if(Weapon.Instance.SecondaryGun == null) Weapon.Instance.PrimaryGun.SetActive(false);

            Weapon.Instance.SecondaryWeapon = _weapon;
            Weapon.Instance.ActiveWeapon = _weapon;
            Destroy(Weapon.Instance.SecondaryGun);
            CreateWeapon();
        }

    }
    #endregion
}
