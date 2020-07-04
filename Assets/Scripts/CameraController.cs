using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speedGame = 7;

    public Controller player;
    void Update()
    {
        if (!player.gameOver && transform.position.z < 135f) 
        {
            transform.position += new Vector3(0, 0, speedGame * Time.deltaTime);
            player.transform.position += new Vector3(0, 0, speedGame * Time.deltaTime);
        }
    }
}
