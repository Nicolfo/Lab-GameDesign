using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalBehaviour : MonoBehaviour
{
    [SerializeField] RangedWeapon weaponToBeHeld;
    [SerializeField] Transform snappingPoint;

    RangedWeapon heldWeapon;

    private void Awake()
    {
        heldWeapon = Instantiate(
            weaponToBeHeld, 
            snappingPoint.position, 
            snappingPoint.rotation);
        heldWeapon.transform.SetParent(transform);
    }

    //action to be invoked by interactable
    public void GiveOrTakeWeapon(Transform player)
    {
        ResetWeaponParent(player);
        SnapWeaponOnPoint(player);
    }

    private void SnapWeaponOnPoint(Transform player)
    {
        heldWeapon.transform.localPosition =
            heldWeapon.transform.parent.Equals(player) ?
            player.GetChild(0).localPosition
            : snappingPoint.localPosition;

        heldWeapon.transform.localRotation =
            heldWeapon.transform.parent.Equals(player) ?
            player.GetChild(0).localRotation
            : snappingPoint.localRotation;
    }

    private void ResetWeaponParent(Transform player)
    {
        heldWeapon.transform.SetParent(
                    heldWeapon.transform.parent.CompareTag("Player") ?
                    transform
                    : player);
    }
}
