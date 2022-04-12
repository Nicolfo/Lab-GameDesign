using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    [SerializeField] Transform snappingPoint;
    
    RangedWeapon heldWeapon;

    //action to be invoked in pedestal's interactable script
    public void PickAndEquipRangedWeapon() => heldWeapon = GetComponentInChildren<RangedWeapon>();

    public void Shoot() => heldWeapon?.Shoot();
}
