using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
[Serializable]
public class Ima
{
    public string content;
}


[Serializable]
public class Feature
{
    public string type;
}


[Serializable]
public class Attributes
{
    public string stat;
}


[Serializable]
public class RootObject
{
    public string nombre_personaje;
    public string nombre_usuario; 
}

[Serializable]
public class request
{
    public string Timeout;
    public string Method;


}


