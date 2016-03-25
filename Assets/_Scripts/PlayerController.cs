// 
// Author: Jiho Yoo
// Date: March 25, 2016
// Description: The player kills enemies and gets scores by hit the berrels
//              
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using System.Collections;
//using System.UI;


public class PlayerController : MonoBehaviour
{
    // PUBLIC INSTANCE VARIABLES - Exposed on the inspector
    public ParticleSystem muzzleFlash;
    public GameObject impact;
    public Animator rifleAnimator;
    public AudioSource bulletFireSound;
    public AudioSource bulletImpactSound;
    public GameObject explosion;
   // public int score; //NOTE: static = belongs to the class and not an instance of the class
    public Scoring scoring;
   
    
    

    // PRIVATE INSTANCE VARIABLES
    //Text _scoreText; //INSERT: PRIVATE?
    private int _score;
    private GameObject[] _impacts;
    private int _currentImpact = 0;
    private int _maxImpacts = 5;
    private Transform _transform;

    private bool _shooting = false; // gun idle is not shooting




	// Use this for initialization
	void Start ()
	{
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);

        // reference to the gameObject's transform component
        this._transform = gameObject.GetComponent<Transform>(); 
        // create an object pool - loop 5 objects
	    this._impacts = new GameObject[this._maxImpacts];
	    for (int impactCount = 0; impactCount < this._maxImpacts ; impactCount++)
	    {
	        this._impacts[impactCount] = (GameObject) Instantiate(this.impact);
            //Cursor.visible = false;
	    }
	}

    
   
	
	// Update is called once per frame
	void Update () {

         
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);
	    // play muzzle flash and shoot impact when left mouse button is pressed
	    if (CrossPlatformInputManager.GetButtonDown( "Fire1" ))
	    {
            this._shooting = true;
	        this.muzzleFlash.Play();
	        this.bulletFireSound.Play();
	        this.rifleAnimator.SetTrigger( "Fire" );
            //Cursor.visible = false;
	    }

	    if (CrossPlatformInputManager.GetButtonUp("Fire1"))
	    {
            this._shooting = false;
           // Cursor.visible = false;
	    }

       

	}

    // Physics Effects
    void FixedUpdate()
    {

        if (this._shooting)
        {
            this._shooting = false;
            RaycastHit hit;
            //Cursor.visible = false;
            Debug.DrawRay( this._transform.position, this._transform.forward, Color.green );
            if (Physics.Raycast (this._transform.position, this._transform.forward, out hit, 50f))
            {
                // whenever shots hit barrel, it explodes
                if (hit.transform.CompareTag("Barrel"))
                {
                    Destroy( hit.transform.gameObject );
                    Instantiate( this.explosion, hit.point, Quaternion.identity );
                    this.explosion.transform.position = hit.point;
                    this.scoring.ScoreValue =+ 10;
                    
                    
                }

                // show particle system at location of ray impact
                this._impacts[this._currentImpact].transform.position = hit.point;
                this._impacts[this._currentImpact].GetComponent<ParticleSystem>().Play();
                this.bulletFireSound.Play();

                // ensure array does not go out of bounds
                if (++this._currentImpact >= this._maxImpacts)
                {
                    this._currentImpact = 0;
                }
            }
        }
    }
}
