using System;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour, IFormattable
{
    public PlayerController _playerController;
    public WeaponController _weaponController;

    public Text healthText;
    public Text staminaText;
    public Text ammoText;
    public string ToString(string format, IFormatProvider formatProvider)
    {
        switch (format)
        {
            case "H": // Health formatting
                return $"Health: {_playerController.health} / {_playerController.healthMax}";

            case "S" when _playerController.stamina < 0:
                return $"Stamina: {_playerController.stamina:F2} / {_playerController.staminaMax} (Exhausted!)";

            case "S": // Stamina normal range
                return $"Stamina: {_playerController.stamina:F2} / {_playerController.staminaMax}";

            case "A": // Ammo formatting
                string reloadingText = _weaponController.isReloading ? " (Reloading)" : "";
                return $"Ammo: {_weaponController.ammo} / {_weaponController.ammoMax}{reloadingText}";

            default:
                return "";
        }
    }

    private void Update()
    {
        UpdateHud();
    }

    private void UpdateHud()
    {
        healthText.text = this.ToString("H", null);
        staminaText.text = this.ToString("S", null);
        ammoText.text = this.ToString("A", null);
    }
}
