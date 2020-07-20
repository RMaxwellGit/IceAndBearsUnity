using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BearBehaviour : MonoBehaviour
{
	/*
	*This class handles the actions (effect and animation) of a single bear. It also handles player input to the bear.
	*/

	public SpriteRenderer myRenderer;
	public Sprite standSprite, digSprite, throwSprite, stunSprite;
	public float digTime, throwTime, stunTime;
	public static float staticDigTime, staticThrowTime;
	public BearBehaviour partnerBear;

	string relevantInput;
	bool inputRecieved;
	bool isDigging;
	bool isStunned;
	bool isFrozen;

	public static BearBehaviour leftBear, rightBear;

	public enum BearSide {Left, Right, Both};
	BearSide mySide;

	void Start() {
		inputRecieved = false;
		isDigging = false;
		isStunned = false;
		isFrozen = false;

		if (myRenderer.flipX) {
			relevantInput = "Right";
			mySide = BearSide.Right;
			rightBear = this;
			GameStateManager.rightBear = rightBear;
		} else {
			relevantInput = "Left";
			mySide = BearSide.Left;
			leftBear = this;
			GameStateManager.leftBear = leftBear;
		}

		staticDigTime = digTime;
		staticThrowTime = throwTime;
	}

    void Update() {
    	if (Input.GetButtonDown(relevantInput)) {
    		inputRecieved = true;
    	}

        if (inputRecieved&&!isDigging&&!isStunned&&!isFrozen) {
        	StartCoroutine("StartDig");
   	    } else if (inputRecieved&&isFrozen) {
   	    	GameStateManager.GameStart();
   	    }

   	    inputRecieved = false;
    }

    public bool GetIsDigging() {
    	return isDigging;
    }

    public void SetInputRecieved(bool input) {
    	inputRecieved = input;
    }

    IEnumerator StartDig() {
    	float relativeDigTime = digTime / GameSpeed.speedMod;
    	float relativeThrowTime = throwTime / GameSpeed.speedMod;

    	isDigging = true;
    	myRenderer.sprite = digSprite;
    	yield return new WaitForSeconds(relativeDigTime);

    	if (partnerBear.GetIsDigging()) {
    		if (mySide == BearSide.Left) {
	    		DigFromSide(BearSide.Both);
    		}
       	} else {
       		DigFromSide(mySide);
       	}

    	myRenderer.sprite = throwSprite;
    	SoundPlayer.PlaySound();

    	yield return new WaitForSeconds(relativeThrowTime);

    	myRenderer.sprite = standSprite;
    	isDigging = false;
    }

    void DigFromSide(BearSide side) {
    	IceBlockBehaviour activeBlock = IceBlockManagerBehaviour.topBlock;
    	switch (side) {
    		case BearSide.Left:
    			activeBlock.SendMessage("TakeHitFromLeft");
    			break;
    		case BearSide.Right:
    			activeBlock.SendMessage("TakeHitFromRight");
    			break;
    		case BearSide.Both:
    			activeBlock.SendMessage("TakeHitFromBoth");
    			break;
    		default:
    			Debug.Log("Bear attempted to dig from impossible side");
    			break;
    	}
    }

    IEnumerator BeStunned() {
    	isStunned = true;

    	StopCoroutine("StartDig");
    	isDigging = false;

    	yield return null; //delay the game by one frame so that StartDig fully stops
    	myRenderer.sprite = stunSprite;
    	yield return new WaitForSeconds(stunTime);
    	myRenderer.sprite = standSprite;

    	isStunned = false;
    }

    public IEnumerator FreezeBear() {
    	isFrozen = true;

    	StopCoroutine ("StartDig");
    	isDigging = false;

    	yield return null;

    	myRenderer.sprite = stunSprite;
    }

    public void UnFreezeBear() {
    	myRenderer.sprite = standSprite;

    	isFrozen = false;
    }
}
