using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class ParrotController : MonoBehaviour
{

    private bool hasPlayer = false;
    private Coroutine parrotAlert;

    public enum WeatherState
    {
        Storm,
        Fog,
        Calm,
        Package
    }
    public WeatherState weatherState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasPlayer && Input.GetKeyDown(KeyCode.E))
        {
            ShowWeather();
        }
    }

    public void Alert()
    {
        Debug.Log("Parrot anim");
        //anim alert
    }

    public void ShowWeather()
    {
        switch(weatherState)
        {
            case WeatherState.Calm: Debug.Log("Parrot says: Calm"); break;
            case WeatherState.Storm: Debug.Log("Parrot says: Storm"); break;
            case WeatherState.Fog: Debug.Log("Parrot says: Fog"); break;
            case WeatherState.Package: Debug.Log("Parrot says: Package"); break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hasPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hasPlayer = false;
        }
    }
}
