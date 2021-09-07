using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    public float speed;
    //Move direction is in world space and normalized.
    public Vector2 moveDirection;

    public GPAudioScript audioSource;

    //Asteroid prefab copies 
    public AsteroidTypes asteroidTypes;

    private bool isHit = false;
    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        //Get a common audio src in scene.
        var aA = GameObject.FindGameObjectsWithTag("aud_src");
        audioSource = aA[0].GetComponent<GPAudioScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
      //  moveDirection = Random.insideUnitCircle.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = rigidbody2d.position + (speed * Time.deltaTime * moveDirection);
        rigidbody2d.MovePosition(pos);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!collider.CompareTag("ast_dest"))
        OnHit(collider);
    }

    //Asteroid on hit
    private void OnHit(Collider2D collider)
    {
        if (!isHit)
        {
            //Play hit sound
            audioSource.PlaySound(GPAudioScript.Audios.BOOM);

            if (collider.gameObject.CompareTag("bullet"))
            {
                var particle = Instantiate(asteroidTypes.particleEffect);
                particle.transform.position = collider.gameObject.transform.position;
                Destroy(particle, 1.0f);
                Destroy(collider.gameObject);
            }

            //If it is a large asteroid spawn two medium asteroids and send them in same dir.
            if (gameObject.CompareTag("large_asteroid"))
            {
                //Create two medium asteroids
                var astA = Instantiate(asteroidTypes.mediumAsteroid);
                var astB = Instantiate(asteroidTypes.mediumAsteroid);
                //set their pos to this.pos
                astA.transform.position = astB.transform.position = transform.position;
                //rotate to some variance
                astA.transform.Rotate(Vector3.forward, Random.Range(0, 360));
                astB.transform.Rotate(Vector3.forward, Random.Range(0, 360));
                //Set their move direction
                var vecA = Quaternion.AngleAxis(-30, Vector3.forward) * this.moveDirection;
                var vecB = Quaternion.AngleAxis(+30, Vector3.forward) * this.moveDirection;

                astA.GetComponent<AsteroidScript>().moveDirection = vecA;
                astB.GetComponent<AsteroidScript>().moveDirection = vecB;

            }
            else if (gameObject.CompareTag("medium_asteroid"))
            {
                var astA = Instantiate(asteroidTypes.smallAsteroid);
                var astB = Instantiate(asteroidTypes.smallAsteroid);
                //Set position and rotation
                astA.transform.position = astB.transform.position = transform.position;
                astA.transform.Rotate(Vector3.forward, Random.Range(0, 360));
                astB.transform.Rotate(Vector3.forward, Random.Range(0, 360));
                //Set their move direction
                var vecA = Quaternion.AngleAxis(-30, Vector3.forward) * this.moveDirection;
                var vecB = Quaternion.AngleAxis(+30, Vector3.forward) * this.moveDirection;

                astA.GetComponent<AsteroidScript>().moveDirection = vecA;
                astB.GetComponent<AsteroidScript>().moveDirection = vecB;
            }
            else if (gameObject.CompareTag("small_asteroid"))
            {
                //Do nothing
            }

            var p = Instantiate(asteroidTypes.particleEffect1);
            p.transform.position = transform.position;
            Destroy(p, 3.0f);

            Destroy(this.gameObject);

            //Invoke OnAsteroidDestroy event 
            AllEventsScript.OnAsteroidDestroy?.Invoke(tag);

            isHit = true;
        }
    }


}
