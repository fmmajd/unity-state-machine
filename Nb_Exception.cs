using UnityEngine;
using System.Collections;
using System;

//can later be used to log, instead of throwing exception and pausing the game
public class Nb_Exception : Exception {

	public delegate void nbexception (Exception e, string msg);
	public static event nbexception NBExceptionThrown;

	public Nb_Exception (string msg) : base ("Nimboos Exception: " + msg){
		if (NBExceptionThrown != null)
			NBExceptionThrown (this, msg);
	}

	public Nb_Exception(string msg, GameObject obj) : base ("Nimboos Exception in Object " + obj.name + ": " + msg){
		if (NBExceptionThrown != null)
			NBExceptionThrown (this, msg);
	}

	public Nb_Exception(string msg, GameObject obj, string scriptName) : base ("Nimboos Exception in Object " + obj.name + " in Script " + scriptName + ": " + msg){
		if (NBExceptionThrown != null)
			NBExceptionThrown (this, msg);
	}

	public Nb_Exception(string msg, string objName) : base ("Nimboos Exception in Object " + objName + ": " + msg){
		if (NBExceptionThrown != null)
			NBExceptionThrown (this, msg);
	}

	public Nb_Exception(string msg, string objName, string scriptName) : base ("Nimboos Exception in Object " + objName + " in Script " + scriptName + ": " + msg){
		if (NBExceptionThrown != null)
			NBExceptionThrown (this, msg);
	}

}
