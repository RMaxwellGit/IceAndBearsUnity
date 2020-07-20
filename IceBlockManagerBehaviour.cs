using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockManagerBehaviour : MonoBehaviour
{
    /*
    *This class handles the spawning of IceBlocks, as well as tracking the position of each block in the hierarchy.
    */

    public static IceBlockBehaviour topBlock;

    public GameObject toSpawn;
    static GameObject[] iceBlocks; //this is static to tell the compiler to not free it from memory when an index is free'd during a Destroy() call
    //although only one of these should exist in a game, so it being static makes sense

    static Transform[] slots;

    void Start() {
        slots = new Transform[3] {transform.GetChild(0), transform.GetChild(1), transform.GetChild(2)};
    	//instantiate three blocks  
        GameObject top, middle, bottom;      
    	top = Instantiate(toSpawn, slots[0]);
    	middle = Instantiate(toSpawn, slots[1]);
    	bottom = Instantiate(toSpawn, slots[2]);

        iceBlocks = new GameObject[3] {top, middle, bottom};

        if (top.GetComponent<IceBlockBehaviour>() != null) {
            topBlock = iceBlocks[0].GetComponent<IceBlockBehaviour>();
        }

        //set reference in GameStateManager
        GameStateManager.blockManager = this;
    }

    public void HandleBrokenBlock() {
        MoveUpBlocks();
        topBlock = iceBlocks[0].GetComponent<IceBlockBehaviour>();
        SpawnNewBlock(2);

        ScoreBehaviour.AddPoints(1);

        GameSpeed.IncreaseSpeed();
    }

    void MoveUpBlocks() {
        for (int i = 0; i < 2; i++) {
            iceBlocks[i] = iceBlocks[i+1];
            iceBlocks[i].transform.SetParent(slots[i]);
            iceBlocks[i].transform.localPosition = Vector3.zero;
        }
    }

    void SpawnNewBlock(int position) {
        iceBlocks[position] = Instantiate(toSpawn, slots[position]);
    }

    public void ClearBlocks() {
        topBlock = null;

        for (int i = 0; i < 3; i++) {
            Destroy(iceBlocks[i]);
        }
    }

    public void FillBlocks() {
        for (int i = 0; i < 3; i++) {
            SpawnNewBlock(i);
        }

        topBlock = iceBlocks[0].GetComponent<IceBlockBehaviour>();
    }
}
