using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlockBehaviour : MonoBehaviour
{
    void Start() {
        gameObject.name = "WoodBlock";
    }

    //Message methods. These functions give other classes an interface to the currently active (Type)BlocKBehaviour
    //This allows each of these c# scripts to behave differently to the same interactions

    public void TakeHitFromLeft() {
		SendMessage("BreakBlock");
		SendMessage("SpawnThrownLog", BearBehaviour.BearSide.Left);
    }

    public void TakeHitFromRight() {
    	SendMessage("BreakBlock");
    	SendMessage("SpawnThrownLog", BearBehaviour.BearSide.Right);
    }

    public void TakeHitFromBoth() {
        StoveBehaviour.AddLogToFire();
        SendMessage("BreakBlock");

		SendMessage("SpawnThrownSnow", BearBehaviour.BearSide.Both);
    }
}
