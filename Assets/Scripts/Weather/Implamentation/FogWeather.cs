using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWeather : MonoBehaviour, IWeather
{

    [SerializeField] private ChildController wheelChild;
    [SerializeField] private ChildController rotorChild;

    private ShipController shipController;

    public void EndWeather()
    {
        AudioManager.Instance.FogSoundPlay(false);
        Debug.Log(this.name + " is end");
        if ((wheelChild.ProblemPregress >= 30 && wheelChild.ProblemPregress <= 60)
            && rotorChild.ProblemPregress >= 0 && rotorChild.ProblemPregress <= 30)
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
        AudioManager.Instance.FogSoundPlay(true);
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
