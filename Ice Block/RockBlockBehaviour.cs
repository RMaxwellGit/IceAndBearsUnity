using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBlockBehaviour : MonoBehaviour
{
	bool rockOnLeft;

    public void SetOnLeft(bool onLeft) {
    	rockOnLeft = onLeft;
    }

    void Start() {
        gameObject.name = "RockBlock";
    }

    //Message methods. These functions give other classes an interface to the currently active (Type)BlocKBehaviour
    //This allows each of these c# scripts to behave differently to the same interactions

    public void TakeHitFromLeft() {
    	if (!rockOnLeft) {
	    	SendMessage("BreakBlock");
	    	SendMessage("SpawnThrownSnow", BearBehaviour.BearSide.Left);
    	} else {
	        BearBehaviour.leftBear.StartCoroutine("BeStunned");
    	}
    }

    public void TakeHitFromRight() {
    	if (rockOnLeft) {
	    	SendMessage("BreakBlock");
	    	SendMessage("SpawnThrownSnow", BearBehaviour.BearSide.Right);
    	} else {
	        BearBehaviour.rightBear.StartCoroutine("BeStunned");
    	}
    }

    public void TakeHitFromBoth() {
    	if (rockOnLeft) {
	        BearBehaviour.leftBear.StartCoroutine("BeStunned");
	    	SendMessage("SpawnThrownSnow", BearBehaviour.BearSide.Right);
	    } else {
	        BearBehaviour.rightBear.StartCoroutine("BeStunned");
	    	SendMessage("SpawnThrownSnow", BearBehaviour.BearSide.Left);
    	}

        SendMessage("BreakBlock");

    }
}
