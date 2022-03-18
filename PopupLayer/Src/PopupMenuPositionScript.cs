using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMenuPositionScript : MonoBehaviour
{

	public Vector2[] edgePositions;

	private int tick = 0;
	private RectTransform Canvas;


	// Start is called before the first frame update
	void Start()
    {
		var rectTrans = this.GetComponent<RectTransform>();
		rectTrans.anchorMin = Vector2.zero;
		rectTrans.anchorMax = Vector2.zero;
		rectTrans.pivot = Vector2.zero;
		var contentSizeFitter = this.gameObject.AddComponent<ContentSizeFitter>();
		contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

		var layoutGroup = this.gameObject.AddComponent<VerticalLayoutGroup>();
		layoutGroup.childControlHeight = true;
		layoutGroup.childControlWidth = true;
		layoutGroup.childForceExpandHeight = true;
		layoutGroup.childForceExpandWidth = true;
		layoutGroup.childAlignment = TextAnchor.LowerLeft;

		Canvas = this.transform.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
	}

    // Update is called once per frame
    void Update()
    {
		if (tick == 0)
		{
			// Hide the tooltip really far away while we let unity calculate sizes etc
			this.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100000, -100000);
			tick = 1;
			return;
		}
		if (tick == 2)
		{
			return;
		}


		tick = 2;



		var mySize = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;

		Vector2 anchoredPosition = edgePositions[0];
		//Debug.Log(myPosition);
		var rect = this.GetComponent<RectTransform>();


		var positions = new Vector2[]
		{
			edgePositions[1],
			edgePositions[1]+new Vector2(0, rect.sizeDelta.y),
			edgePositions[0]+new Vector2(-rect.sizeDelta.x, 0),
			edgePositions[0]+new Vector2(-rect.sizeDelta.x, rect.sizeDelta.y)
		};

		var failed = false;
		foreach(var pos in positions)
		{
			failed = SetPositionFailOnOverflow(rect, pos);
			if(!failed)
			{
				break;
			}
		}
	}

	public bool SetPositionFailOnOverflow(RectTransform rect, Vector2 anchoredPosition)
	{
		var failed = false;
		if (anchoredPosition.x + rect.rect.width > Canvas.rect.width)
		{
			anchoredPosition.x = Canvas.rect.width - rect.rect.width;
			failed = true;
		}
		if (anchoredPosition.x < 0)
		{
			anchoredPosition.x = 0;
			failed = true;
		}

		if (anchoredPosition.y + rect.rect.height > Canvas.rect.height)
		{
			anchoredPosition.y = Canvas.rect.height - rect.rect.height;
			failed = true;
		}
		if (anchoredPosition.y < 0)
		{
			anchoredPosition.y = 0;
			failed = true;
		}
		rect.anchoredPosition = anchoredPosition;
		return failed;
	}
}
