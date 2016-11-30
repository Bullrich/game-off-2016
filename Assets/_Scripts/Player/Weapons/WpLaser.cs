using UnityEngine;
using Glitch.Manager;
using Glitch.GameCamera;
using System.Collections;
using System;
// By @JavierBullrich
namespace Glitch.Weapons
{
    public class WpLaser : WeaponController
    {
        public Transform Laser, LaserEnd;
        ParticleSystem particles;
        ParticleSystem.EmissionModule em;
        public float LaserSpeed;
        float DamageTime;

        void Start()
        {
            Laser = bulletSpawner.GetChild(0);
            LaserEnd = Laser.GetChild(0);
            particles = transform.GetChild(1).GetComponent<ParticleSystem>();
            em = particles.emission;
            em.enabled = false;
            bulletSpawner.gameObject.SetActive(false);
        }

        public override void FireUp()
        {
            Laser.localScale = new Vector3(0, Laser.localScale.y);
            canFire = true;
            CameraShake.canShake = false;
            canMove = true;
            em.enabled = false;
            particles.Clear();
            bulletSpawner.gameObject.SetActive(false);
            DamageTime = 0;
        }

        bool canFire = true;
        public override void Fire()
        {
            if (canFire)
                Laser.transform.localScale = new Vector3(Laser.transform.localScale.x + (LaserSpeed * Time.deltaTime), Laser.transform.localScale.y);
            CameraShake.canShake = true;
            canMove = false;
            bulletSpawner.gameObject.SetActive(true);
            LaserCollisions();
        }

        public LayerMask collisionMask;
        GameObject lastHittedTarget;
        void LaserCollisions()
        {
            /*float lineLenght = .05f;
            lineLenght = Laser.transform.localScale.x / 1.4f;
            lineLenght = Vector2.Distance(bulletSpawner.position, LaserEnd.position);
            print(lineLenght + " " + Vector2.right * GetAngleToFire(direction));//*/

            RaycastHit2D hit = new RaycastHit2D();

            switch (direction)
            {
                case Direction.Right:
                    hit = Physics2D.Raycast(bulletSpawner.position, Vector2.right, collisionMask);
                    break;
                case Direction.Up:
                    hit = Physics2D.Raycast(bulletSpawner.position, Vector2.up, collisionMask);
                    break;
                case Direction.Left:
                    hit = Physics2D.Raycast(bulletSpawner.position, Vector2.left, collisionMask);
                    break;
                case Direction.Down:
                    hit = Physics2D.Raycast(bulletSpawner.position, Vector2.down, collisionMask);
                    break;
                default:
                    break;
            }
            if (hit)
            {
                Debug.DrawLine(bulletSpawner.position, hit.point, Color.red);
                print("Hitted " + hit.transform.name);
                if (hit.transform.gameObject.layer == 8 && Vector2.Distance(LaserEnd.position, hit.point) < 0.2f)
                {
                    canFire = false;
                    em.enabled = true;
                    particles.transform.position = LaserEnd.position;
                }
                lastHittedTarget = hit.transform.gameObject;
                if (hit.transform.gameObject.layer == 10)
                    DamageTarget(hit.transform.GetComponent<IDamagable>());
            }
            else if (lastHittedTarget != null)
            {
                lastHittedTarget = null;
                FireUp();
            }
            else
            {
                particles.Clear();
            }
        }

        void DamageTarget(IDamagable target)
        {
            if (DamageTime < 0.2f)
                DamageTime += GameManagerBase.DeltaTime;
            else
            {
                target.ReceiveDamage(DamagePerBullet);
                DamageTime = 0;
            }
        }
    }
}