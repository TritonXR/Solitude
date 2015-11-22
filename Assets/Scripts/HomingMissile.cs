using UnityEngine;
using System.Collections;

public class HomingMissile : MonoBehaviour {
	
	private float rocketSpeed; //speed of laser
	private float rocketDamage; //damage of laser
	public float homingDelay;
	
	public GameObject target; //Ship the laser is targeting
	
	public void Initialize(float rocketSpeed, float rocketDamage, GameObject target) {
	
	  this.rocketSpeed = rocketSpeed;
	  this.rocketDamage = rocketDamage;
	  this.target = target;
	  
	  StartCoroutine(RocketHoming());
	
	}
	
	// Use this for initialization
	void Fire () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
	
	  Data enemyData;
	
	  if (enemyData = other.GetComponent<Data>()) {
	  
	    enemyData.damage (rocketDamage);
	    GameObject.Destroy (this.gameObject);
	    
	  
	  }
	}
	
	IEnumerator RocketHoming() {
	
	  Quaternion startRotation = this.transform.rotation;
	  this.transform.LookAt (target.transform.position);
	  Quaternion endRotation = this.transform.rotation;
	  this.transform.rotation = startRotation;
	  
	  float i = 0;
	  while (true) {
	    
	    Quaternion newRotation = Quaternion.Lerp (startRotation, endRotation, i/homingDelay);
	    this.transform.rotation = newRotation;
			this.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        this.GetComponent<Rigidbody>().AddForce(this.transform.forward * rocketSpeed, ForceMode.Impulse);
        
		this.transform.LookAt (target.transform.position);
		endRotation = this.transform.rotation;
		this.transform.rotation = newRotation;
        
        i+=Time.deltaTime;
        yield return null;
	  
	  }
	}	
}
