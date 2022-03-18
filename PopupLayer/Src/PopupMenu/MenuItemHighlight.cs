using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuItemHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public Image image;
	public Color normal;
	public Color hover;
	public Color pressed;

	public int state = 0;

	public void OnPointerDown(PointerEventData eventData)
	{
		state = state | 2;
		SetColor();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		state = state | 1;
		SetColor();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		state = state & ~1;
		SetColor();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		state = state & ~2;
		SetColor();
	}

	public void SetColor()
	{
		if(state == 0)
		{
			image.color = normal;
		}
		else if ((state & 2) != 0)
		{
			image.color = pressed;
		}
		else
		{
			image.color = hover;
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		image = this.GetComponent<Image>();
		SetColor();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
