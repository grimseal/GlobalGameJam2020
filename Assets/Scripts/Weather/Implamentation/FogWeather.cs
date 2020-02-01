using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWeather : MonoBehaviour, IWeather
{

    [SerializeField] private ChildController wheelChild;
    [SerializeField] private ChildController rotorChild;

    public void EndWeather()
    {
        if ((wheelChild.ProblemPregress >= 30 && wheelChild.ProblemPregress <= 60)
            && rotorChild.ProblemPregress >= 0 && rotorChild.ProblemPregress <= 30)
        {
            Debug.Log("All is good");
        }
        else
        {
            Debug.Log("Ship take a 1 damage");
        }
        Debug.Log(this.name + " is end");
    }

    public void StartWeather()
    {
        Debug.Log(this.name + " is start");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
