using UnityEngine;
using System.Collections;
// By @JavierBullrich
namespace Glitch.Interactable
{
    [RequireComponent(typeof(BoxCollider2D))]
     abstract class InteractableObject : MonoBehaviour
    {
        public abstract void InteractWith();

        public abstract void TriggerEntered();
        public abstract void TriggerExit();
    }
}