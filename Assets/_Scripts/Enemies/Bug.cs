using UnityEngine;
using System.Collections;
using Glitch.Enemy;
using Glitch.Player;
// By @JavierBullrich

public class Bug : Enemy
{
    public Sprite goingUp, goingDown;

    [Header("Character Controller 2D")]
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    public float moveSpeed = 6;

    public Vector2 movement;

    CharacterController2D char2D;

    public override void Start()
    {
        base.Start();
        moveSpeed = Random.Range(3, 9);
        spr = GetComponent<SpriteRenderer>();
        char2D = new CharacterController2D(this, transform, maxJumpHeight, minJumpHeight, timeToJumpApex, moveSpeed);
    }

    public override void Destroy()
    {
        Glitch.Manager.GameManagerBase.instance.PlaySfx(Glitch.Manager.SoundManager.Sfx.explosion);
        gameObject.SetActive(false);
    }

    public override void InteractWith()
    {
        //throw new NotImplementedException();
    }

    int direction = 1;
    private void Update()
    {
        DamageAnim();

        BugBehavior();

        char2D.CalculateVelocity(ref movement, direction);
        char2D.Move(movement * Glitch.Manager.GameManagerBase.DeltaTime);
    }

    private void BugBehavior()
    {
        if (movement.y > 0)
            spr.sprite = goingUp;
        else if (movement.y < 0)
            spr.sprite = goingDown;

        if (char2D.collisions.below)
        {
            movement.y = Random.Range(20, 30);
            PlaySfx(Glitch.Manager.SoundManager.Sfx.jump);
        }
        else if (char2D.collisions.left)
            direction = 1;
        else if (char2D.collisions.right)
            direction = -1;
        else if (char2D.collisions.above)
            movement.y = 0;
    }

    public override void ReceiveDamage(int damage)
    {
        TakeHit(damage);
        damagedAnim = 1;
    }

    public override void TriggerEntered(GlitcherBehaviorBase glitcher)
    {
        //throw new NotImplementedException();
        glitcher.ReceiveDamage(damagePerHit);
        if (!glitcher._tran.gameObject.activeInHierarchy)
            transform.parent.gameObject.SetActive(false);
    }

    public override void TriggerExit(GlitcherBehaviorBase glitcher)
    {
        //throw new NotImplementedException();
    }
}
