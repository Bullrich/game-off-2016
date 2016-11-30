using UnityEngine;
using System.Collections;
using Glitch.Manager;
// By @JavierBullrich
namespace Glitch.Weapons
{
    public class WpRifle : WeaponController
    {
        public override void Fire()
        {
            if (canShoot > shootInterval)
            {
                Bullet bul = GameManagerBase.instance.GetPoolObject("RegularBullet").gObject.GetComponent<Bullet>();
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