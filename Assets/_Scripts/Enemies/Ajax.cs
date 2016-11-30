using UnityEngine;
using System.Collections;
using Glitch.Player;
using System;
// By @JavierBullrich
namespace Glitch.Enemy
{
    public class Ajax : Enemy
    {
        private bool activate;
        IAjax xutter, jammer;
        public Enemy enemyXutter, enemyJammer;
        Transform player;
        [Range(1, 5)]
        public float jammerSpeed = 2;
        private float jammerStartPos;
        private GlitcherBehaviorBase glitch;
        Animator anim;
        Glitch.Manager.FormRoom room;

        public override void Destroy()
        {
            xutter.Activate(false);
            jammer.Activate(false);
            activate = false;
            StartCoroutine(BossDestroyed());
        }

        IEnumerator BossDestroyed()
        {
            PlaySfx(Manager.SoundManager.Sfx.explosion);
            yield return new WaitForSeconds(1.1f);
            transform.localScale /= 2;
            PlaySfx(Manager.SoundManager.Sfx.explosion);
            yield return new WaitForSeconds(1.3f);
            transform.localScale /= 2;
            PlaySfx(Manager.SoundManager.Sfx.explosion);
            yield return new WaitForSeconds(1.9f);
            room.Fight(false);
            Destroy(gameObject);
        }

        public override void Start()
        {
            base.Start();
            xutter = enemyXutter.transform.GetComponent<IAjax>();
            jammer = enemyJammer.transform.GetComponent<IAjax>();
            jammerStartPos = enemyJammer.gameObject.transform.position.y;
            anim = GetComponent<Animator>();
            room = transform.parent.GetComponent<Glitch.Manager.FormRoom>();
        }

        public override void InteractWith()
        {
            //throw new NotImplementedException();
        }

        public override void ReceiveDamage(int damage)
        {
            print(lifePoints + " life!");
            if (activate)
            {
                TakeHit(damage);
                damagedAnim = 1;

            }
        }

        private void Update()
        {
            if (activate)
            {
                DamageAnim();
                float step = jammerSpeed * Glitch.Manager.GameManagerBase.DeltaTime;
                if (!jammer.Attacking())
                    enemyJammer.gameObject.transform.position =
                        Vector2.MoveTowards(enemyJammer.gameObject.transform.position, new Vector2(player.position.x, jammerStartPos), step);
                activate = player.gameObject.activeInHierarchy;
                if (!activate)
                {
                    xutter.Activate(false);
                    jammer.Activate(false);
                    anim.Play("AjaxAsleep");
                    //room.Fight(false);
                }
            }
        }

        public override void TriggerEntered(GlitcherBehaviorBase glitcher)
        {
            if (!activate)
                Activate();
            player = glitcher._tran;
            glitch = glitcher;
        }

        private void Activate()
        {
            print("ACTIVATED!");
            xutter.Activate(true);
            jammer.Activate(true);
            activate = true;
            anim.Play("AjaxAwake");
            room.Fight(true);
        }

        public override void TriggerExit(GlitcherBehaviorBase glitcher)
        {

        }
    }
}