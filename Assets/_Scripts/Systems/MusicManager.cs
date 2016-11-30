using UnityEngine;
using System.Collections;
// By @JavierBullrich
namespace Glitch.Manager
{
    public class MusicManager : MonoBehaviour
    {
        public AudioClip[] music;
        public AudioSource musicSource;
        int musicClips;

        private void Start()
        {
            musicSource = GetComponent<AudioSource>();
            PlayMusic();
        }

        void PlayMusic()
        {
            musicSource.clip = music[musicClips];
            musicClips++;
            if (musicClips > music.Length - 1)
                musicClips = 0;
            musicSource.Play();
            float musicLength = musicSource.clip.length;
            StartCoroutine(PlayNextSong(musicLength));
        }

        IEnumerator PlayNextSong(float length)
        {
            yield return new WaitForSeconds(length);
            PlayMusic();
        }

    }
}