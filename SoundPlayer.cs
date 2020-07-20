using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
	static AudioSource myPlayer;

    void Start()
    {
        myPlayer = GetComponent<AudioSource>();
    }

    public static void PlaySound() {
    	myPlayer.pitch = RandomPitch(0.5f, 1.05f);

    	myPlayer.Play();
    }

    static float RandomPitch(float min, float max) {
    	float range = max - min;
    	float newPitch = Random.value * range;

    	newPitch += min;

    	return newPitch;
    }
}
