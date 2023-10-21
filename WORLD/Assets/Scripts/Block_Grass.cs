using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Grass : Block
{

    public Texture2D[] Texture2D;
    private new void Awake()
    {
        base.Awake();
    }
    public void Start()
    {
        base.Start();
    }
    public override void InitUnit()
    {
        base.InitUnit();
    }
    private IEnumerator MakeGrass()
    {
        yield return new WaitForSeconds(5);
        if ((int)Posion.y == 2 * MainGame.World_y - 1 || MainGame.unit[(int)Posion.x, (int)Posion.y + 1, (int)Posion.z].unitType == UnitType.Null)
        {
            this.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = Texture2D[0];
            this.transform.GetChild(1).GetComponent<MeshRenderer>().material.mainTexture = Texture2D[1];
            this.transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = Texture2D[1];
            this.transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = Texture2D[1];
            this.transform.GetChild(4).GetComponent<MeshRenderer>().material.mainTexture = Texture2D[1];
        }
    }
    private void OnEnable()
    {
        StartCoroutine(MakeGrass());
    }
    public override void SetOneActive(string s, bool b)
    {
        base.SetOneActive(s, b);
        StartCoroutine(MakeGrass());
    }
}
