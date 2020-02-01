using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelChildAction : MonoBehaviour, IChildAction
{

    private ChildController childController;

    public void ChangeProblemValue(int value)
    {
        if (childController == null)
        {
            Debug.LogError("There is no childController, please insert it");
            return;
        }
        childController.AddValueToProblem(value);
    }

    // Start is called before the first frame update
    void Start()
    {
        childController = GetComponent<ChildController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
