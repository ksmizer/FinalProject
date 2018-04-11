using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject player;
	public float cameraHeight = 20.0f;
	
	void Update () {
		float player_x = player.transform.position.x;
		float player_y = player.transform.position.y;
		float player_z = player.transform.position.z;
		
		float rounded_x = RoundToNearestPixel(player_x);
		float rounded_y = RoundToNearestPixel(player_y);
		float rounded_z = RoundToNearestPixel(player_z);
		
		Vector3 pos = new Vector3(rounded_x, rounded_y, rounded_z);
		pos.y += cameraHeight;
		pos.z -= cameraHeight;
		pos.x -= cameraHeight;
		transform.position = pos;
	}
	
	float pixelToUnits = 240f;
	
	float RoundToNearestPixel (float coordinate) {
		float valueInPixels = coordinate * pixelToUnits;
		valueInPixels = Mathf.Round(valueInPixels);
		float roundedCoordinate = valueInPixels * (1 / pixelToUnits);
		return roundedCoordinate;
	}
}
