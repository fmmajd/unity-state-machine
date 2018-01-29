using UnityEngine;
using System.Collections;

public class Nb_StateModel<M>: INBState where M:struct  {

	public delegate void enterEvent();
	public delegate void exitEvent();

	enterEvent enterCallback;
	exitEvent exitCallback;

	private string name;
	private M state;

	public Nb_StateModel(M state, enterEvent ent = null, exitEvent ex = null){
		enterCallback = ent;
		exitCallback = ex;
		this.state = state;
		name = state.ToString ();
	}

	public void setOnEnter(enterEvent func){
		enterCallback = func;
	}

	public void setOnExit(exitEvent func){
		exitCallback = func;
	}

	public M getState(){
		return state;
	}

	public string getName(){
		return name;
	}

	public void onEnter(){
		if(enterCallback != null)
			enterCallback ();
	}

	public void onExit(){
		if(exitCallback != null)
			exitCallback ();
	}


	//operation overloading
	public override bool Equals(System.Object obj){
		Nb_StateModel<M> s = obj as Nb_StateModel<M>;
		if ((object)s == null)
			return false;
		return base.Equals (obj) && Equals (obj as Nb_StateModel<M>);
	}

	public bool Equals(Nb_StateModel<M> otherState){
		if (!base.Equals (otherState))
			return false;
		if (name == otherState.getName ())
			return true;
		if (state.Equals (otherState.getState ()))
			return true;
		return false;
	}

	public static bool operator ==(Nb_StateModel<M> a, Nb_StateModel<M> b){
		if (System.Object.ReferenceEquals(a, b))
			return true;
		//		if ((Object)a == null || (Object)b == null)//removing object conversion results in infinite loop in == operation
		//			return false;
		if (a.getName () == b.getName ())
			return true;
		if (a.getState ().Equals (b.getState ()))
			return true;
		return false;
	}

	public static bool operator !=(Nb_StateModel<M> a, Nb_StateModel<M> b){
		return !(a == b);
	}

	public override string ToString ()
	{
		return "State: " + getName ();
	}

	public override int GetHashCode ()
	{
		return getName ().GetHashCode ();
	}

}


