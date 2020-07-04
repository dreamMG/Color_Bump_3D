using System;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public enum Type { ChangeColor, Move, Tilt };
    
    [Tooltip("Method of obstacles")]
    public Type type;

    [Tooltip("Basic colors")]
    //COLOR
    public Material[] materials;
    private float timer;
    //MOVE
    public float moveX;
    public bool moveAndBack;
    public float speed = 4f;
    private Vector3 currentPos;
    private bool collisionWithPlayer = false;
    //TILT
    public float rotate;
    public Transform player;
    private bool tilt;

    private void Start()
    {
        currentPos = transform.position;
        player = FindObjectOfType<Controller>().transform;

        if(type == Type.Tilt)
        {
            tilt = true;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        TypeObstacles();
    }

    void TypeObstacles()
    {
        switch (type)
        {
            case (Type.ChangeColor):
                if (timer > speed)
                    ChangeColor();
                break;
            case (Type.Move):
                if (collisionWithPlayer) return;
                Move();
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (tilt && other.gameObject.tag == "Player")
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,rotate),100f);
            tilt = false;
        }
    }

    private void Move()
    {
        if (!moveAndBack)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(moveX, transform.position.y, transform.position.z), speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, new Vector3(moveX, transform.position.y, transform.position.z)) < 0.8f)
                moveAndBack = true;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, currentPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, currentPos) < 0.01f)
                moveAndBack = false;
        }
    }

    private void ChangeColor()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();

        if (renderer != null)
        {
            if (renderer.sharedMaterial.name.Equals(materials[0].name))
                renderer.material = materials[1];
            else
                renderer.material = materials[0];
        }
        timer = 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collisionWithPlayer = true;
        }
    }
}
