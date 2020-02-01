using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeather
{
    /// <summary>
    /// Start all effects, parrot screams
    /// </summary>
    void StartWeather();
   
    /// <summary>
    /// End effects and deal damage
    /// </summary>
    void EndWeather();
}
