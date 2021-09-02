using UnityEngine;

public class ShipTeleportScript : MonoBehaviour
{
    //X
    public float leftXLimit = 0;
    public float rightXLimit = 0;
    //Y
    public float topYLimit = 0;
    public float bottomYLimit = 0;

    // Update is called once per frame
    void Update()
    {
        //Check y pos
        if (transform.position.y > topYLimit)
        {
            transform.position = new Vector2(transform.position.x, bottomYLimit);
        }
        else if (transform.position.y < bottomYLimit)
        { 
            transform.position = new Vector2(transform.position.x, topYLimit);
        }

        //Check x pos
        if (transform.position.x > rightXLimit)
        {
            transform.position = new Vector2(leftXLimit, transform.position.y);
        }
        else if (transform.position.x < leftXLimit)
        {
            transform.position = new Vector2(rightXLimit, transform.position.y);
        }

    }
}
