using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;

//a class of key, value
//or just two elements set
//in this class, direction is important,
//so <x, y> != <y, x>
//but can't have two similar <x, y> and <x, y>

public sealed class Nb_Tuple<T1, T2> {

	private List<Nb_TupleItem<T1, T2>> items;

	public Nb_Tuple(){
		items = new List<Nb_TupleItem<T1, T2>> ();
	}

	public void Add(T1 t1, T2 t2){
		if (Contains (t1, t2))
			throw new Exception ("Tuple already contains these values.");
		items.Add (new Nb_TupleItem<T1, T2>(t1, t2));
	}

	public void RemoveIfExists(T1 t1, T2 t2){
		for(int i=0; i<items.Count; i++){
			if(items[i].getFirst ().Equals (t1) && items[i].getSecond ().Equals (t2)){
				items.RemoveAt (i);
				break;
			}
		}
	}

	public void Clear(){
		items.Clear ();
	}

	public bool Contains(T1 t1, T2 t2){
		foreach (Nb_TupleItem<T1, T2> item in items)
			if (item.getFirst ().Equals (t1) && item.getSecond ().Equals (t2))
				return true;
		return false;
	}

	public bool ContainsFirst(T1 t1){
		foreach(Nb_TupleItem<T1, T2> item in items){
			if (item.getFirst ().Equals (t1))
				return true;
		}
		return false;
	}

	public bool ContainsSecond(T2 t2){
		foreach(Nb_TupleItem<T1, T2> item in items){
			if (item.getSecond ().Equals (t2))
				return true;
		}
		return false;
	}

	public int Count(){
		return items.Count;
	}

	public Nb_Tuple<T1, T2> FindAllSeconds(T1 first){
		Nb_Tuple<T1, T2> res = new Nb_Tuple<T1, T2> ();
		foreach(Nb_TupleItem<T1, T2> item in items){
			if (item.getFirst ().Equals (first))
				res.Add (item.getFirst (), item.getSecond ());
		}
		return res;
	}

	public Nb_Tuple<T1, T2> FindAllFirsts(T2 second){
		Nb_Tuple<T1, T2> res = new Nb_Tuple<T1, T2> ();
		foreach(Nb_TupleItem<T1, T2> item in items){
			if (item.getSecond ().Equals (second))
				res.Add (item.getFirst (), item.getSecond ());
		}
		return res;
	}

	public int FindIndex(T1 t1, T2 t2){
		for (int i = 0; i < items.Count; i++)
			if (items [i].getFirst ().Equals (t1) && items [i].getSecond ().Equals (t2))
				return i;
		return -1;
	}

	public void Insert(int index, T1 t1, T2 t2){
		items.Insert (index, new Nb_TupleItem<T1, T2>(t1, t2));
	}

	public Nb_TupleItem<T1, T2> this[int i]{
		get{
			return items [i];
		}
	}

	public class Nb_TupleItem<M1, M2> {

		private M1 first;
		private M2 second;

		public Nb_TupleItem(M1 t1, M2 t2){
			first = t1;
			second = t2;
		}

		public M1 getFirst(){
			return first;
		}

		public M2 getSecond(){
			return second;
		}

		public override string ToString ()
		{
			return "Tuple: <" + first.ToString () + " , " + second.ToString () + ">"; 
		}

	}

}
