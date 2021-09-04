using System.Collections;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public BulletMoverScript bulletMover;
    public GameObject currentShip;
    public GPAudioScript gPAudioSource;
    public UILivesBarScript livesBarScript;

    public GameObject spaceShipPrefab;

    uint playerLives = 0;

    private void OnEnable()
    {
        AllEventsScript.OnShipDestroyed += this.OnPlayerShipDestroyed;
    }

    private void OnDisable()
    {
        AllEventsScript.OnShipDestroyed -= this.OnPlayerShipDestroyed;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerLives = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (playerLives != 0)
            {
                ResetShip();
            }
        }
    }

    private void ResetShip()
    {
        float rotation = 0;
        if (currentShip != null)
        {
            rotation = currentShip.transform.rotation.eulerAngles.z;
            Destroy(currentShip); 
        }

        currentShip = Instantiate(spaceShipPrefab);
        currentShip.GetComponent<SpaceshipPhysScript>().moverScript = bulletMover;
        //Set rotation of ship to previous rotation
        currentShip.GetComponent<SpaceshipPhysScript>().currRotation = rotation;
        currentShip.transform.Rotate(Vector3.forward, rotation);
    }

    //Events
    void OnPlayerShipDestroyed()
    {
        playerLives--;
        livesBarScript.SetLives(playerLives);

    }
}
