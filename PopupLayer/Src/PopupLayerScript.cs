using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupLayerScript : MonoBehaviour
{
	protected PopupLayerObjectScript layerObject;
	protected PopupLayerMenuObjectScript layerMenuObject;

	public GameObject Menu;
	public GameObject MenuItem;
	public GameObject MenuSubItem;

	[NonSerialized]
	public PopupMenuScript currentMenu;

	// Start is called before the first frame update
	void Start()
    {
		var rectTrans = this.GetComponent<RectTransform>();
		rectTrans.anchorMin = new Vector2(0, 0);
		rectTrans.anchorMax = new Vector2(1, 1);
		rectTrans.pivot = new Vector2(0, 0);
		rectTrans.offsetMin = Vector2.zero;
		rectTrans.offsetMax = Vector2.zero;
		{
			var go = new GameObject();
			go.transform.parent = this.transform;
			go.name = "PopupObject";
			layerObject = go.AddComponent<PopupLayerObjectScript>();
			go.AddComponent<RectTransform>();
		}
		{
			var go = new GameObject();
			go.transform.parent = this.transform;
			go.name = "PopupMenuObject";
			layerMenuObject = go.AddComponent<PopupLayerMenuObjectScript>();
			go.AddComponent<RectTransform>();
		}



	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Set(GameObject prefab, Vector2 position)
	{
		Clean();


		layerObject.target = prefab;
		layerObject.myPosition = position;
	}

	public void SetMenu(PopupMenu menu, Vector2 position)
	{
		Clean();

		var go = Instantiate(Menu);
		var menuScript = go.AddComponent<PopupMenuScript>();
		menuScript.popupLayerScript = this;
		menuScript.menu = menu;

		layerMenuObject.target = menuScript.gameObject;
		layerMenuObject.myPosition = position;

		currentMenu = menuScript;
	}

	internal GameObject AddMenuPopup(PopupMenu menu, Vector2[] edgePositions)
	{

		var go = new GameObject();
		go.transform.parent = this.transform;
		go.name = "PopupMenuObject";
		var menuPositionScript = go.AddComponent<PopupMenuPositionScript>();
		go.AddComponent<RectTransform>();


		var goMenu = Instantiate(Menu);
		var menuScript = goMenu.AddComponent<PopupMenuScript>();
		menuPositionScript.edgePositions = edgePositions;
		menuScript.popupLayerScript = this;
		menuScript.menu = menu;

		goMenu.transform.SetParent(go.transform);



		return go;
	}

	public void CloseMenu()
	{
		Clean();
	}

	public void Clean()
	{
		if (layerObject.target != null)
		{
			Destroy(layerObject.target);
		}

		if (layerMenuObject.target != null)
		{
			Destroy(layerMenuObject.target);
		}

		if (currentMenu != null)
		{
			currentMenu.Collapse();
			currentMenu = null;
		}
	}
}
