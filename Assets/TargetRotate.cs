using UnityEngine;
using System.Collections;

public class TargetRotate : MonoBehaviour {
    public float rotateAmount;

    private Transform rotater;

	// Use this for initialization
	void Start () {
        rotater = this.gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 angles = new Vector3();

        //Rotate by rotateAmount every second.
        angles.z += rotateAmount * Time.deltaTime;
        rotater.Rotate(angles);
	}
}
