using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Glitch.Manager;
// By @JavierBullrich

public class LostScreenMenu : MonoBehaviour {
    public Text restart, mainMenu;

    private void Start()
    {
        ChangeTextColor(restart);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0.5f)
            ChangeTextColor(restart);
        else if (Input.GetAxisRaw("Vertical") < -0.5f)
            ChangeTextColor(mainMenu);

        if (Input.GetButtonDown("Jump"))
            Play();
        else if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void ChangeTextColor(Text assigned)
    {
        restart.color = Color.white;
        mainMenu.color = Color.white;
        assigned.color = Color.yellow;
    }

    void Play()
    {
        if (mainMenu.color == Color.yellow)
            SceneManager.LoadScene("MainMenu");
        else if (restart.color == Color.yellow)
            GameManagerBase.instance.gameObject.GetComponent<GlitchManager>().RestartGame();
        gameObject.SetActive(false);
    }

}
