using UnityEngine;
using System.Collections;
// By @JavierBullrich

public class GlitcherAnimations {
    public Animator anim;

    public GlitcherAnimations(Animator animator)
    {
        anim = animator;
    }

    public void Play(string animationName)
    {
        anim.Play(animationName);
    }
	
}
