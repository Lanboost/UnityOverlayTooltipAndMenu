using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopupMenuItemScript : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
	public PopupLayerScript popupLayerScript;
	public PopupMenuItem item;
	public PopupMenuScript parent;

	public bool isHover = false;
	public float timer = 0;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isHover)
		{
			timer += Time.deltaTime;
			if(timer >= 0.5)
			{
				if (item.menu == null)
				{
					parent.Collapse();
					isHover = false;
				}
				else
				{
					OpenSubMenu();
					isHover = false;
				}
			}
		}
    }

	public static Vector2[] RectTransformToScreenSpaceEdges(RectTransform transform)
	{
		Vector2 size = transform.rect.size;
		Vector2 pos = (Vector2)transform.position / transform.lossyScale;
		Vector2 posi = pos - (size * transform.pivot);
		posi.y += size.y / 2;
		return new Vector2[] { posi, posi+new Vector2(size.x, 0) };
	}

	protected void OpenSubMenu()
	{
		var edgePositions = RectTransformToScreenSpaceEdges(this.GetComponent<RectTransform>());

		parent.OpenSubMenu(item.menu, edgePositions);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isHover = false;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(item.menu == null)
		{
			popupLayerScript.CloseMenu();
			item.Action();
		}
		else
		{
			OpenSubMenu();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isHover = true;
		timer = 0;
	}
}
