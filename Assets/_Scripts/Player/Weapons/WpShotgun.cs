using UnityEngine;
using System.Collections;
using Glitch.Manager;
// By @JavierBullrich
namespace Glitch.Weapons
{
    public class WpShotgun : WeaponController
    {
        public int bulletsPerShot = 5;

        public override void Fire()
        {
            int shoots = bulletsPerShot / 2;
            if (canShoot > shootInterval)
            {
                for (int i = -shoots; i < shoots + 1; i++)
                {
                    Bullet bul = GameManagerBase.instance.GetPoolObject("RegularBullet").gObject.GetComponent<Bullet>();
                    bul.Spawn(bulletSpawner.position, GetAngleToFire(direction) + (i * 5), DamagePerBullet);
                    bul.gameObject.SetActive(true);
                    canShoot = 0;
                    PlaySfx();
                }
            }
        }
        public override void FireUp()
        {

        }

    }
}