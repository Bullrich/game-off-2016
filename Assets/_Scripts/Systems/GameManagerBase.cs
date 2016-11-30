using UnityEngine;
using System.Collections;
using Glitch.DTO;
// By @JavierBullrich
namespace Glitch.Manager
{
    public abstract class GameManagerBase : MonoBehaviour
    {
        public static GameManagerBase instance;
        SoundManager sfxManager;
        PoolManager poolSystem;
        public delegate void UpdateLifepoints(int lifePoints, int totalLifePoints);
        UpdateLifepoints updateLife;
        Glitch.GameCamera.GlitchCameraFollow gameCamera;
        /// <summary>Use this instead of Time.DeltaTime</summary>
        public static float DeltaTime;
        private bool gamePaused;
        public AudioSource audioS;

        public virtual void Awake()
        {
            sfxManager = GetComponent<SoundManager>();
            Cursor.visible = false;
        }

        public ManagerDTO getSFX()
        {
            ManagerDTO dto = new ManagerDTO();
            dto.script = sfxManager;
            return dto;
        }
        public void PlaySfx(SoundManager.Sfx sfx)
        {
            AudioClip clip = sfxManager.getSfx(sfx);
            audioS.clip = clip;
            audioS.Play();
        }

        protected void InitManager()
        {
            poolSystem = GetComponent<PoolManager>();
            gameCamera = Camera.main.GetComponent<Glitch.GameCamera.GlitchCameraFollow>();
        }

        public ManagerDTO GetPoolObject(string objectName)
        {
            ManagerDTO dto = new ManagerDTO();
            dto.gObject = poolSystem.GetPooledObject(objectName);
            return dto;
        }

        public void ChangeCameraFocus(Transform target = null)
        {
            gameCamera.ChangeTarget(target);
        }

        public virtual void Update()
        {
            if (!gamePaused)
                DeltaTime = Time.deltaTime;
        }

        public abstract void ExecuteStory(StoryDTO storyDto);
        public abstract bool canExecuteStory(StoryDTO storyDto);

        public void PauseGame(bool pause)
        {
            gamePaused = pause;
        }

        public void SuscribeUI(UpdateLifepoints function)
        {
            updateLife = function;
        }
        public void UpdateLifeUI(int lifePoints, int totalLifePoints)
        {
            updateLife.Invoke(lifePoints, totalLifePoints);
        }

        public abstract ManagerDTO returnPlayer();
    }
}