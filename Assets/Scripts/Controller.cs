using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 1;

    private Vector3 startPos;
    private Vector3 deltaPos;

    private Rigidbody rb;

    [SerializeField] private float slowMotion = 2f;
    [SerializeField] private Animator animator;
    [SerializeField] private Text text; 

    [Header("Edge Z")]
    [SerializeField] private float minCameraDis = -4f;

    [HideInInspector]
    public bool gameOver = false;
    private bool gameStart;

    private bool onMeta;

    [SerializeField] private Text timeText;
    private float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 0.0001f;
    }

    void Update()
    {
        if (!onMeta && !gameOver)
        {
            timer += Time.deltaTime;
            timeText.text = " " + Mathf.Round(timer * 10) / 10;
        }
        if (!gameOver)
            Move();
        else
            GameOver();

        if (onMeta)
        {
            GoToNextLevel();
        }
    }

    private void GoToNextLevel()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Panel") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameManager.instance.WinLevelLoadNext();
            }
        }
    }

    private void LateUpdate()
    {
        //Edges
        UpdateMove();
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!gameStart)
                StartGame();

            startPos = Input.mousePosition;
            rb.velocity = Vector3.zero;
        }

        if (Input.GetMouseButton(0))
        {
            deltaPos = Input.mousePosition - startPos;
            startPos = Input.mousePosition;

            rb.AddForce(new Vector3(deltaPos.x, 0, deltaPos.y) * speed);
        }

        if (Input.GetMouseButtonUp(0))
        {
            startPos = Vector3.zero;
        }
    }

    private void UpdateMove()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -9.5f, 9.5f);
        pos.z = Mathf.Clamp(pos.z, Camera.main.transform.position.z + minCameraDis, 140f);

        transform.position = pos;
    }
    private void GameOver()
    {
        if (gameOver && animator.GetCurrentAnimatorStateInfo(0).IsName("Panel") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Restart();
            }
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void StartGame()
    {
        Time.timeScale = 1f;
        gameStart = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Meta" && !gameOver)
        {
            PlayerPrefs.DeleteKey("Record");
            PlayerPrefs.SetFloat("Timer", Mathf.Round(timer * 10) / 10);

            if (PlayerPrefs.HasKey("Lvl" + GameManager.currentLvl))
            {
                if (PlayerPrefs.GetFloat("Timer") < PlayerPrefs.GetFloat("Lvl" + GameManager.currentLvl))
                {
                    timeText.text = "NEW RECORD: " + PlayerPrefs.GetFloat("Timer");
                }
            }

            SlowMotion();
            animator.SetBool("Panel", true);
            text.text = "Congratulation TAP to go next Level";
            onMeta = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            if (collision.gameObject.GetComponent<Renderer>().material.name != this.GetComponent<Renderer>().material.name)
            {
                SlowMotion();
                animator.SetBool("Panel", true);
                text.text = "TAP TO RESET";
                gameOver = true;
            }
        }
        if (collision.gameObject.tag == "Meta" && !gameOver)
        {
            PlayerPrefs.DeleteKey("Record");
            PlayerPrefs.SetFloat("Timer", Mathf.Round(timer * 10) / 10);

            if (PlayerPrefs.HasKey("Lvl" + GameManager.currentLvl))
            {
                if (PlayerPrefs.GetFloat("Timer") < PlayerPrefs.GetFloat("Lvl" + GameManager.currentLvl))
                {
                    timeText.text = "NEW RECORD: " + Mathf.Round(timer * 10) / 10;
                }
            }

            SlowMotion();
            animator.SetBool("Panel", true);
            text.text = "Congratulation TAP to go next Level";
            onMeta = true;
        }
    }

    private void SlowMotion()
    {
        Time.timeScale = 0.5f;
        speed = 0.2f;
    }
}
