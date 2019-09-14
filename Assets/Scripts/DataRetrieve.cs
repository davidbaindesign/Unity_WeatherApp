using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class DataRetrieve : MonoBehaviour
{
    // Start is called before the first frame update

    //If you fork this, please change this. Don't be a dick.
    private string OWMkey = "6180bd3f3dd535d87360adb5c9545cfd";
    private string url;


    void Start()
    {
        
    }

    //Call this in LocationRetrieve when latitude and longitude have been determined
    public void StartConnecting(float latitude, float longitude)
    {
        Debug.Log("Started connecting");
        url = "https://samples.openweathermap.org/data/2.5/weather?lat=" + latitude + "&lon=" + longitude + "&appid=" + OWMkey;

        //test
       // url = "http://intranet.orbiseducation.com/test_local/David_Playground/formjson.json";

        StartCoroutine(GetData());

    }

    //gets the json from our server for the questions and answer information
    IEnumerator GetData()
    {
        Debug.Log(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("error");
            Debug.Log(www.error);
        }
        else
        {
            //  questionInfo = QuestionInfo.CreateFromJSON(www.downloadHandler.text);
            // FormSetup(questionInfo);
            Debug.Log(www.downloadHandler.text);
        }
    }


}
