using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _ammoText;
    [SerializeField] private TMPro.TextMeshProUGUI _ammoReserveText;
    [SerializeField] private TMPro.TextMeshProUGUI _roundsText;

    void UpdateText()
    {
        _ammoText.text = string.Format("{0}", Weapon.Instance.ActiveWeapon.CurrentClipSize);
        _ammoReserveText.text = string.Format("{0}", Weapon.Instance.ActiveWeapon.AmmoReserve);
        _roundsText.text = string.Format("{0}", RoundManager.Instance.Round);
    }

    private void Update()
    {
        UpdateText();
    }
}
