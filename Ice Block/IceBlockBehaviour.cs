using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockBehaviour : MonoBehaviour
{
    /*
    *This class handles a single IceBlock, and is responsible for setting its type and behaviour as well as coordinating function
    *all IceBlock's need like BreakBlock().
    */

	enum BlockType {Snow, RockLeft, RockRight, Wood, COUNT}

	BlockType myType;

	public Sprite snowSprite, rockLeftSprite, rockRightSprite, woodSprite;
	public SpriteRenderer myRenderer;

    public IceBlockManagerBehaviour manager;

    public GameObject leftThrownSnow, rightThrownSnow;

    public float marginOfError;

    // Start is called before the first frame update
    void Start()
    {
        myType = SetType();
        SetSprite(myType);
        SetMethod(myType);
    }

    BlockType SetType() {
        return (BlockType)Random.Range(0, (float)(BlockType.COUNT));
    }

    void SetSprite(BlockType type) {
    	switch (type) {
    		case BlockType.RockLeft:
    			myRenderer.sprite = rockLeftSprite;
    			break;
    		case BlockType.RockRight:
    			myRenderer.sprite = rockRightSprite;
    			break;
    		case BlockType.Wood:
    			myRenderer.sprite = woodSprite;
    			break;
    		case BlockType.Snow:
    		default:
    			myRenderer.sprite = snowSprite;
    			break;
    	}
    }

    void SetMethod(BlockType type) {
    	switch (type) {
    		case BlockType.RockLeft:
                gameObject.AddComponent(typeof(RockBlockBehaviour));
                GetComponent<RockBlockBehaviour>().SetOnLeft(true);
                break;
    		case BlockType.RockRight:
                gameObject.AddComponent(typeof(RockBlockBehaviour));
                GetComponent<RockBlockBehaviour>().SetOnLeft(false);
                break;
    		case BlockType.Wood:
                gameObject.AddComponent(typeof(WoodBlockBehaviour));
    			break;
    		case BlockType.Snow:
    		default:
                gameObject.AddComponent(typeof(SnowBlockBehaviour));
    			break;
    	}
    }

    void BreakBlock() {
        manager.HandleBrokenBlock();
        Destroy(gameObject);
    }

    void SpawnThrownSnow(BearBehaviour.BearSide side) {
        switch (side) {
            case BearBehaviour.BearSide.Left:
                Instantiate(leftThrownSnow);
                break;
            case BearBehaviour.BearSide.Right:
                Instantiate(rightThrownSnow);
                break;
            case BearBehaviour.BearSide.Both:
                Instantiate(leftThrownSnow);
                Instantiate(rightThrownSnow);
                break;
            default:
                Debug.Log("Attempted to spawn thrown snow on invalid side");
                break;
        }
    }

    void SpawnThrownLog(BearBehaviour.BearSide side) {
        GameObject log;
        switch (side) {
            case BearBehaviour.BearSide.Left:
                log = Instantiate(leftThrownSnow);
                log.GetComponent<ThrownSnowBehaviour>().ShowAsLog();
                break;
            case BearBehaviour.BearSide.Right:
                log = Instantiate(rightThrownSnow);
                log.GetComponent<ThrownSnowBehaviour>().ShowAsLog();
                break;
            case BearBehaviour.BearSide.Both:
                GameObject leftLog = Instantiate(leftThrownSnow);
                leftLog.GetComponent<ThrownSnowBehaviour>().ShowAsLog();
                GameObject rightLog = Instantiate(rightThrownSnow);
                rightLog.GetComponent<ThrownSnowBehaviour>().ShowAsLog();
                break;
            default:
                Debug.Log("Attempted to spawn thrown log on invalid side");
                break;
        }
    }
}
