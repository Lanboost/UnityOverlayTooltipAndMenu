using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTipOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public GameObject prefab;
	public string tooltipText = "";

	float time = 0;
	bool isHover = false;
	bool tooltipShowing = false;
	PopupLayerScript popupLayerScript;

    // Start is called before the first frame update
    void Start()
    {
		popupLayerScript = FindObjectOfType<PopupLayerScript>();

	}

    // Update is called once per frame
    void Update()
    {
		if(tooltipShowing && !isHover)
		{
			time += Time.deltaTime;
			if (time > 0.3)
			{
				HideTooltip();
				tooltipShowing = false;
			}
		}


        if(!isHover)
		{
			return;
		}

		if (!tooltipShowing)
		{
			time += Time.deltaTime;
			if (time > 0.5)
			{
				ShowTooltip();
				tooltipShowing = true;
			}
		}
	}

	public static Vector2 RectTransformToScreenSpace(RectTransform transform)
	{
		Vector2 size = transform.rect.size;
		Vector2 pos = (Vector2)transform.position / transform.lossyScale;
		Vector2 posi = pos - (size * transform.pivot);
		posi.x += size.x / 2;
		return posi;
	}

	public void ShowTooltip()
	{
		var go = Instantiate(prefab);
		go.GetComponentInChildren<Text>().text = tooltipText;
		popupLayerScript.Set(go, RectTransformToScreenSpace(this.GetComponent<RectTransform>()));
	}

	public void HideTooltip()
	{
		popupLayerScript.Clean();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isHover = true;
		time = 0;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isHover = false;
		time = 0;
	}
}
