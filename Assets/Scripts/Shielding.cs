using UnityEngine;
using System.Collections;

public class Shielding : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnTriggerEnter(Collider shield) {
        //Debug.Log ("die");
        if (shield.gameObject.GetComponent<Laser>())
        {
            if (shield.gameObject.GetComponent<Laser>().isEnemy)
            {
                Destroy(shield.gameObject);
                GetComponent<AudioSource>().Play();
            }
        }
		//if (shield
	}
}
