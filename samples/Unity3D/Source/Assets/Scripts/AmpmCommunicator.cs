using UnityEngine;
using System.Collections;
using AmpmLib;

public class AmpmCommunicator : MonoBehaviour {

	//singleton 
	static AmpmCommunicator instance = null;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);

		}
		else
		{
			if (this != instance)
			{
				Destroy(gameObject);
			}
		}
	}


	// Use this for initialization
	void OnEnable () {
		AMPM.OnConfigLoaded += ParseConfig;
		AMPM.GetConfig ();
	}

	void ParseConfig(){ 
		// do stuff with the configuration
		StartHeartBeat();
	}

	void StartHeartBeat ()
	{
		StopAllCoroutines ();
		Debug.Log("Starting App heartbeat");
		StartCoroutine ("HeartNow");
	}

	private IEnumerator HeartNow(){
		while (true) {
			AMPM.Heart ();
			yield return new WaitForSeconds ((1/60));
		}
	}
}
