using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPointController : MonoBehaviour
{
    [SerializeField]
    private ShipController shipController;

    public void Trigger()
    {
        shipController.AddHPPoint();
    }
}
