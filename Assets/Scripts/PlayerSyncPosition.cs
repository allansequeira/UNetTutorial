using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncScript : NetworkBehaviour {

	[SyncVar]
	private Vector3 syncPosition;
	[SerializeField]Transform myTransform;
	[SerializeField]float lerpRate = 15;

	// Update is called once per frame
	void FixedUpdate () {
	
	}


	/// <summary>
	/// Lerps the position of the player characters that don't belong to us.
	/// In out game instance, if there are other players, we want their positions to lerp or their motion to smooth out because position
	/// data is going to be received in increments and then players are moved to those new positions that are coming
	/// i.e. smooth out the movement of the players. This won't run on our game instance.
	/// </summary>
	void LerpPosition() {
		if (!isLocalPlayer) {
			myTransform.position = Vector3.Lerp(myTransform.position, syncPosition, Time.deltaTime * lerpRate);
		}
	}

	// Since this is server authoritative system, we need to tell the server about this player's position and 
	// then we want the server to tell all the clients (game instances) where this player's new position is and then
	// those clients wiil use the LerpPosition function to move those player characters to their positions.
	// To do this we write a command that is sent to the server i.e. the client is telling the server to run this command
	// The command must have "Cmd" as the prefix. The client will provide the parameter.
	// This code will only run on the server, but will be called by our game instance client
	[Command]
	void CmdProvidePositionToServer(Vector3 pos) {
		syncPosition = pos;
	}
}
