using UnityEngine;
using System.Collections;

public class RandomFloatNew : MonoBehaviour {

	public Vector3 center; //Center of the random floating, that everything floats around
	public Vector3 floatDimensions; //Each value represents the radius 
                                    //from center to edge of float area
    public float floatDuration;

	public GameObject sphereSizeObject;

	private Vector3 startLocation; 
	private Vector3 destination;
	
	bool check = false; //to check if in coroutine or not
	float i; //to store time lerp counter

	private Vector3 testVector = new Vector3(0,0,0); //sets testVector to 0's
	
	// Use this for initialization
	void Start () {
	
		//Debug.Log ("spheresize: " + sphereSizeObject.GetComponent<Renderer>.bounds.size.x);
		center = Camera.main.transform.position;
		//Debug.Log (center);
		startLocation = transform.position; //sets current location to start loc
		destination = testVector; //sets desintation of float to 0
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//run coroutine ONLY if check is FALSE 
		if (!check) { 
			
			StartCoroutine(randomFloat()); //call randomFloat coroutine to begin floating
		}
	}
	

    /*
     * Does the actual floating as a Coroutine, so that other things can
     * be done while the object is floating
     */
	IEnumerator randomFloat() { 
	
	  //Vector3 startLocation; //starting location of lerp 
	  //Vector3 destination; //ending location of lerp
	  
	  //Quaternion startRotation; //starting rotation
	  //Quaternion endRotation; //ending rotation
	
	  check = true; //ensures coroutine doesn't run in update again

	  destination = new Vector3(center.x + Random.Range(-floatDimensions.x, floatDimensions.x),
		                        center.y + Random.Range(-floatDimensions.y, floatDimensions.y), 
		                        center.z + Random.Range(-floatDimensions.z, floatDimensions.z));
		
	  /* This part handles random rotation. Comment out if not desired */
	  //startRotation = this.transform.rotation;
	  //transform.LookAt(new Vector3(center.x + Random.Range(-floatDimensions.x, floatDimensions.x), 
	  //                             center.y + Random.Range(-floatDimensions.y, floatDimensions.y), 
	  //                             center.z + Random.Range(-floatDimensions.z, floatDimensions.z)));
		
	  //endRotation = this.transform.rotation; 
	  /* end random rotation */
		
	  //uses lerp to change location in duration of time
	  for (i=0.0f; i<floatDuration; i+=Time.deltaTime ) {
			
	    //actual movement
	    this.transform.position = Vector3.Lerp(startLocation, destination, i/floatDuration );
		this.transform.LookAt (Camera.main.transform.position);  
	    //random rotation - disable of not desired
	    //this.transform.rotation = Quaternion.Lerp (startRotation, endRotation, i/floatDuration);
		  
	    yield return null; //ensures the for loop runs again if condition hasn't been met
			
	  }
	  
	  //this.transform.LookAt (Camera.main.transform.position);  
	  startLocation = transform.position; //makes position the new start 
	  check = false; //ensures coroutine runs again in update 
	
	}
}
