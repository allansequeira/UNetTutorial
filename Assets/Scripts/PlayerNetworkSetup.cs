using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

	/// <summary>
	/// The FPS character camera.
	/// </summary>
	[SerializeField]Camera FPSCharacterCamera;

	/// <summary>
	/// The FPS character audio listener.
	/// </summary>
	[SerializeField]AudioListener FPSAudioListener;

	// Use this for initialization
	void Start () {

		if (isLocalPlayer) {

			// deactivate the Scene camera
			GameObject.Find("Scene Camera").SetActive(false);

			// enable the Character Controller component and first person controller component script (which handles the movement of the FPS)
			GetComponent<CharacterController>().enabled = true;
			GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;

			// enable the FPS camera and audiolistener
			FPSCharacterCamera.enabled = true;
			FPSAudioListener.enabled = true;

		}
	
	}

}
