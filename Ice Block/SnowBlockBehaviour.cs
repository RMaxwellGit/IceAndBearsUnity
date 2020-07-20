using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBlockBehaviour : MonoBehaviour
{
    void Start() {
        gameObject.name = "SnowBlock";
    }

    //Message methods. These functions give other classes an interface to the currently active (Type)BlocKBehaviour
    //This allows each of these c# scripts to behave differently to the same interactions

    public void TakeHitFromLeft() {
    	SendMessage("BreakBlock");
    	SendMessage("SpawnThrownSnow", BearBehaviour.BearSide.Left);
    }

    public void TakeHitFromRight() {
    	SendMessage("BreakBlock");
    	SendMessage("SpawnThrownSnow", BearBehaviour.BearSide.Right);
    }

    public void TakeHitFromBoth() {
        BearBehaviour.leftBear.StartCoroutine("BeStunned");
        BearBehaviour.rightBear.StartCoroutine("BeStunned");
    }
}
