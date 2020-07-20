using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSpeed
{
	public static float speedMod = 1;

	static float speedChange = 0.05f;
	public static void IncreaseSpeed() {
		speedMod += speedChange;
	}

	public static void ResetSpeed() {
		speedMod = 1;
	}
}
