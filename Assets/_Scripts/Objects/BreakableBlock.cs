using UnityEngine;
using System.Collections;
using System;
// By @JavierBullrich
[RequireComponent(typeof(BoxCollider2D))]
public class BreakableBlock : MonoBehaviour, IDamagable
{
    Animator anim;
    [Range(1, 15)]
    public int lifepoints;
    public bool indestructable;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Destroy()
    {
        Glitch.Manager.GameManagerBase.instance.PlaySfx(Glitch.Manager.SoundManager.Sfx.explosion);
        Destroy(gameObject);
    }

    public void ReceiveDamage(int damage)
    {
        if (!indestructable)
        {
            anim.Play("Block.Hurt", -1, 0f);
            lifepoints -= damage;
            if (lifepoints <= 0)
                Destroy();
        }
    }
}
