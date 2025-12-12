using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("Socket where the weapon is attached")]
    public Transform weaponSocket;     // Assign this in Inspector

    private GameObject currentWeapon;  // The weapon currently equipped

    /// <summary>
    /// Equips a weapon prefab into the weapon socket.
    /// </summary>
    public void Equip(GameObject weaponPrefab)
    {
        if (weaponSocket == null)
        {
            Debug.LogError("[WeaponHolder] Weapon socket is NOT assigned!");
            return;
        }

        if (weaponPrefab == null)
        {
            Debug.LogError("[WeaponHolder] Equip() called with NULL prefab!");
            return;
        }

        // Remove old weapon
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        // Spawn weapon into socket
        currentWeapon = Instantiate(weaponPrefab, weaponSocket);

        // Snap weapon exactly to socket transform
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.transform.localScale = Vector3.one;
    }

    /// <summary>
    /// Removes current weapon.
    /// </summary>
    public void Unequip()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
            currentWeapon = null;
        }
    }

    /// <summary>
    /// Returns the currently equipped weapon object.
    /// </summary>
    public GameObject GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
