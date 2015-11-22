/**
 * CurvedGUI
 * 
 * Author(s): Flying_Banana
 * Created: 18-6-2015
 */

using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {
	#region Public Variables
	// Rotation Speed
	public float speed = 100.0f;

	// Should Rotate
	public bool shouldRotate = true;
	
	// Whether panel is going left
	public bool goingLeft;
	#endregion

	// Update is called once per frame
	void Update () {
		if (!shouldRotate)
			return;

		float actual = speed * Time.deltaTime;

		if (goingLeft) {
			actual *= -1;
		}

		transform.localPosition = new Vector3(transform.localPosition.x + actual, transform.localPosition.y, transform.localPosition.z);

		if (transform.localPosition.x >= UICurvedDisplay.ParentCurvedDisplay(transform).GetComponent<RectTransform>().rect.width / 2 + 110) {
			goingLeft = true;
		} else if (transform.localPosition.x <= -UICurvedDisplay.ParentCurvedDisplay(transform).GetComponent<RectTransform>().rect.width / 2 - 110) {
			goingLeft = false;
		}
	}
}
