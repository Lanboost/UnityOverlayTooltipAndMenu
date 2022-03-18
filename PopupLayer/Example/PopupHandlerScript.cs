using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupHandlerScript : MonoBehaviour
{
	public PopupLayerScript popupLayer;

	public Text Label;
	public Button Button;
	public GameObject popupMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OpenMenu(string[] values, Vector2 position)
	{
		/*var go = Instantiate(popupMenu);
		foreach(var val in values)
		{

			var goo = Instantiate(Button);
			goo.GetComponentInChildren<Text>().text = val;
			goo.transform.parent = go.transform;
		}

		popupLayer.Set(go, position);*/

		var builder = PopupMenu.Builder();
		builder.AddItem("Test 1", delegate () { });
		builder.AddItem("Test 2", delegate () { });
		builder.AddSubMenu("Sub Menu 1", PopupMenu.Builder().AddItem("Sub Item 1", delegate () { }).AddItem("Sub Item 2", delegate () { }).Build());
		builder.AddItem("Test 3", delegate () { });
		builder.AddItem("Test 4", delegate () { });


		popupLayer.SetMenu(builder.Build(), position);
	}
}
