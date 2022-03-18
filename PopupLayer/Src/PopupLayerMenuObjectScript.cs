using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupLayerMenuObjectScript : MonoBehaviour
{

	private GameObject _target;
	private int tick = 0;

	private RectTransform Canvas;

	public GameObject target
	{
		get
		{
			return _target;
		}
		set
		{
			_target = value;
			_target.transform.parent = this.transform;
			tick = 0;
		}
	}

	public Vector2 myPosition;

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

	void Update()
	{
		if (_target == null)
		{
			return;
		}

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



		var mySize = _target.GetComponent<RectTransform>().sizeDelta;

		Vector2 anchoredPosition = myPosition;
		//Debug.Log(myPosition);
		var rect = this.GetComponent<RectTransform>();

		/*anchoredPosition.x -= rect.sizeDelta.x / 2;

		// Calculate if we should go below, or above
		var mid = anchoredPosition.y + mySize.y / 2;
		if (mid < Canvas.rect.height / 2)
		{
			//anchoredPosition.y -= rect.sizeDelta.y;
			anchoredPosition.y += 10;
			anchoredPosition.y += mySize.y;
		}
		else
		{
			anchoredPosition.y -= rect.sizeDelta.y;
			anchoredPosition.y -= 10;
		}*/

		if (anchoredPosition.x + rect.rect.width > Canvas.rect.width)
		{
			anchoredPosition.x = Canvas.rect.width - rect.rect.width;
		}
		if (anchoredPosition.x < 0)
		{
			anchoredPosition.x = 0;
		}

		if (anchoredPosition.y + rect.rect.height > Canvas.rect.height)
		{
			anchoredPosition.y = Canvas.rect.height - rect.rect.height;
		}
		if (anchoredPosition.y < 0)
		{
			anchoredPosition.y = 0;
		}
		rect.anchoredPosition = anchoredPosition;
	}
}
