using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateManager 
{
	public static IceBlockManagerBehaviour blockManager;
	public static BearBehaviour leftBear, rightBear;

   static float minSecondsForDeathScreen = 2;
   static bool forceDeathScreen = false;

   	public static IEnumerator GameOver() {
         forceDeathScreen = true;
   		//Reset speed
   		GameSpeed.speedMod = 1;
   		//Clear IceBlocks
   		blockManager.ClearBlocks();
   		//Set both bears to stunned until start
   		//Also sets a, w, left key, right key, left button and right button to call GameStart()
   		leftBear.StartCoroutine("FreezeBear");
   		rightBear.StartCoroutine("FreezeBear");
   		//Set highscore
   		HighScoreBehaviour.CheckNewHighscore(ScoreBehaviour.score);
   		//Play a sound
   		SoundPlayer.PlaySound();
         yield return new WaitForSeconds(minSecondsForDeathScreen);
         forceDeathScreen = false;
         //Refill IceBlockManager with IceBlocks
         blockManager.FillBlocks();
   	}

   	public static void GameStart() {
         if (!forceDeathScreen) {
            //Reset points
            ScoreBehaviour.ResetScore();
            //Set both bears to standing
            //also sets a, left key, left button to dig left
            //and sets d, right key, right button to dig right
            leftBear.UnFreezeBear();
            rightBear.UnFreezeBear();
            //Fill stove
            StoveBehaviour.ResetStove();
            //Play a sound
            SoundPlayer.PlaySound();
         }
   	}
}
