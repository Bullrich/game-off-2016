using UnityEngine;
using System.Collections;
using Glitch.DTO;
using System;
using Glitch.Interactable;
using Glitch.Manager;
// By @JavierBullrich
namespace Glitch.Player {
    public abstract class GlitcherBehaviorBase : IDamagable
    {
        public float maxJumpHeight = 4;
        public float minJumpHeight = 1;
        public float timeToJumpApex = .4f;
        float accelerationTimeAirborne = .2f;
        float accelerationTimeGrounded = .1f;
        public float moveSpeed = 6;

        [Header("Wall Sliding")]
        [Tooltip("Defines if the player can slide through a wall")]
        public bool canWallSlide;
        [Tooltip("Defines if the player can Jump from a wall")]
        public bool canWallJump;

        public Vector2 wallJumpClimb;
        public Vector2 wallJumpOff;
        public Vector2 wallLeap;

        public float wallSlideSpeedMax = 3;
        public float wallStickTime = .25f;
        float timeToWallUnstick;

        float gravity;
        float maxJumpVelocity;
        float minJumpVelocity;
        Vector3 velocity;
        float velocityXSmothing;

        CustomGlitcherController2D controller;
        protected GlitcherAnimations glAnim;
        public Transform _tran;
        protected AudioSource audioSource;
        SoundManager sManager;

        protected Vector2 directionalInput;
        protected bool wallSliding;
        int wallDirX;
        public int lifePoints = 10, maxLifePoints = 10;
        public IInteractable switchObject;

        public abstract void Interact(Glitch.Interactable.IInteractable interactable);

        protected void SetValues(GlitcherSettingDTO dto)
        {
            
            maxJumpHeight = dto.maxJumpHeight;
            minJumpHeight = dto.minJumpHeight;
            timeToJumpApex = dto.timeToJumpApex;
            moveSpeed = dto.moveSpeed;
            canWallSlide = dto.canWallSlide;
            canWallJump = dto.canWallJump;
            wallJumpClimb = dto.wallJumpClimb;
            wallJumpOff = dto.wallJumpOff;
            wallLeap = dto.wallLeap;

            wallSlideSpeedMax = dto.wallSlideSpeedMax;
            wallStickTime = dto.wallStickTime;

            controller = dto.controller;
            controller.AssignGlitchScript(this);
            _tran = dto.tran;
            glAnim = new GlitcherAnimations(dto.animator);
            audioSource = dto.audio;
            sManager = (SoundManager)GameManagerBase.instance.getSFX().script;
            setGravity();
        }

        protected void PlaySfx(SoundManager.Sfx sfx)
        {
            AudioClip clip = sManager.getSfx(sfx);
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void Restart(int savedLifePoints)
        {
            lifePoints = savedLifePoints;
            Glitch.Manager.GameManagerBase.instance.UpdateLifeUI(lifePoints, maxLifePoints);
        }

        public abstract void Init(GlitcherSettingDTO glitcherDTO);

        public void SetDirectionalInput(Vector2 input)
        {
            directionalInput = input;
        }

        public enum KeyInput
        {
            Space,
            Fire,
            FireUp,
            Action,
            Down
        }

        public abstract void ActionManager(KeyInput inp);

        float damageCooldown = 0;
        public virtual void Update()
        {
            CalculateVelocity();
            if (canWallSlide)
                HandleWallSliding();
            if (damageCooldown > 0)
                damageCooldown -= Glitch.Manager.GameManagerBase.DeltaTime;
            controller.Move(velocity * Glitch.Manager.GameManagerBase.DeltaTime, directionalInput);
            if (Mathf.Abs(directionalInput.x) > 0)
                _tran.localScale = new Vector2(Mathf.Abs(_tran.localScale.x) * Mathf.Sign(directionalInput.x), _tran.localScale.y);
            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
                if (Mathf.Abs(directionalInput.x) > 0)
                    glAnim.Play("Glitcher.Walk");
                else
                    glAnim.Play("Glitcher.Idle");
            }
            else if (!wallSliding)
                glAnim.Play("Glitcher.Jumping");
        }

        void CalculateVelocity()
        {
            float targetVelocityX = directionalInput.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y += gravity * Glitch.Manager.GameManagerBase.DeltaTime;
        }

        void HandleWallSliding()
        {
            wallDirX = (controller.collisions.left) ? -1 : 1;
            wallSliding = false;
            if ((controller.collisions.left || controller.collisions.right) && velocity.y < 0)
            {
                wallSliding = true;

                if (velocity.y < wallSlideSpeedMax)
                    velocity.y = -wallSlideSpeedMax;

                if (timeToWallUnstick > 0)
                {
                    velocityXSmothing = 0;
                    velocity.x = 0;

                    if (directionalInput.x != wallDirX && directionalInput.x != 0)
                        timeToWallUnstick -= Glitch.Manager.GameManagerBase.DeltaTime;
                    else
                        timeToWallUnstick = wallStickTime;
                    if (!controller.collisions.below)
                        glAnim.Play("Glitcher.WallSliding");
                }
                else
                    timeToWallUnstick = wallStickTime;
            }
        }

        public void OnJumpInputUp()
        {
            if (velocity.y > minJumpVelocity)
                velocity.y = minJumpVelocity;
        }

        public void OnJumpInputDown()
        {
            if (wallSliding && canWallJump)
            {
                if (wallDirX == directionalInput.x)
                {
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                }
                else if (directionalInput.x == 0)
                {
                    velocity.x = -wallDirX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                }
                else
                {
                    velocity.x = -wallDirX * wallLeap.x;
                    velocity.y = wallLeap.y;
                }
                //PlaySfx(SoundManager.Sfx.jump);
            }
            if (controller.collisions.below)
            {
                velocity.y = maxJumpVelocity;
                //PlaySfx(SoundManager.Sfx.jump);
            }
        }

        public void setGravity()
        {
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
            Debug.Log("gravity: " + gravity + " jump velocity " + maxJumpVelocity);
        }

        public void ReceiveDamage(int damage)
        {
            if (damageCooldown <= 0)
            {
                velocity.x = -Mathf.Sign(_tran.localScale.x) * 18;
                velocity.y = 17;
                lifePoints -= damage;
                Glitch.Manager.GameManagerBase.instance.UpdateLifeUI(lifePoints, maxLifePoints);
                if (lifePoints <= 0)
                    Destroy();
                PlaySfx(SoundManager.Sfx.hit);
                damageCooldown = 2;
            }
        }

        public void Destroy()
        {
            GameManagerBase.instance.gameObject.GetComponent<GlitchManager>().Died();
            _tran.gameObject.SetActive(false);
        }
    }
}