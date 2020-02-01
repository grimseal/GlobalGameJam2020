using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormWeather : MonoBehaviour, IWeather
{

    [SerializeField] private ChildController wheelChild;
    [SerializeField] private ChildController rotorChild;

    private ShipController shipController;

    public void EndWeather()
    {
        Debug.Log(this.name + " is end");
        if ((wheelChild.ProblemPregress >= 0 && wheelChild.ProblemPregress <= 30)
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
