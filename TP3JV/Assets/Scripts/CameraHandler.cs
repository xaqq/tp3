using UnityEngine;
using System.Collections;

public class CameraHandler : MonoBehaviour {
	
	bool PanelOpen = false;
	public TweenRotation ArrowTween;
	public TweenPosition PanelTween;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void ToggleCameraPanel()
	{
		if (PanelOpen)
		{
			ArrowTween.Play(false);
			PanelTween.Play(false);
			PanelOpen = false;
		}
		else
		{
			ArrowTween.Play(true);
			PanelTween.Play(true);
			PanelOpen = true;
		}
	}
}
