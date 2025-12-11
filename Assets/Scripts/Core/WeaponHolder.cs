using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Transform weaponSocket;   // assign from inspector
    private GameObject currentWeapon;

    public void Equip(GameObject weaponPrefab)
    {
        // Delete previous weapon
        if (currentWeapon != null)
            Destroy(currentWeapon);

        // Spawn new one
        currentWeapon = Instantiate(weaponPrefab, weaponSocket);

        // Reset local transform to match socket
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.transform.localScale = Vector3.one;
    }
}
