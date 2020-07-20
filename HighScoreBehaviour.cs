using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreBehaviour : MonoBehaviour
{
 	/*
	*This class handles tracking the player's highscore and updating the highscore board accordingly.
	*/

    public static float highscore;
    static Text board;

    void Start() {
    	highscore = 0;
    	board = GetComponent<Text>();
    }

    static void UpdateBoard() {
    	board.text = "Highscore: " + highscore;
    }

    public static void CheckNewHighscore(float score) {
    	if (score > highscore) {
	    	highscore = score;
	    	UpdateBoard();
    	}
    }
}
