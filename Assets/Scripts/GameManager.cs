using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int currentLvl;
    public Controller player;

    public Text record;

    public void Awake()
    {
        instance = this;
        currentLvl = PlayerPrefs.GetInt("Lvl");

        if(SceneManager.GetActiveScene().buildIndex != currentLvl)
        {
            SceneManager.LoadScene(currentLvl);
        }

        if(PlayerPrefs.HasKey("Lvl" + currentLvl))
        record.text = "Record: " + PlayerPrefs.GetFloat("Lvl" + currentLvl) + "s";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void WinLevelLoadNext()
    {
        if (PlayerPrefs.HasKey("Lvl" + currentLvl))
        {
            if(PlayerPrefs.GetFloat("Lvl" + currentLvl) > PlayerPrefs.GetFloat("Timer"))
                PlayerPrefs.SetFloat("Lvl" + currentLvl, PlayerPrefs.GetFloat("Timer"));
        } else
        {
            PlayerPrefs.SetFloat("Lvl" + currentLvl, PlayerPrefs.GetFloat("Timer"));
        }

        if (currentLvl == 0)
        {
            PlayerPrefs.SetInt("Lvl", 1);
            SceneManager.LoadScene(1);
        }
        else
        {
            PlayerPrefs.SetInt("Lvl", 0);
            SceneManager.LoadScene(0);
        }
    }
}
