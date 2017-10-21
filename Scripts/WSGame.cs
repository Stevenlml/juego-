using System;
using System.Collections;
using System.Text;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;


public class WSGame : MonoBehaviour {


    public delegate void WSResponse(string response);
    public static WSGame instance;
    private string WS_URL = "http://localhost:8080/ServiceWeb/rest/usuario/login?correo=correo1&clave=123";


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            GameObject.Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


   public void StartWebService(string endpoint, WSResponse successCallback, WSResponse errorCallback = null)
    {
        StartCoroutine(GetText(endpoint, successCallback, errorCallback));
    }
    


    IEnumerator GetText(string comentario, WSResponse successCallback = null, WSResponse errorCallback = null)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(WS_URL + WWW.EscapeURL(comentario)))
        {
#if UNITY_5_5_4 || UNITY_5_6_OR_NEWER
            www.timeout = 20;
#endif
            UTF8Encoding encoder = new UTF8Encoding();
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(encoder.GetBytes(comentario));
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");


            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                if (errorCallback != null) errorCallback(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text.Trim());
                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
                Debug.Log(results);

                if (successCallback != null) successCallback(www.downloadHandler.text);
            }
        }
    }












    /*  private IEnumerator ConsumeWebService(string json, WSResponse successCallback = null,WSResponse errorCallback = null)
      {
          string url = Go_URL ;

          Debug.Log("Sending JSON: " + json);
          Debug.Log("To URL: " + url);

          using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
          {
  #if UNITY_5_5_4 || UNITY_5_6_OR_NEWER
              www.timeout = 20;
  #endif
              UTF8Encoding encoder = new UTF8Encoding();
              www.uploadHandler = (UploadHandler)new UploadHandlerRaw(encoder.GetBytes(json));
              www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
              www.SetRequestHeader("Content-Type", "application/json");

              yield return www.Send();

              if (www.isNetworkError)
              {
                  Debug.Log(www.error);
                  if (errorCallback != null) errorCallback(www.error);

              }
              else
              {
                  string responseString = www.downloadHandler.text.Trim();
                  Debug.Log("responseString " + responseString);

                  if (www.downloadHandler.text == null)
                  {
                      if (errorCallback != null) errorCallback("www.downloadHandler.text is " + www.downloadHandler.text);

                  }
                  else
                  {
                      if (successCallback != null) successCallback(www.downloadHandler.text.Trim());


                  }
              }
          }
      }*/

}
