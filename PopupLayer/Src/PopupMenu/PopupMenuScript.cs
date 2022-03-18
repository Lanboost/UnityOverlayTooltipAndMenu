using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenuScript : MonoBehaviour
{

	public PopupLayerScript popupLayerScript;
	
	public PopupMenu menu;

	public PopupMenu currentSubMenu;
	public GameObject subMenu;

	List<PopupMenuItemScript> items = new List<PopupMenuItemScript>();

	// Start is called before the first frame update
	void Start()
    {
        foreach(var m in menu.items)
		{
			GameObject item = null;
			if (m.menu == null)
			{
				item = Instantiate(popupLayerScript.MenuItem, this.transform);
			}
			else
			{
				item = Instantiate(popupLayerScript.MenuSubItem, this.transform);
			}
			var menuItem = item.AddComponent<PopupMenuItemScript>();
			menuItem.parent = this;
			menuItem.popupLayerScript = popupLayerScript;
			menuItem.item = m;
			items.Add(menuItem);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Collapse()
	{
		if (subMenu != null)
		{
			subMenu.GetComponentInChildren<PopupMenuScript>().Collapse();
			Destroy(subMenu);
			subMenu = null;
		}
		currentSubMenu = null;
	}

	public void OpenSubMenu(PopupMenu menu, Vector2[] edgePositions)
	{
		if(currentSubMenu == menu)
		{
			return;
		}

		Collapse();
		currentSubMenu = menu;


		subMenu = popupLayerScript.AddMenuPopup(currentSubMenu, edgePositions);
	}
}
