/**
 * CurvedGUI
 * 
 * Author(s): Flying_Banana
 * Created: 15-7-2014
 */

using UnityEngine;
using System;

/// <summary>
/// This is the main script that repositions every child in the transform this script is attached to so we get a curved
/// appearance. Depending on the curving radius, the RectTransform's size, and the x position of the child transforms, 
/// every object is positioned and rotated accordingly. Every child is processed recursively unless it has a script 
/// component UIStraightDisplay attached. This serve as a marker for this script to not process its children. However 
/// that object itself will still be tilted.
/// 
/// Note:
/// - set the UI camera to perspective projection for best effect
/// - set the curveRadius to at least more than three times the screen width for optimal effect
/// - UI elements placed outside the screen will still be tilted
/// - when in convex mode, make sure the camera is moved sufficiently back so the effect is rendered
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class UICurvedDisplay : MonoBehaviour {
	#region Public Variables
	// The radius of the curved surface. Larger radius will create a flatter look. This must be bigger than half of the
	// attached widget's width.
	public float curveRadius;

	// If this is turned on, we will flip the calculations on the z-axis so it appears the UI is popping out.
	public bool isConvex;
	#endregion

	// Update is called once per frame
	void LateUpdate () {
		ConfigureTransform(transform);
	}

	/// <summary>
	/// This is used to find the first UICurvedDisplay parented to "self".
	/// </summary>
	/// <returns>The curved display.</returns>
	/// <param name="self">Transform trying to get its parent curved display instance.</param>
	public static UICurvedDisplay ParentCurvedDisplay(Transform self) {
		return self.GetComponentInParent<UICurvedDisplay>();
	}

	/// <summary>
	/// This method uses recursion to rearrange all the transforms with RectTransform component attached so they appear
	/// placed on a curved surface. We skip all the transforms with another UICurvedDisplay or a UIStraightDisplay
	/// component attached to it.
	/// </summary>
	/// <param name="root">Root transform.</param>
	public void ConfigureTransform(Transform root) {
		for (int i = 0; i < root.childCount; i++) {
			Transform child = root.GetChild(i);

			// We only curve if it's either a straight display, or a RectTransform
			if (child.GetComponent<UIStraightDisplay>() != null || child.GetComponent<RectTransform>() != null) {
				// Configure the depth
				// This is the pixel position relative to this transform
				Vector3 a = transform.InverseTransformPoint(child.position);

				// This calculates where we want the transforms be in pixel position, relative to this transform
				a = GetPointOnArc(a);

				// This calculates where the parent is in this transform's pixel position
				Vector3 b = transform.InverseTransformPoint(child.parent.position);

				// This calculates where we want the transform be in pixel position, relative to its parent
				Vector3 c = a - b;

				child.localPosition = c;

				// Configure the angle
				child.eulerAngles = new Vector3(child.eulerAngles.x, GetTangentAngle(a), child.eulerAngles.z);
			}

			// We only recurse if the child isn't a curved display and it isn't straight either
			if (child.GetComponent<UICurvedDisplay>() == null && (child.GetComponent<UIStraightDisplay>() == null ||
			(child.GetComponent<UIStraightDisplay>() != null && !child.GetComponent<UIStraightDisplay>().isActiveAndEnabled))) {
				ConfigureTransform(child);
			}
		}

	}

	/// <summary>
	/// Private helper method: returns the coordinate of the center of the cylindrical surface for the given height.
	/// </summary>
	/// <param name="y">The y coordinate of the center we want.</param>
	/// <returns>The center's z-coordinate.</returns>
	Vector3 GetCenter(float y) {
		float halfWidth = GetComponent<RectTransform>().rect.width / 2;
		if (curveRadius <=  halfWidth)
			throw new UnityException("Calculation exception: curveRadius must be bigger than half of its UIWidget's width!");

		float magnitude = (float)(Math.Pow((Math.Pow(curveRadius, 2) - Math.Pow(halfWidth, 2)), 0.5));

		if (!isConvex)
			magnitude *= -1;

		return new Vector3(transform.localPosition.x, y, magnitude);
	}

	/// <summary>
	/// Private helper method: returns the rotation on the y-axis for the given pivot point's coordinates.
	/// </summary>
	/// <returns>The tangent angle to the arc.</returns>
	/// <param name="current">The coordinates.</param>
	float GetTangentAngle(Vector3 current) {
		Vector3 center = GetCenter(current.y);
		Vector3 c2c = current - center;

		if (c2c.Equals(Vector3.zero))
			return 0;
	
		float angle = -(float)(Math.Atan2(c2c.z, c2c.x) / 2 / Math.PI * 360 - 90);

		if (isConvex)
			angle += 180;

		return angle;
	}

	/// <summary>
	/// Private helper method: returns the arc coordinates of the UIWidget for the given pivot point's current 
	/// coordinates.
	/// </summary>
	/// <returns>The coordinates of the widget placed on the arc.</returns>
	/// <param name="current">The current coordinates.</param>
	Vector3 GetPointOnArc(Vector3 current) {
		Vector3 center = GetCenter(current.y);
		float x = current.x;

		if (Math.Abs(x) > curveRadius) {
			x = curveRadius;
		}

		int factor = 1;
		if (isConvex) {
			factor = -1;
		}


		return new Vector3(current.x, current.y, (float)Math.Pow(Math.Pow(curveRadius, 2) - Math.Pow(x, 2), 0.5) * factor + center.z);
	}
}
