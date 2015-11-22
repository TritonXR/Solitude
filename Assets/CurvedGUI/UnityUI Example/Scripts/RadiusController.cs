/**
 * CurvedGUI
 * 
 * Author(s): Flying_Banana
 * Created: 18-6-2015
 */

using UnityEngine;
using System;

public class RadiusController : MonoBehaviour {
	public void ChangeCurvature(float value) {
		UICurvedDisplay.ParentCurvedDisplay(transform).curveRadius = (float)Math.Pow(value, 3) * 10000 + 700;
	}
}
