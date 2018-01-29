using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Nb_StateMachine<T> where T:struct {

	private Nb_StateModel<T> curState;
	private List<Nb_StateModel<T>> allStates;
	private Nb_Tuple<Nb_StateModel<T>, Nb_StateModel<T>> transitions;
	private Nb_Tuple<Nb_StateModel<T>, Nb_StateModel<T>> defaultTransitions;

	public Nb_StateMachine(Nb_StateModel<T> curState){
		this.curState = curState;
		transitions = new Nb_Tuple<Nb_StateModel<T>, Nb_StateModel<T>> ();
		allStates = new List<Nb_StateModel<T>> ();
		foreach(T state in Enum.GetValues (typeof(T))){
			Nb_StateModel<T> tmp = new Nb_StateModel<T> (state);
			allStates.Add (tmp);
		}
	}

	public Nb_StateMachine(T t){
		allStates = new List<Nb_StateModel<T>> ();
		foreach(T state in Enum.GetValues (typeof(T))){
			Nb_StateModel<T> tmp = new Nb_StateModel<T> (state);
			allStates.Add (tmp);
		}
		curState = getState (t);
		transitions = new Nb_Tuple<Nb_StateModel<T>, Nb_StateModel<T>> ();
		defaultTransitions = new Nb_Tuple<Nb_StateModel<T>, Nb_StateModel<T>> ();
	}

	public void addTransition(Nb_StateModel<T> state1, Nb_StateModel<T> state2, bool isDefault = false){
		addTransition (state1.getState (), state2.getState (), isDefault);
	}

	public void addTransition(T t1, T t2, bool isDefault = false){
		transitions.Add (getState (t1), getState (t2));
		if(isDefault){
			if (defaultTransitions.ContainsFirst (getState (t1)))
				throw new Nb_Exception ("This state has already a default transition: " + defaultTransitions.FindAllSeconds (getState (t1))[0]);
			defaultTransitions.Add (getState (t1), getState (t2));
		}
	}

	public void removeTransition(T t1, T t2){
		transitions.RemoveIfExists (getState (t1), getState (t2));
		if (defaultTransitions.Contains (getState (t1), getState (t2)))
			defaultTransitions.RemoveIfExists (getState (t1), getState (t2));
		else
			throw new Nb_Exception ("The transition was not found.");
	}

	public void changeState(T nextT){
		if(!isTransitionAllowed (curState.getState (), nextT)){
			throw new Nb_Exception ("State transition from " + curState.ToString () + " to " + nextT.ToString () + " is not allowed.");
		}
		curState.onExit ();
		curState = getState (nextT);
		getState (nextT).onEnter ();
	}

	public void changeState(){//change to next default state
		if (!defaultTransitions.ContainsFirst (curState))
			throw new Nb_Exception ("There is no default transition for state: " + curState.getName ());
		changeState (defaultTransitions.FindAllSeconds (curState)[0].getSecond ());
	}

	public void changeState(Nb_StateModel<T> nextState){
		if (!isTransitionAllowed (curState, nextState)) {
			throw new Nb_Exception ("State transition from " + curState.ToString () + 
				" to " + nextState.ToString () + " is not allowed.");
		}
		curState.onExit ();
		curState = nextState;
		curState.onEnter ();
	}

	public T getState(){
		return curState.getState();
	}

	public Nb_StateModel<T> getState(T t){//for when adding enter and exit listeners
		foreach(Nb_StateModel<T> model in allStates){
			if (model.getState ().Equals (t))
				return model;
		}
		throw new Nb_Exception ("State not defined.");
	}

	private bool isTransitionAllowed(Nb_StateModel<T> state1, Nb_StateModel<T> state2){
		if (transitions.Contains (state1, state2))
			return true;
		return false;
	}

	private bool isTransitionAllowed(T t1, T t2){
		if (transitions.Contains (getState (t1), getState (t2)))
			return true;
		return false;
	}

//	private Nb_StateModel<T> getStateItem(T t){
//		foreach(Nb_StateModel<T> model in allStates){
//			if (model.getState ().Equals (t))
//				return model;
//		}
//		throw new Exception ("NB: State not defined.");
//	}
		
}
