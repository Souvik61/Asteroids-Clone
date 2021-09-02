using UnityEngine;

public class SpaceshipScript : MonoBehaviour
{

    //Inspector vars
    public float speed = 0;
    public float rotationSpeed = 0;
    //Internal vars
    float x = 0;
    float y = 0;
    float zRotation = 0;

    private EdgeCollider2D selfCollider;

    private void Awake()
    {
        selfCollider = GetComponent<EdgeCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        if (Input.GetKey(KeyCode.W))
        {
            y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            y = -1;
        }
        else 
        {
            y = 0;
        }

        //Rotation
        if (Input.GetKey(KeyCode.D))
        {
            zRotation = -1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            zRotation = 1;
        }
        else
        {
            zRotation = 0;
        }

        transform.Translate(new Vector2(0, y) * Time.deltaTime * speed);
        transform.Rotate(0, 0, zRotation * Time.deltaTime * rotationSpeed, Space.World);

    }
}
