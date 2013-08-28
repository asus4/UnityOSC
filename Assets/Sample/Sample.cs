using UnityEngine;
using System.Collections;
using OSC;

public class Sample : MonoBehaviour {
	
	[SerializeField]
	int port = 7000;
	
	[SerializeField]
	string hostname = "localhost";
	
	OSCManager oscManager;

	void OnEnable() {
		oscManager = OSCManager.instance;
		oscManager.LocalPort = port;
		oscManager.OnMessage += OnOSCMessage;
	}
	
	void OnDisable() {
		oscManager.OnMessage -= OnOSCMessage;
	}

	void OnOSCMessage (OSCMessage message)
	{
		Debug.Log(message);
	}
	
	void OnGUI() {
		if(GUILayout.Button("single")) {
			oscManager.SendPacket(createMessage(), hostname, port);
		}
		if(GUILayout.Button("double")) {
			oscManager.SendPacket(createMessage(), hostname, port);
			oscManager.SendPacket(createMessage(), hostname, port);
		}
		if(GUILayout.Button("bundle")) {
			oscManager.SendPacket(createBundle(), "localhost", port);
		}
	}
	
	OSCMessage createMessage() {
		return OSCMessage.Create("/test", Time.frameCount, Random.Range(0,10), Random.value, "hoge");
	}
	
	OSCBundle createBundle() {
		OSCBundle bundle = new OSCBundle();
		bundle.Append(createMessage());
		bundle.Append(createMessage());
		bundle.Append(createMessage());
		return bundle;
	}	
}
