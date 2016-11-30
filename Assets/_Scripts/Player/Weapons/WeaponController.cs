using UnityEngine;
using System.Collections;
using Glitch.Manager;
// By @JavierBullrich
namespace Glitch.Weapons
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class WeaponController : MonoBehaviour
    {
        public Transform bulletSpawner;
        protected Direction direction;
        protected float canShoot;
        public float shootInterval = .2f;
        public int WeaponId;
        public bool canMove = true;
        [Range(1, 10)]
        public int DamagePerBullet;

        public abstract void Fire();
        public abstract void FireUp();
        Vector3 regularScale, invertedScale;
        private AudioSource audioSource;
        SoundManager sManager;

        void Awake()
        {
            bulletSpawner = transform.GetChild(0);
            regularScale = transform.localScale;
            invertedScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            sManager = (SoundManager)GameManagerBase.instance.getSFX().script;
        }

        void Update()
        {
            if (canShoot < shootInterval)
                canShoot += Time.deltaTime;
        }

        protected void PlaySfx()
        {
            audioSource.clip = sManager.getSfx(SoundManager.Sfx.shoot);
            audioSource.Play();
        }

        protected int GetAngleToFire(Direction dir)
        {
            if (transform.localScale.x < 0)
            {
                if (dir == Direction.Left)
                    dir = Direction.Right;
                else if (dir == Direction.Right)
                    dir = Direction.Left;
            }
            return (int)dir * 90;
        }

        #region Handle Weapon Rotation
        public enum Direction
        {
            Right,
            Up,
            Left,
            Down
        }

        public void TurnWeapon(Vector2 dir, bool isWallSliding)
        {
            direction = VectorToDirection(dir);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation(direction)));
            if (isWallSliding)
                transform.localScale = invertedScale;
            else
                transform.localScale = regularScale;
        }

        public Direction VectorToDirection(Vector2 vec)
        {
            if (vec == Vector2.zero && direction != Direction.Down && direction != Direction.Up)
                return direction;
            else
            {
                Direction newDirection;
                if (Mathf.Abs(vec.y) > 0.2)
                {
                    if (vec.y > 0.2)
                        newDirection = Direction.Up;
                    else
                        newDirection = Direction.Down;
                }
                else
                {
                    if (vec.x > 0 || transform.parent.transform.lossyScale.x > 0)
                        newDirection = Direction.Right;
                    else
                        newDirection = Direction.Left;
                }
                return newDirection;
            }
        }

        public void SwitchedWeapon(bool right)
        {
            if (!right)
                TurnWeapon(new Vector2(-1, 0), false);
        }

        protected float rotation(Direction dir)
        {
            float rotationAngle = 0;
            if (dir == Direction.Up)
                rotationAngle = 90;
            else if (dir == Direction.Left)
                rotationAngle = 0;
            else if (dir == Direction.Down)
                rotationAngle = 270;
            return rotationAngle;
        }
        #endregion
    }
}