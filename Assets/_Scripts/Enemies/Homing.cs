using UnityEngine;
using Glitch.Manager;
using Glitch.Player;
// By @JavierBullrich
namespace Glitch.Enemy
{
    public class Homing : Enemy
    {
        GlitcherController player;
        public float speed = 3;
        ParticleSystem ps;
        [Range(3, 15)]
        public float distanceToFollow;

        public override void Start()
        {
            base.Start();
            player = (GlitcherController)GameManagerBase.instance.returnPlayer().script;
            ps = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            if(Vector2.Distance(transform.position, player.transform.position) < distanceToFollow)
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), player.transform.position, speed * GameManagerBase.DeltaTime);
        }

        public override void ReceiveDamage(int damage)
        {
            TakeHit(damage);
        }

        public override void Destroy()
        {
            ps.Clear();
            gameObject.SetActive(false);
        }

        public override void InteractWith()
        {
            //throw new NotImplementedException();
        }

        public override void TriggerEntered(GlitcherBehaviorBase glitcher)
        {
            glitcher.ReceiveDamage(damagePerHit);
        }

        public override void TriggerExit(GlitcherBehaviorBase glitcher)
        {
            //throw new NotImplementedException();
        }
    }
}