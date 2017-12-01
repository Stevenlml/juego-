using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class WSGame : MonoBehaviour
{

    [SerializeField]
    public InputField usuarioField = null;

    [SerializeField]
    public InputField claveField = null;

    [SerializeField]
    private Text feedback = null;

    [SerializeField]
    private Toggle rememberData = null;

    public delegate void WSResponse(string response);
    public static WSGame instance;

    void Start()
    {

        if (PlayerPrefs.HasKey("remember") && PlayerPrefs.GetInt("remember") == 1)
        {
            usuarioField.text = PlayerPrefs.GetString("rememberLogin");
            claveField.text = PlayerPrefs.GetString("rememberPass");
        }

    }

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




    public void StartWebService(WSResponse successCallback)
    {
        StartCoroutine(GetText(successCallback));
    }



    public IEnumerator GetText(WSResponse successCallback = null)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(

       "localhost:8080/ServiceWeb/rest/usuario/login?correo=" + usuarioField.text + "&clave=" + claveField.text

        );
        yield return webRequest.Send();

        if (!webRequest.isNetworkError)
        {

            RootObject rs = JsonUtility.FromJson<RootObject>(

                webRequest.downloadHandler.text

            );


            Debug.Log(rs.nombre_personaje);
            Debug.Log(rs.nombre_usuario);
            if (successCallback != null) successCallback(webRequest.downloadHandler.text);

        }
        else
        {
            Debug.Log(webRequest.error);
        }
    }

    public void mandar()
    {
        WSGame.instance.StartWebService(resultados);

        string usuario = usuarioField.text;

        string clave = claveField.text;

       

        if (rememberData.isOn)
        {
            PlayerPrefs.SetInt("remember", 1);
            PlayerPrefs.SetString("rememberLogin", usuario);
            PlayerPrefs.SetString("rememberPass", clave);
        }

    }

    private void Fallo(string response)
    {
        feedback.CrossFadeAlpha(100f, 0f, false);
        feedback.color = Color.red;
        feedback.text = "Ha ocurrido un error en la conexion\nIntente de nuevo";
    }

    public void resultados(string response)
    {

        
        Debug.Log(response);
        if (response != "{}")
        {

            feedback.CrossFadeAlpha(100f, 0f, false);
            feedback.color = Color.green;
            feedback.text = "Se ha logeado correctamente\nCargando juego....";
            //StartCoroutine(CargarEscena());
        }

        else
        {
            feedback.CrossFadeAlpha(100f, 0f, false);
            feedback.color = Color.red;
            feedback.text = "No se ha logeado correctamente\nIntente de nuevo";
        }



    }

   /* IEnumerator CargarEscena()
    {
        yield return new WaitForSeconds(5);
        Application.LoadLevel("");
        
    }*/ 

}
