using UnityEngine;
using System.Collections;
using Glitch.Enemy;
// By @JavierBullrich

public interface IAjax {
    void Activate(bool status);
    Enemy returnScript();
    bool Attacking();
}
