using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeatherData
{
    [SerializeField] public string city;
    [SerializeField] public string weatherDescription;

    [SerializeField] public float actualTemp;
    [SerializeField] public float windSpeed;
}
