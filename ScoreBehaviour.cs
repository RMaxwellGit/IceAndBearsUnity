using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBehaviour : MonoBehaviour
{
	/*
	*This class handles tracking the player's score and updating the score board accordingly.
	*/

    public static float score;
    static Text board;

    void Start() {
    	score = 0;
    	board = GetComponent<Text>();
    }

    static void UpdateBoard() {
    	board.text = "Score: " + score;
    }

    public static void AddPoints(float points) {
    	score += points;
    	UpdateBoard();
    }

    public static void ResetScore() {
    	score = 0;
    	UpdateBoard();
    }
}
