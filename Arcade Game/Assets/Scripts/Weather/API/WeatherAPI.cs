using System.Collections;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;

public class WeatherAPI : MonoBehaviour
{
    //Arreglo de structs que organizan la informacion de un pais.
    [SerializeField] private WeatherCountry[] countries = new WeatherCountry[10];

    //String no modificable que contiene la llave API.
    private static readonly string apiKey = "dd355587e331db0873d6e0b86b684739";

    //Clase que realiza el cambio de clima.
    private WeatherChangeEnvironment weatherChange;

    //Int que se usa para guardar los datos de un pais del arreglo de paises.
    private int currentRandomIndex;

    //String donde se convierte la informacion Json.
    private string json;

    [SerializeField] private TextMeshProUGUI countryDisplayText;

    private void Start()
    {
        weatherChange = gameObject.GetComponent<WeatherChangeEnvironment>();
        
        StartCoroutine(RetrieveWeatherData());
    }

    private string UpdatedURL()
    {
        currentRandomIndex = GetRandomCountry();

        string url = $"https://api.openweathermap.org/data/2.5/weather?lat={countries[currentRandomIndex].countryLatitude}&lon={countries[currentRandomIndex].countryLongitude}&appid={apiKey}&units=metric";

        return url;
    }

    IEnumerator RetrieveWeatherData()
    {
        yield return new WaitForSecondsRealtime(10);

        ClearCountriesData();

        UnityWebRequest request = new UnityWebRequest(UpdatedURL());
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);

            json = request.downloadHandler.text;

            DecodeJson();

            weatherChange.SetEnvironmentValues(countries[currentRandomIndex].weatherData.actualTemp);
            UpdateScreenDisplay(countries[currentRandomIndex].country);
        }

        if (!GameManager.Instance.GameOver)
        {
            StartCoroutine(RetrieveWeatherData()); 
        }
    }

    private void DecodeJson()
    {
        var weatherJson = JSON.Parse(json);

        countries[currentRandomIndex].weatherData.city = weatherJson["name"].Value;
        countries[currentRandomIndex].weatherData.actualTemp = float.Parse(weatherJson["main"]["temp"].Value);
        countries[currentRandomIndex].weatherData.weatherDescription = weatherJson["weather"][0]["description"].Value;
        countries[currentRandomIndex].weatherData.windSpeed = float.Parse(weatherJson["wind"]["speed"].Value);
        countries[currentRandomIndex].weatherIsDisplayed = true;
    }

    private void ClearCountriesData()
    {
        for (int i = 0; i < countries.Length;i++)
        {
            countries[i].weatherData.city = string.Empty;
            countries[i].weatherData.weatherDescription = string.Empty;
            countries[i].weatherData.actualTemp = 0f;
            countries[i].weatherData.windSpeed = 0f;
            countries[i].weatherIsDisplayed = false;
        }
    }

    private int GetRandomCountry()
    {
        int randomCountryNumber = Random.Range(0, countries.Length);
        return randomCountryNumber;
    }

    private void UpdateScreenDisplay(string country)
    {
        countryDisplayText.text = $"Escenario de {country} iniciado.";
    }
}
