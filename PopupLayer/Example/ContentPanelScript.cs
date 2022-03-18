using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContentPanelScript : MonoBehaviour, IPointerClickHandler
{
	public PopupLayerScript popupLayer;


	public void OnPointerClick(PointerEventData eventData)
	{

		if (eventData.button == PointerEventData.InputButton.Right)
		{
			Debug.Log("Open Menu");


			var builder = PopupMenu.Builder();
			builder.AddItem("Test 1", delegate () { Debug.Log("Clicked 1"); });
			builder.AddItem("Test 2", delegate () { Debug.Log("Clicked 2");  });
			builder.AddSubMenu("Sub Menu 1", 
				PopupMenu.Builder().
				AddItem("Sub Item 1", delegate () { Debug.Log("Clicked sub 1");  }).
				AddItem("Sub Item 2", delegate () { Debug.Log("Clicked sub 2");  }).
				Build()
			);
			builder.AddItem("Test 3", delegate () { Debug.Log("Clicked 3"); });
			builder.AddItem("Test 4", delegate () { Debug.Log("Clicked 4"); });


			popupLayer.SetMenu(builder.Build(), eventData.position);
			
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
