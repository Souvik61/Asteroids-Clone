using UnityEngine;

public class AsteroidSpawnScript : MonoBehaviour
{
    //this is a scriptable object
    public AsteroidTypes asteroidTypes;

    public float spawnDistance = 12.0f;
    public float spawnRate = 1.0f;
    public int amountPerSpawn = 1;
    [Range(0.0f, 45.0f)]
    public float trajectoryVariance = 30.0f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroids), spawnRate, spawnRate);
    }

    //Spawn asteroids
    void SpawnAsteroids()
    {
        for (int i = 0; i < this.amountPerSpawn; i++)
        {
            // Choose a random direction from the center of the spawner and
            // spawn the asteroid a distance away
            Vector2 spawnDirOnUnitCir = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirOnUnitCir * spawnDistance;

            //Draw ray for debugging
            Debug.DrawRay(transform.position, spawnPoint, Color.red,0.5f);

            //set the asteroid on spawn point 
            var aster = CreateRandomAsteroid();
            aster.transform.position = spawnPoint;
            aster.transform.Rotate(Vector3.forward, Random.Range(0, 360));//rotate random angle

            //send it to center of screen with some variance

            Vector2 vecA = (this.transform.position - spawnPoint).normalized;
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            vecA = Quaternion.AngleAxis(variance, Vector3.forward) * vecA;

            aster.GetComponent<AsteroidScript>().moveDirection = vecA;

        }

    }
    //Generate a random asteroid
    GameObject CreateRandomAsteroid()
    {
        GameObject output = null;
        switch (Random.Range(0, 3))
        {
            case 0:
                output = Instantiate(asteroidTypes.smallAsteroid);
                break;
            case 1:
                output = Instantiate(asteroidTypes.mediumAsteroid);
                break;
            case 2:
                output = Instantiate(asteroidTypes.largeAsteroid);
                break;
            default:
                break;
        }
        return output;
    }

}
