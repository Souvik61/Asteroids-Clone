using UnityEngine;


[CreateAssetMenu(fileName ="Asteroid types")]
public class AsteroidTypes : ScriptableObject
{
    //Asteroid prefabs
    public GameObject smallAsteroid;
    public GameObject mediumAsteroid;
    public GameObject largeAsteroid;

    //Particle effect
    public GameObject particleEffect;
    public GameObject particleEffect1;
}
