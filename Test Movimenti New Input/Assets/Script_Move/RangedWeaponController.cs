using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    [SerializeField] RangedWeapon rangedWeapon;

    public void PickAndEquipRangedWeapon()
    {
        //TODO(Giuseppe): implementare sistema di equipaggiamento armi a distanza
    }

    public void Shoot() => rangedWeapon?.Shoot();
}
