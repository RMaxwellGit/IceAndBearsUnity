using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBehaviour : MonoBehaviour
{
	public float maxTimeRemaining;
	static float timeAddedByLog;
	static float currentTime;
	static float staticMaxTime;

	public SpriteRenderer myRenderer;
	public Sprite fullSprite, halfSprite, emberSprite, outSprite;

	static bool stoveActive;
    // Start is called before the first frame update
    void Start()
    {
    	stoveActive = true;

    	currentTime = maxTimeRemaining;
        timeAddedByLog = maxTimeRemaining / 3;

        staticMaxTime = maxTimeRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        SetSpriteFromTime(currentTime);
        if (currentTime >= 0) {
	        currentTime -= Time.deltaTime * GameSpeed.speedMod;
        } else if (stoveActive) {
        	StartCoroutine(GameStateManager.GameOver());
        	stoveActive = false;
        }
    }

    void SetSpriteFromTime(float curTime) {
    	float timeFraction = curTime / maxTimeRemaining;

    	timeFraction = Mathf.Clamp(timeFraction, 0, 1);

    	if (timeFraction > 0.66f) {
    		myRenderer.sprite = fullSprite;
		} else if (timeFraction > 0.33f) {
			myRenderer.sprite = halfSprite;
		} else if (timeFraction > 0) {
			myRenderer.sprite = emberSprite;
		} else {
			myRenderer.sprite = outSprite;
		}
    }

    public static void AddLogToFire() {
    	currentTime = staticMaxTime;
    }

    public static void ResetStove() {
    	currentTime = staticMaxTime;
    	stoveActive = true;
    }

    public static float GetCurrentTime() {
        return currentTime;
    }
}
