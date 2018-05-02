using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

	// This code is to ensure that the person using the client only uses their car.
	void Start () {
        if (isLocalPlayer)
            GetComponent<Drive>().enabled = true;
	}
}
