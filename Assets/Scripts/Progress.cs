using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    public Controller player;

    public Slider slider;

    public GameObject record;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Record"))
        {
            Instantiate(record, new Vector3(0, 0.525f, PlayerPrefs.GetFloat("Record")), Quaternion.Euler(90, 0, 0));
        }

        slider.maxValue = 140;
        player = FindObjectOfType<Controller>();
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (!player.gameOver && slider.value < player.transform.position.z)
            slider.value = player.transform.position.z;

        if (player.gameOver)
        {
            float rec = PlayerPrefs.GetFloat("Record");

            if (rec < slider.value)
            {
                PlayerPrefs.SetFloat("Record", slider.value);
            }
        }

    }
}
