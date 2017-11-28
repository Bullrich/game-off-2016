using UnityEngine;
using Glitch.Enemy;
using Glitch.Player;
// By @JavierBullrich

public class Slime : Enemy
{
    [Range(0.3f, 4)]
    public float speed;
    int dir = 1;

    public override void Destroy()
    {
        Glitch.Manager.GameManagerBase.instance.PlaySfx(Glitch.Manager.SoundManager.Sfx.explosion);
        gameObject.SetActive(false);
    }

    public override void ReceiveDamage(int damage)
    {
        TakeHit(damage);
        damagedAnim = 1;
    }

    
    private void Update()
    {
        transform.Translate((Vector2.right * dir) * Glitch.Manager.GameManagerBase.DeltaTime * speed);
        spr.flipX = (dir == 1);
        HorizontalCollisions();
        UpdateRaycastOrigins();
        DamageAnim();
    }

    void HorizontalCollisions()
    {
        float directionX = dir;

        Debug.DrawRay(transform.position, Vector2.right * directionX, Color.red);
        float spriteHeight = spr.sprite.rect.height / spr.sprite.pixelsPerUnit;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.5f), Vector2.right * directionX, 1f, collisionMask);
        if (hit)
        {
            dir *= -1;
        }

        VerticalFall();
    }
    void VerticalFall()
    {
        float spriteWidth = spr.sprite.textureRect.width / spr.sprite.pixelsPerUnit;
        Vector2 raycastStartPoint = (dir == 1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
        RaycastHit2D hit = Physics2D.Raycast(raycastStartPoint, Vector2.down, .1f, collisionMask);
        Debug.DrawRay(raycastStartPoint, Vector2.down, Color.red);

        if (hit)
            return;
        else
        {
            dir *= -1;
        }
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
