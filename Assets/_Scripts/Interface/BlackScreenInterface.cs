using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Glitch.DTO;
// By @JavierBullrich
namespace Glitch.UI
{
    public class BlackScreenInterface : MonoBehaviour
    {
        Image blackScreen;
        GameInterface parentInterface;

        private void Start()
        {
            blackScreen = GetComponent<Image>();
            StartCoroutine(StartWithBlackScreen());
        }

        public void SetUp(ManagerDTO dto)
        {
            parentInterface = (GameInterface)dto.script;
        }
        public void KeepScreenChange()
        {
            StartCoroutine(KeepScreenBlack());
        }

        public void ShowBlackScreen()
        {
            StartCoroutine(FadeTheScreen());
        }
        #region Screen Fading
        IEnumerator FadeTheScreen()
        {
            parentInterface.canContinue = false;
            parentInterface.canSwith = false;
            Color transparent = blackScreen.color;

            float fadeTime = 0;
            float fadeSpeed = 2;
            while (blackScreen.color.a < 1)
            {
                yield return new WaitForEndOfFrame();
                fadeTime += Glitch.Manager.GameManagerBase.DeltaTime * fadeSpeed;
                blackScreen.color = Color.Lerp(transparent, Color.black, fadeTime);
            }
            parentInterface.canSwith = true;

            while (blackScreen.color.a > 0)
            {
                yield return new WaitForEndOfFrame();
                parentInterface.canSwith = false;
                fadeTime -= Glitch.Manager.GameManagerBase.DeltaTime * fadeSpeed;
                blackScreen.color = Color.Lerp(transparent, Color.black, fadeTime);
            }
            parentInterface.canContinue = true;
            yield return new WaitForEndOfFrame();
        }

        IEnumerator KeepScreenBlack()
        {
            yield return new WaitForEndOfFrame();
            Color transparent = blackScreen.color;
            //blackScreen.color = Color.black;
            parentInterface.canContinue = false;
            parentInterface.canSwith = false;

            float fadeTime = 0;
            float fadeSpeed = 1f;
            while (blackScreen.color.a < 1)
            {
                yield return new WaitForEndOfFrame();
                parentInterface.canSwith = false;
                fadeTime += Glitch.Manager.GameManagerBase.DeltaTime * fadeSpeed;
                blackScreen.color = Color.Lerp(transparent, Color.black, fadeTime);
            }
            yield return new WaitForSeconds(3f);
            parentInterface.canSwith = true;
            parentInterface.canContinue = true;
            yield return new WaitForEndOfFrame();
        }

        IEnumerator StartWithBlackScreen()
        {
            yield return new WaitForEndOfFrame();
            Color transparent = blackScreen.color;
            blackScreen.color = Color.black;
            parentInterface.canContinue = false;
            parentInterface.canSwith = false;

            float fadeTime = 1;
            float fadeSpeed = 1f;
            while (blackScreen.color.a > 0)
            {
                yield return new WaitForEndOfFrame();
                parentInterface.canSwith = false;
                fadeTime -= Glitch.Manager.GameManagerBase.DeltaTime * fadeSpeed;
                blackScreen.color = Color.Lerp(transparent, Color.black, fadeTime);
            }
            parentInterface.canContinue = true;
            yield return new WaitForEndOfFrame();
        }
        #endregion
    }
}