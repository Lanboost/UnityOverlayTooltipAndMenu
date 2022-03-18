using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenu
{
	public List<PopupMenuItem> items = new List<PopupMenuItem>();

	public PopupMenu(List<PopupMenuItem> items)
	{
		this.items = items;
	}

	public static PopupMenuBuilder Builder()
	{
		return new PopupMenuBuilder();
	}

}

public class PopupMenuBuilder
{
	public List<PopupMenuItem> items = new List<PopupMenuItem>();
	public PopupMenuBuilder AddItem(string text, Action action)
	{
		items.Add(new PopupMenuItem(text, action));
		return this;
	}

	public PopupMenuBuilder AddSubMenu(string text, PopupMenu menu)
	{
		items.Add(new PopupMenuItem(text, menu));
		return this;
	}

	public PopupMenu Build()
	{
		return new PopupMenu(items);
	}
}

public class PopupMenuItem
{
	public PopupMenu menu;
	public string Text;
	public Action Action;

	public PopupMenuItem(string text, Action action)
	{
		Text = text;
		Action = action;
	}

	public PopupMenuItem(string text, PopupMenu menu)
	{
		Text = text;
		this.menu = menu;
	}
}
