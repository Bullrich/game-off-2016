using UnityEngine;
using System.Collections;
// By @JavierBullrich
namespace Glitch.Weapons
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Bullet : MonoBehaviour
    {
        public LayerMask collisionMasks;
        Transform _tran;
        public float speed, rayLenght = .1f;
        protected int DamagePerHit;

        void Awake()
        {
            _tran = GetComponent<Transform>();
            float sprLength = GetComponent<SpriteRenderer>().sprite.rect.x;
        }

        void Update()
        {
            VerticalCollisions();
            _tran.Translate(Vector2.right * Time.deltaTime * speed);
        }

        public void Spawn(Vector3 position, float rotationZ, int damage)
        {
            _tran.position = position;
            _tran.rotation = Quaternion.Euler(new Vector3(0, 0, rotationZ));
            DamagePerHit = damage;
            Invoke("Despawn", 14f);
        }

        void Despawn()
        {
            gameObject.SetActive(false);
        }

        // Checks when the coin distance to the ground is short enough to "collide"
        void VerticalCollisions()
        {

            Debug.DrawLine(transform.position, transform.position + new Vector3(rayLenght, 0), Color.blue);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, rayLenght, collisionMasks);

            if (hit)
            {
                CollisionDetection(hit.transform.gameObject);
            }
        }

        Vector3 rotation(BulletDirection dir)
        {
            Vector3 rotationAngle = Vector3.zero;
            if (dir == BulletDirection.Up)
                rotationAngle.z = 90;
            else if (dir == BulletDirection.Left)
                rotationAngle.z = 180;
            else if (dir == BulletDirection.Down)
                rotationAngle.z = 270;
            return rotationAngle;
        }

        public enum BulletDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        public abstract void CollisionDetection(GameObject hit);
    }
}