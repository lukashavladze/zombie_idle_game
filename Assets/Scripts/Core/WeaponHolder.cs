using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Transform weaponSlot;  // Assign hand_r
    public GameObject currentWeapon;

    public void Equip(GameObject weaponPrefab)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(weaponPrefab, weaponSlot);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}
