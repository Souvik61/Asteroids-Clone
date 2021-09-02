using UnityEngine;

public class BulletMoverScript : MonoBehaviour
{
    //Inspector
    public float bulletSpeed = 0;

    private uint arrayIndexPtr = 0;
    private Transform[] bulletTransforms = new Transform[20];
    private float[] timersOfBullets = new float[20];

    private void Awake()
    {
        //Initialise all timers to -1
        for (int i = 0; i < 20; i++) { timersOfBullets[i] = -1.0f; }
    
    }

    // Update is called once per frame
    void Update()
    {

        //Loop and update all timers
        for (int i = 0; i < 20; i++)
        {

            //if timer is set to -1
            if ((int)timersOfBullets[i] == -1)
            {

            }
            //if timer is almost zero 
            else if (timersOfBullets[i] < 0.2f)
            {
                if (bulletTransforms[i] != null)
                {
                    Destroy(bulletTransforms[i].gameObject);
                }
                timersOfBullets[i] = -1;
            }
            //else decrease the timer
            else
            {
                timersOfBullets[i] = timersOfBullets[i] - Time.deltaTime;
            }

        }

        foreach (var bullet in bulletTransforms)
        {
            if (bullet != null)
            {
                bullet.Translate(0, bulletSpeed * Time.deltaTime, 0, Space.Self);
            }
        }
    }

    public void AddBullet(GameObject bullet)
    {
        if (arrayIndexPtr > 19) { arrayIndexPtr = 0; }

        if (bulletTransforms[arrayIndexPtr] != null)
        {
            DestroyImmediate(bulletTransforms[arrayIndexPtr].gameObject);   
        }
        bulletTransforms[arrayIndexPtr] = bullet.transform;
        timersOfBullets[arrayIndexPtr] = 10.0f;
        arrayIndexPtr++;
    
    }

}
