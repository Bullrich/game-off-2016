using UnityEngine;
using System.Collections;
// By @JavierBullrich
namespace Glitch.Weapons
{
    public class FastBullet : Bullet
    {
        public override void CollisionDetection(GameObject hit)
        {
            if (hit.layer == 10 || hit.tag == "Damagable")
                hit.GetComponent<IDamagable>().ReceiveDamage(DamagePerHit);
            gameObject.SetActive(false);
        }

    }
}