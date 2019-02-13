using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    private string itemname;
    private Sprite itemicon;
    private string itemtype;

    //konstruktor

    public Item(string itemname, Sprite itemicon, string itemtype)
    {
        this.itemname = itemname;
        this.itemicon = itemicon;
        this.itemtype = itemtype;
    }


    // gettery

    public string GetItemName()
    {
        return itemname;
    }
    public Sprite GetItemIcon()
    {
        return itemicon;
    }
    public string GetItemType()
    {
        return itemtype;
    }
    public void SetItemName(string name)
    {
        itemname = name;
    }
    public void SetItemIcon(Sprite icon)
    {
        itemicon = icon;
    }
    public void SetItemType(string type)
    {
        itemtype = type;
    }
	public Item Items { get; set; }
    
}