using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Null, Block
}
public class UnitSpace : MonoBehaviour
{
    public UnitType unitType;
    private Vector3 posion;
    public Vector3 Posion
    {
        get => posion; set
        {
            posion = value;
            gameObject.name = value.ToString("F0");
            gameObject.transform.position = posion;
        }
    }
    public virtual void InitUnit()
    {
        unitType = UnitType.Null;
    }
    public void Awake()
    {
        InitUnit();
    }
    void Update()
    {

    }
}
