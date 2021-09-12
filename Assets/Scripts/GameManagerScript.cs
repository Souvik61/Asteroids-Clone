using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public BulletMoverScript bulletMover;
    public ScoreManagerScript scoreManagerScript;
    public GameObject currentShip;
    public GPAudioScript gPAudioSource;
    public UILivesBarScript livesBarScript;
    public GameObject gameOverText;
    public TMP_Text scoreText;

    public GameObject spaceShipPrefab;

    //Internal vars
    enum PlayerState { ALIVE, DEAD } ;
    PlayerState playerState;
    uint playerLives = 0;
    //Game states
    enum GameStates { RUNNING, STOPPED };
    GameStates gameState;

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
        gameState = GameStates.RUNNING;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //pop scene
            SceneManager.LoadSceneAsync(0);
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

    //Game events
    void OnGameOver()
    {
        gameState = GameStates.STOPPED;
        scoreText.text = "Your Score : " + scoreManagerScript.PlayerScore;
        gameOverText.SetActive(true);
        
    }

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

        //if all player lives over
        if (playerLives == 0)
        {
            StartCoroutine(nameof(StartGameOverEvent));
        }
    }

    IEnumerator StartGameOverEvent()
    {
        yield return new WaitForSeconds(2.0f);
        OnGameOver();
    }
}
