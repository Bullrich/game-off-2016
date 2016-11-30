using UnityEngine;
using System.Collections;
// By @JavierBullrich

    /// <summary>Interface managing all objects that can receive damage</summary>
public interface IDamagable {
    void ReceiveDamage(int damage);
    void Destroy();
}
