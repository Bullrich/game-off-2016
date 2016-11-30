using UnityEngine;
using System.Collections;
using System;
using Glitch.Manager;
// By @JavierBullrich
namespace Glitch.Weapons
{
    public class WpMachineGun : WeaponController
    {
        public override void Fire()
        {
            if (canShoot > shootInterval)
            {
                Bullet bul = GameManagerBase.instance.GetPoolObject("MiniBullet").gObject.GetComponent<Bullet>();
                bul.Spawn(bulletSpawner.position, GetAngleToFire(direction), DamagePerBullet);
                bul.gameObject.SetActive(true);
                canShoot = 0;
                PlaySfx();
            }
        }

        public override void FireUp()
        {
        }
    }
}