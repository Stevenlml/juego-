using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour {


    [SerializeField]
    public InputField usuarioField = null;

     [SerializeField]
     public InputField claveField = null;

     [SerializeField]
     private Text feedback = null; 

     [SerializeField]
     private Toggle rememberData = null; 


     // Use this for initialization
     void Start () {

         if (PlayerPrefs.HasKey("remember") && PlayerPrefs.GetInt("remember") == 1) {
             usuarioField.text = PlayerPrefs.GetString("rememberLogin");
             claveField.text = PlayerPrefs.GetString("rememberPass");    
         }

     }

     public void mandar()
     {
         WSGame.instance.StartWebService(usuarioField.text, resultados, Fallo);

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

         RootObject rs = JsonUtility.FromJson<RootObject>(response);
         if (rs.nombre_personaje == "Cualquier cosa")
         {

             feedback.CrossFadeAlpha(100f, 0f, false);
             feedback.color = Color.green;
             feedback.text = "Se ha logeado correctamente\nCargando juego....";
         }

         else {
             feedback.CrossFadeAlpha(100f, 0f, false);
             feedback.color = Color.red;
             feedback.text = "No se ha logeado correctamente\nIntente de nuevo";
         }

     }
  


}

    



