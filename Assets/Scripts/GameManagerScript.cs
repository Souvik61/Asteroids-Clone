using System.Collections;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public BulletMoverScript bulletMover;
    public GameObject currentShip;
    public GPAudioScript gPAudioSource;
    public UILivesBarScript livesBarScript;

    public GameObject spaceShipPrefab;

    //Internal vars
    enum PlayerState { ALIVE, DEAD } ;
    PlayerState playerState;
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
        playerState = PlayerState.ALIVE;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (playerLives != 0)
            {
                //ResetShip();
            }
        }
    }

    private void ResetShip(float prevRotation)
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
        currentShip.GetComponent<SpaceshipPhysScript>().currRotation = prevRotation;
        currentShip.transform.Rotate(Vector3.forward, prevRotation);
        playerState = PlayerState.ALIVE;
    }

    IEnumerator StartResetShip(float prevRotation)
    {
        yield return new WaitForSeconds(2);
        ResetShip(prevRotation);
    }

    //Events
    void OnPlayerShipDestroyed(float prevShipRotation)
    {
        playerLives--;
        livesBarScript.SetLives(playerLives);
        //TryResetShip();
        if (playerLives != 0)
        {
            StartCoroutine(nameof(StartResetShip), prevShipRotation);
        }
        playerState = PlayerState.DEAD;


    }
}
