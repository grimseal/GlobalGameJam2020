using System.Collections;
using System.Collections.Generic;
using InteractiveObject;
using UnityEngine;

public class PackageWeather : MonoBehaviour, IWeather
{

    [SerializeField] private ChildController wheelChild;
    [SerializeField] private ChildController rotorChild;

    private ShipController shipController;

    public void EndWeather()
    {
        GrapplingHookController.Instance.CanGrab = false;
        Debug.Log(this.name + " is end");
        if ((wheelChild.ProblemPregress >= 30 && wheelChild.ProblemPregress <= 60)
            && rotorChild.ProblemPregress >= 30 && rotorChild.ProblemPregress <= 60)
        {
            Debug.Log("All is good");
        }
        else
        {
            Debug.Log("Ship take a 1 damage");
            shipController.SubHPPoint();
        }
    }

    public void StartWeather()
    {
        Debug.Log(this.name + " is start");
        GrapplingHookController.Instance.CanGrab = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        shipController = FindObjectOfType<ShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
