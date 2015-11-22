using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public Material friendlyLaser; //green laser
	public Material unfriendlyLaser; //red laser

	private float laserSpeed; //speed of laser
	private float accuracyOffset; //accuracy offset 
	private float laserDamage; //damage of laser
    
    private bool isEnemy = false; //Did the laser come from an enemy?
    
    private Vector3 targetLoc; //Ship the laser is targeting
    private Vector3 destination; //Destination + accuracy offset
    
    private float initialDistanceToTarget;
    
    
    //call this method immediatly after Instantiating the object
    public void Initialize(bool isEnemy, float laserSpeed, float accuracyOffset, 
                           float laserDamage, Vector3 targetLoc) {
                           
                           
       //constructifies
       this.isEnemy = isEnemy;
       this.laserSpeed = laserSpeed;
       this.accuracyOffset = accuracyOffset;
       this.laserDamage = laserDamage;
       this.targetLoc = targetLoc;
    
      //checks if the laser came from an enemy ship
      if (isEnemy) {
      
        //red material
        this.GetComponent<MeshRenderer>().material = unfriendlyLaser;
        
      }
      
      else {
      
        this.GetComponent<MeshRenderer>().material = friendlyLaser;
      
      }
        
      //Calls the "Fire" method (Pseudo start)
      Fire();
    }

	// Use this for initialization
	void Fire () {
	
	  //uses the target location, and then adds an accuracy offset
	  destination = new Vector3(targetLoc.x + Random.Range(-accuracyOffset, accuracyOffset),
	                            targetLoc.y + Random.Range (-accuracyOffset, accuracyOffset),
	                            targetLoc.z + Random.Range(-accuracyOffset, accuracyOffset));
	                            
	  //faces the new destination
	  this.transform.LookAt (destination);
	  
	  //stores the distance to the target destination
      this.initialDistanceToTarget = Vector3.Distance(this.transform.position, destination);
      
      //fires teh lazors
      this.GetComponent<Rigidbody>().AddForce(this.transform.forward*laserSpeed, ForceMode.Impulse);
	
	}
	
	// Update is called once per frame
	void Update () {
	  
	  //checks if the laser has passed it's target by a factor of 2
	  if (Mathf.Abs(Vector3.Distance(this.transform.position, destination)) > 
	      Mathf.Abs ((initialDistanceToTarget * 2))) {
	  
	    //Destroys the laser in that case
	    GameObject.Destroy (this.gameObject);
	  
	  }
	}
	
	//Runs when the laser hits a target
	void OnTriggerEnter(Collider other) {
	
	  //If the gameObject the laser hit has a data script
	  if (other.gameObject.GetComponent<Data>()) {
	  
	    //If the gameObject the laser hit is of different faction.
	    if (other.gameObject.GetComponent<Data>().isEnemy() != isEnemy) {
	    
	      //Do damage to the object
	      other.gameObject.GetComponent<Data>().damage (laserDamage);
	      
	      //Destroy the laser
	      GameObject.Destroy (this.gameObject);
	    }
	  }
	  

	
	}
}
