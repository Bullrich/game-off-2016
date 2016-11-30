using UnityEngine;
using System.Collections;
using System;
// By @JavierBullrich

namespace Glitch.Weapons
{
    public class RegularBullet : Bullet
    {
        public override void CollisionDetection(GameObject hit)
        {
            //print(hit.name);
            if (hit.layer == 10 || hit.tag == "Damagable")
                hit.GetComponent<IDamagable>().ReceiveDamage(DamagePerHit);
            gameObject.SetActive(false);
        }
    }
}