using UnityEngine;
using System.Collections;
using Glitch.Player;
using System;
// By @JavierBullrich

namespace Glitch.Enemy
{
    public class AjaxJammer : Enemy, IAjax
    {
        public float fallSpeed = 3;
        bool canFall, canLift;
        public bool active;

        public override void Destroy()
        {
            Glitch.Manager.GameManagerBase.instance.PlaySfx(Glitch.Manager.SoundManager.Sfx.explosion);
            gameObject.SetActive(false);
        }

        public override void Start()
        {
            base.Start();

        }

        public bool Attacking()
        {
            return canFall;
        }

        public Enemy returnScript()
        {
            return this;
        }

        public override void InteractWith()
        {
            //throw new NotImplementedException();
        }

        public override void ReceiveDamage(int damage)
        {
            /*TakeHit(damage);
            damagedAnim = 1;*/
        }

        private void Update()
        {
            if (active)
            {
                DamageAnim();
                UpdateRaycastOrigins();
                if (!canLift)
                    CheckFloor();
                if (canFall)
                {
                    Move(Vector2.down, fallSpeed);
                    canLift = false;
                }
                else if (startPos.y - _tran.position.y > 0.1 && canLift)
                {
                    Move(Vector2.up, fallSpeed / 2);
                }
                else
                    canLift = false;
            }
        }

        public void StartLifting()
        {
            canLift = true;
        }

        void Move(Vector2 direction, float speed)
        {
            _tran.Translate(direction * speed * Glitch.Manager.GameManagerBase.DeltaTime);
        }

        void CheckFloor()
        {
            for (int i = 0; i < verticalRayCount; i++)
            {
                Vector2 rayOrigin = raycastOrigins.bottomLeft;
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
                print(verticalRayCount);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 9, collisionMask);
                if (hit)
                {
                    Debug.DrawLine(rayOrigin, hit.point, Color.blue);
                    if (hit.transform.gameObject.layer == 9)
                        canFall = true;
                    else if (hit.distance == 0)
                    {
                        canFall = false;
                        Glitch.Manager.GameManagerBase.instance.PlaySfx(Glitch.Manager.SoundManager.Sfx.explosion);
                    }
                }
            }
        }

        public override void TriggerEntered(GlitcherBehaviorBase glitcher)
        {
            if (active)
                glitcher.ReceiveDamage(damagePerHit);
        }

        public override void TriggerExit(GlitcherBehaviorBase glitcher)
        {
            //throw new NotImplementedException();
        }

        public void Activate(bool status)
        {
            active = status;
        }
    }
}