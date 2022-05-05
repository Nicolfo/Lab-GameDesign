using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [SerializeField] float msBetweenShots = 200f;
    [SerializeField] Transform firePoint;
    [SerializeField] Projectile projectile;
    [SerializeField] float projectileSpeed = 20f;

    float nextShotTime;

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000f;

            Projectile newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
            newProjectile.SetSpeed(projectileSpeed);

        }
    }
}
