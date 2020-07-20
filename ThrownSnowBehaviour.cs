using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownSnowBehaviour : MonoBehaviour
{
    /*
    *This class handles displaying thrown snow before Destroy()ing it.
    */

	public float effectLength;
	float timeLeft;

    public SpriteRenderer myRenderer;
    public Sprite logSprite;

    void Start() {
    	timeLeft = effectLength;
    }

    void Update() {
    	timeLeft -= Time.deltaTime * GameSpeed.speedMod;

    	if (timeLeft <= 0) {
    		Destroy(gameObject);
    	}
    }

    public void ShowAsLog() {
        myRenderer.sprite = logSprite;
        transform.position = new Vector3(transform.position.x, 3.07f, transform.position.z);
    }
}
