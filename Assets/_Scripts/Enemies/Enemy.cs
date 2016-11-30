using UnityEngine;
using System.Collections;
using Glitch.Interactable;
using Glitch.Player;
using System;
using Glitch.Manager;
// By @JavierBullrich
namespace Glitch.Enemy
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(AudioSource))]
    public abstract class Enemy : RaycastController, IDamagable, IInteractable
    {
        [Range(1, 30)]
        [SerializeField]
        private int TotalLifePoints = 3;
        [HideInInspector]
        public int lifePoints = 1;
        Transform target;
        [Range(1, 5)]
        public int damagePerHit;
        protected SpriteRenderer spr;
        protected Transform _tran;
        [HideInInspector]
        public float damagedAnim;

        protected Vector3 startPos;
        private AudioSource audioSource;
        SoundManager sManager;


        public override void Start()
        {
            base.Start();
            spr = GetComponent<SpriteRenderer>();
            _tran = transform;
            startPos = _tran.position;
            audioSource = GetComponent<AudioSource>();
            sManager = (SoundManager)GameManagerBase.instance.getSFX().script;
            lifePoints = TotalLifePoints;
        }

        public void ResetEnemy()
        {
            lifePoints = TotalLifePoints;
            _tran.position = startPos;
            gameObject.SetActive(true);
        }

        protected void TakeHit(int damage)
        {
            lifePoints -= damage;
            PlaySfx(SoundManager.Sfx.hit);
            if (lifePoints <= 0)
                Destroy();
        }

        protected void PlaySfx(SoundManager.Sfx sfx)
        {
            AudioClip clip = sManager.getSfx(sfx);
            audioSource.clip = clip;
            audioSource.Play();
        }

        protected void DamageAnim()
        {
            spr.color = Color.Lerp(Color.white, Color.red, damagedAnim);
            if (damagedAnim > 0)
                damagedAnim -= Glitch.Manager.GameManagerBase.DeltaTime * 2;
        }

        //public abstract void HandleDamage();
        public abstract void ReceiveDamage(int damage);
        public abstract void Destroy();

        public abstract void InteractWith();

        public abstract void TriggerEntered(GlitcherBehaviorBase glitcher);

        public abstract void TriggerExit(GlitcherBehaviorBase glitcher);

        public GameObject ReturnGameobject()
        {
            return gameObject;
        }
    }
}