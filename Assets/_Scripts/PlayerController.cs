/*
 *  Jiho Yoo / 300813612
 */
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//PUBLIC MEMBER VARIABLES
	public Transform flashPoint;
	public GameObject muzzleFlash;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Instantiate (this.muzzleFlash, flashPoint.position, Quaternion.identity);
		}
	}
}
