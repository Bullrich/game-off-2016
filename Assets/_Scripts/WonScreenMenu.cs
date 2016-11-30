using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// By @JavierBullrich

public class WonScreenMenu : MonoBehaviour {
    public Text mainMenuText, exitText;

    private void Start()
    {
        ChangeTextColor(mainMenuText);
    }

    void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0.5f)
            ChangeTextColor(mainMenuText);
        else if (Input.GetAxisRaw("Vertical") < -0.5f)
            ChangeTextColor(exitText);

        if (Input.GetButtonDown("Jump"))
            Play();
        else if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void ChangeTextColor(Text assigned)
    {
        mainMenuText.color = Color.white;
        exitText.color = Color.white;
        assigned.color = Color.yellow;
    }

    void Play()
    {
        if (mainMenuText.color == Color.yellow)
            SceneManager.LoadScene("MainMenu");
        else if (exitText.color == Color.yellow)
            Application.Quit();
    }

}
