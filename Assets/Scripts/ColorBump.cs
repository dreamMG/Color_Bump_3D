using UnityEngine;

public class ColorBump : MonoBehaviour
{
    public Controller player;
    private Renderer r;

    private void Start()
    {
        r = player.GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !player.gameOver)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.gameObject.tag == "ColorSwap")
                    swapColor(hit.collider.gameObject);
            }
        }
    }

    void swapColor(GameObject edge)
    {
        r.sharedMaterial = edge.GetComponent<Renderer>().sharedMaterial;
    }
}
