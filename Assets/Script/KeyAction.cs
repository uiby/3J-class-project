using UnityEngine;
using System.Collections;

public class KeyAction : MonoBehaviour {
	// A container for control target
	GameObject target;
	BlockController control;
	CameraController viewChenge;
	public GameObject[] blocks = new GameObject[1];
	
	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		/*
		if (!control) {
			// In future, this method is called by other obj.
			ConnectWithBlock();
			return;
		}
		*/

		// Pitch Block
		if (Input.GetKeyDown("w")) {
			control.PitchBlock(1);
		}
		else if (Input.GetKeyDown("s")) {
			control.PitchBlock(-1);
		}

		// Yaw Block
		else if (Input.GetKeyDown("e")) {
			control.YawBlock(1);
		}
		else if (Input.GetKeyDown("q")) {
			control.YawBlock(-1);
		}

		// Roll Block
		else if (Input.GetKeyDown("d")) {
			control.RollBlock(1);
		}
		else if (Input.GetKeyDown("a")) {
			control.RollBlock(-1);
		}

		// Move Block
		float x = 0.0f;
		float z = 0.0f;
		if (Input.GetKey("up")) {
			z = 0.1f;
			control.MoveBlock(x,z);
		}
		else if (Input.GetKey("down")) {
			z = -0.1f;
			control.MoveBlock(x,z);
		}
		else if (Input.GetKey("right")) {
			x = 0.1f;
			control.MoveBlock(x,z);
		}
		else if (Input.GetKey("left")) {
			x = -0.1f;
			control.MoveBlock(x,z);
		}

		// Drop Block
		if (Input.GetKeyDown("space")) {
			control.DropBlock();
		}

		// Rotate Camera
		if (Input.GetKey("return")){
			viewChenge.RotateCam(1);
		}
		else if (Input.GetKey("delete")){
			viewChenge.RotateCam(-1);
		}
	}

	public void ConnectWithBlock() {
		target = GameObject.Find("block(new)");
		if (!target) return;
		control = target.GetComponent<BlockController>();
	}

	public void DisconnectWithBlock() {
		target = GameObject.Find("_DummyBlock");
		if (!target) return;
		control = target.GetComponent<BlockController>();
	}
}
