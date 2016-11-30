using UnityEngine;
using System.Collections;
// By @JavierBullrich
namespace Glitch.Manager
{
    public class SoundManager : MonoBehaviour
    {
        [Header("Sound effects")]
        public AudioClip heroDeath;
        public AudioClip[] explosion, hit, jump, pickup, shoot;

        public enum Sfx
        {
            heroDeath,
            explosion,
            hit,
            jump,
            pickup,
            shoot
        }

        public AudioClip getSfx(Sfx option)
        {
            AudioClip audioSFX;
            switch (option)
            {
                case Sfx.heroDeath:
                    audioSFX = heroDeath;
                    break;
                case Sfx.explosion:
                    audioSFX = explosion[Random.Range(0, explosion.Length)];
                    break;
                case Sfx.hit:
                    audioSFX = hit[Random.Range(0, hit.Length)];
                    break;
                case Sfx.jump:
                    audioSFX = jump[Random.Range(0, jump.Length)];
                    break;
                case Sfx.pickup:
                    audioSFX = pickup[Random.Range(0, pickup.Length)];
                    break;
                case Sfx.shoot:
                    audioSFX = shoot[Random.Range(0, shoot.Length)];
                    break;
                default:
                    audioSFX = null;
                    break;
            }
            return audioSFX;
        }

    }
}