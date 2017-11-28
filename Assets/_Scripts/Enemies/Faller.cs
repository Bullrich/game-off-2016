using UnityEngine;
using System.Collections;
using Glitch.Enemy;
using Glitch.Player;
// By @JavierBullrich

public class Faller : Enemy
{
    public float fallSpeed = 3;
    bool canFall, canLift;
    [Header("Sprites for animation")]
    public Sprite regularSprite;
    public Sprite fallingSprite,
        hittedSprite, hittedSprite2;

    public override void Destroy()
    {
        Glitch.Manager.GameManagerBase.instance.PlaySfx(Glitch.Manager.SoundManager.Sfx.explosion);
        gameObject.SetActive(false);
    }

    public override void InteractWith()
    {
        //throw new NotImplementedException();
    }

    public override void ReceiveDamage(int damage)
    {
        TakeHit(damage);
        damagedAnim = 1;
    }

    private void Update()
    {
        DamageAnim();
        UpdateRaycastOrigins();
        if(!canLift)
            CheckFloor();
        if (canFall)
        {
            Move(Vector2.down, fallSpeed);
            spr.sprite = fallingSprite;
            canLift = false;
        }
        else if (startPos.y - transform.localPosition.y > 0.1 && canLift)
        {
            Move(Vector2.up, fallSpeed / 2);
            spr.sprite = regularSprite;
        }
        else
            canLift = false;
    }

    public void StartLifting()
    {
        canLift = true;
    }

    void Move(Vector2 direction, float speed)
    {
        transform.Translate(direction * speed * Glitch.Manager.GameManagerBase.DeltaTime);
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
                    StartCoroutine(StartAnimation());
                    Glitch.Manager.GameManagerBase.instance.PlaySfx(Glitch.Manager.SoundManager.Sfx.explosion);
                }
            }
        }
    }

    IEnumerator StartAnimation()
    {
        spr.sprite = hittedSprite;
        yield return new WaitForSeconds(0.1f);
        spr.sprite = hittedSprite2;
        yield return new WaitForSeconds(0.2f);
        StartLifting();
    }

    public override void TriggerEntered(GlitcherBehaviorBase glitcher)
    {
        glitcher.ReceiveDamage(damagePerHit);
        if (!glitcher._tran.gameObject.activeInHierarchy)
            transform.parent.gameObject.SetActive(false);
    }

    public override void TriggerExit(GlitcherBehaviorBase glitcher)
    {
        //throw new NotImplementedException();
    }
}
