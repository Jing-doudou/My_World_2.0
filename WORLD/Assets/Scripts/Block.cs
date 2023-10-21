using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//岩石，泥土，泥土草，青铜，铁，砖石，水
public class Block : UnitSpace
{
    private int hp = 1;
    public Block_Type blockType;
    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                hp = 0;
                DesEvent();
            }
        }
    }
    public override void InitUnit()
    {
        unitType = UnitType.Block;
        foreach (Transform item in this.transform)
        {
            item.gameObject.SetActive(false);
        }
    }
    public new void Awake()
    {
        base.Awake();
    }
    public void Start()
    {
        OnCreateEvent();

    }
    public void OnCreateEvent()
    {
        int x, y, z;
        foreach (Transform item in transform)
        {
            x = (int)(Posion + MainGame.Direction[item.name].v3).x;
            y = (int)(Posion + MainGame.Direction[item.name].v3).y;
            z = (int)(Posion + MainGame.Direction[item.name].v3).z;
            if (MainGame.unit[x, y, z].unitType == UnitType.Block)
            {
                Block b = MainGame.unit[x, y, z] as Block;
                b.SetOneActive(MainGame.Direction[item.name].Other, false);
            }
            else
            {
                item.gameObject.SetActive(true);
            }
        }
    }

    public virtual void ChackAround()
    {
        int x, y, z;
        foreach (Transform item in transform)
        {
            x = (int)(Posion + MainGame.Direction[item.name].v3).x;
            y = (int)(Posion + MainGame.Direction[item.name].v3).y;
            z = (int)(Posion + MainGame.Direction[item.name].v3).z;
            if (MainGame.unit[x, y, z].unitType != UnitType.Block)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
    /// <summary>
    /// Block Destory event 
    /// </summary>
    public void DesEvent()
    {
        int x, y, z;
        foreach (Transform item in transform)
        {
            x = (int)(Posion + MainGame.Direction[item.name].v3).x;
            y = (int)(Posion + MainGame.Direction[item.name].v3).y;
            z = (int)(Posion + MainGame.Direction[item.name].v3).z;
            if (MainGame.unit[x, y, z].unitType == UnitType.Block)
            {
                Block b = MainGame.unit[x, y, z] as Block;
                b.SetOneActive(MainGame.Direction[item.name].Other, true);
            }
        }
        MainGame.mainGame.CreateBlock((int)Posion.x, (int)Posion.y, (int)Posion.z, 0);
    }
    public virtual void SetOneActive(string s, bool b)
    {
        transform.Find(s).gameObject.SetActive(b);
    }
}
