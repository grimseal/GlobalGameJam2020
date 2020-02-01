using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{

    [SerializeField] private GameObject stormObject;
    [SerializeField] private GameObject fogObject;
    [SerializeField] private GameObject calmObject;
    [SerializeField] private GameObject packageObject;

    [Header("Weather settings"), SerializeField] private float calmTime = 30f;
    [SerializeField] private float weatherTime = 30f;
    [SerializeField] private ParrotController parrot;

    private IWeather packageWeather, stormWeather, calmWeather, fogWeather;
    private Coroutine weather;

    // Start is called before the first frame update
    void Start()
    {
        packageWeather = packageObject.GetComponent(typeof(IWeather)) as IWeather;
        stormWeather = stormObject.GetComponent(typeof(IWeather)) as IWeather;
        calmWeather = calmObject.GetComponent(typeof(IWeather)) as IWeather;
        fogWeather = fogObject.GetComponent(typeof(IWeather)) as IWeather;

        parrot = FindObjectOfType<ParrotController>();

        StartCoroutine(WeatherSequence());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WeatherSequence()
    {
        parrot.weatherState = ParrotController.WeatherState.Calm;
        yield return StartCoroutine(StartWeather(calmWeather, 0, weatherTime, false));
        
        parrot.weatherState = ParrotController.WeatherState.Package;
        yield return StartCoroutine(StartWeather(packageWeather, calmTime, weatherTime));
        
        parrot.weatherState = ParrotController.WeatherState.Fog;
        yield return StartCoroutine(StartWeather(fogWeather, calmTime, weatherTime));
        
        parrot.weatherState = ParrotController.WeatherState.Storm;
        yield return StartCoroutine(StartWeather(stormWeather, calmTime, weatherTime));
        
        parrot.weatherState = ParrotController.WeatherState.Package;
        yield return StartCoroutine(StartWeather(packageWeather, calmTime, weatherTime));
        
        Debug.Log("That's all");
    }

    private IEnumerator StartWeather(IWeather weather, float calmTime, float weatherTime, bool needParrot = true)
    {
        if(needParrot)
        {
            
            parrot.Alert();
        }
        yield return new WaitForSeconds(calmTime);
        weather.StartWeather();
        yield return new WaitForSeconds(weatherTime);
        weather.EndWeather();
    }
}
