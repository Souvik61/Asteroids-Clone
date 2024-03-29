﻿using System.Collections;
using UnityEngine;

public class SpaceshipPhysScript : MonoBehaviour
{
    //Inspector vars
    public float forceMultiplier = 0;
    public float rotationSpeed = 0;
    public float velocityClamp = 0;
    public float currRotation = 0;//Set rotation from outside

    public float bulletShootDelay = 0;

    public GameManagerScript managerScript;
    public BulletMoverScript moverScript;
    public StarScrollerScript starScroller;
    public GameObject bulletPrefab;
    public GameObject particlePrefab;
    public AudioClip laserShootClip;
    public AudioClip boostClip;
    public AudioSource boostAudioSource;
    //Internal vars
    float x = 0;
    float goForward = 0;
    float zRotation = 0;

    //Animation
    bool isNoHarm;

    //gun trigger delay vars
    bool readyToShoot = true;
    float roundTimer = 0.0f;
    //

    private AudioSource audioSource;
    private Collider2D selfCollider;
    private SpriteRenderer spRenderer;
    private Rigidbody2D selfBody;
    private Transform leftGun, rightGun;
    private ParticleSystem rightParticle, leftParticle;

    private void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        selfCollider = GetComponent<Collider2D>();
        selfBody = GetComponent<Rigidbody2D>();

        leftGun = transform.Find("LeftMissile");
        rightGun = transform.Find("RightMissile");
        leftParticle = transform.Find("LeftBooster").GetComponent<ParticleSystem>();
        rightParticle = transform.Find("RightBooster").GetComponent<ParticleSystem>();

        //find game manager
        managerScript = GameObject.FindGameObjectWithTag("manager").GetComponent<GameManagerScript>();
        starScroller = managerScript.starScroller;

    }

    // Start is called before the first frame update
    void Start()
    {
        //Blink space ship and take no damage until two seconds after spawn.
        StartNoHarm();
        Invoke(nameof(StopNoHarm), 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryShootProjectile();
        }

        //If going forward play boost sound else stop it
        if (goForward == 1)
        {
            if (!boostAudioSource.isPlaying)
            {
                boostAudioSource.Play();
            }
        }
        else {
            if (boostAudioSource.isPlaying)
            {
                boostAudioSource.Stop();
            }
        }

        //updates two boosters accordingly
        ProcessParticleUpdate();

        ScrollStars();

    }

    private void FixedUpdate()
    {
        //Move the ship forward
        selfBody.AddForce(transform.up * goForward * forceMultiplier, ForceMode2D.Force);

        // if velocity greater than velocityClamp, clamp velocity. else move with velocity.😎
        if (selfBody.velocity.magnitude > velocityClamp)
        {
            selfBody.velocity = selfBody.velocity.normalized * velocityClamp;
        }
        //rotate accordingly
        selfBody.MoveRotation(currRotation);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OnHit(collider);
    }


    //Other functions

    //On hit with other object
    private void OnHit(Collider2D collider)
    {
        if (collider.tag.Contains("asteroid"))
        {
            var a = Instantiate(particlePrefab);
            a.transform.position = transform.position;
            Destroy(a, 0.6f);
            Destroy(this.gameObject);
            //Pass in z rotation
            AllEventsScript.OnShipDestroyed?.Invoke(transform.rotation.eulerAngles.z);
        }

    }

    void ProcessInput()
    {
        //Set forward flags
        if (Input.GetKey(KeyCode.W)) { goForward = 1; }
        else if (Input.GetKey(KeyCode.S)) 
        { 
            //goForward = -1; 
        }
        else { goForward = 0; }

        //Rotation
        if (Input.GetKey(KeyCode.D)) { zRotation = -1; }
        else if (Input.GetKey(KeyCode.A)) { zRotation = 1; }
        else { zRotation = 0; }

        currRotation += zRotation * Time.deltaTime * rotationSpeed;

    }

    void ProcessParticleUpdate()
    {
        //If going forward play boost effects else stop it
        if (goForward == 1)
        {
            //enable left particle
            if (!leftParticle.isPlaying)
            {
                leftParticle.Play();
                var a = leftParticle.main;
                a.loop = true;
            }
            //enable right particle
            if (!rightParticle.isPlaying)
            {
                rightParticle.Play();
                var a = rightParticle.main;
                a.loop = true;
            }
        }
        else
        {
            //disable left particle
            if (leftParticle.isPlaying)
            {
                var a = leftParticle.main;
                a.loop = false;
            }
            //disable right particle
            if (rightParticle.isPlaying)
            {
                var a = rightParticle.main;
                a.loop = false;
            }
        }
    }

    void ScrollStars()
    {
        Vector2 a = selfBody.velocity;

        starScroller.xSpeed = a.x * 0.008f;
        starScroller.ySpeed = a.y * 0.008f;
    }

    void TryShootProjectile()
    {
        if (readyToShoot)
        {
            ShootProjectile();
            StartCoroutine(nameof(ShootDelayFunc));
        }
    }

    void ShootProjectile()
    {
        audioSource.PlayOneShot(laserShootClip);
        //Create left bullet
        moverScript.AddBullet(Instantiate(bulletPrefab, leftGun.position, leftGun.rotation));
        moverScript.AddBullet(Instantiate(bulletPrefab, rightGun.position, rightGun.rotation));
        
    }

    //This function delays bullet shooting for set ammount of seconds.
    IEnumerator ShootDelayFunc()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(bulletShootDelay);
        readyToShoot = true;
    }
    //Beeping animation after spawn
    IEnumerator BeepShip()
    {
        for (;;)
        {
            spRenderer.enabled = false;
            leftParticle.gameObject.GetComponent<ParticleSystemRenderer>().enabled = false;
            rightParticle.gameObject.GetComponent<ParticleSystemRenderer>().enabled = false;
            yield return new WaitForSeconds(0.25f);
            spRenderer.enabled = true;
            leftParticle.gameObject.GetComponent<ParticleSystemRenderer>().enabled = true;
            rightParticle.gameObject.GetComponent<ParticleSystemRenderer>().enabled = true;
            yield return new WaitForSeconds(0.25f);
            if (!isNoHarm)
            { break; }
        }
    }

    //start NoHarm
    void StartNoHarm()
    {
        isNoHarm = true;
        selfCollider.enabled = false;
        StartCoroutine(nameof(BeepShip));
    }
    //stop NoHarm
    void StopNoHarm()
    {
        isNoHarm = false;//this also stops beeping coroutine.
        selfCollider.enabled = true;
    }

}
