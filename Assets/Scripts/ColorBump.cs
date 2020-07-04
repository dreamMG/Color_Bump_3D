using UnityEngine;

public class ColorBump : MonoBehaviour
{
    public Controller player;

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
        player.GetComponent<Renderer>().sharedMaterial = edge.GetComponent<Renderer>().sharedMaterial;
    }
}
