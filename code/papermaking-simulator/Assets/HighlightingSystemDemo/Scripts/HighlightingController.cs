using UnityEngine;
using System.Collections;

public class HighlightingController : MonoBehaviour
{
	protected HighlightableObject ho;
	
	void Awake()
	{
		ho = gameObject.AddComponent<HighlightableObject>();
	}
	
	void Update()
	{
		// Fade in/out constant highlighting with 'Tab' button
		if (Input.GetKeyDown(KeyCode.Tab)) 
		{
			ho.ConstantSwitch();
		}
		// Turn on/off constant highlighting with 'Q' button
		else if (Input.GetKeyDown(KeyCode.Q))
		{
			ho.ConstantSwitchImmediate();
		}
		
		// Turn off all highlighting modes with 'Z' button
		if (Input.GetKeyDown(KeyCode.Z)) 
		{
			ho.Off();
		}
		
		AfterUpdate();
	}

    void OnMouseEnter()
    {
        ho.ConstantSwitch();
        print("enter");
    }

    void OnMouseDown()
    {
        ho.ConstantSwitch();
        print("down");
    }

    protected virtual void AfterUpdate() {}
}