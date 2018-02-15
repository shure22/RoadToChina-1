using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplay : MonoBehaviour {

	public static string seat0 = "empty";
	public static string seat1 = "empty";
	public static string seat2 = "empty";
	public GameObject[] dishPrefabs;
    public float y=2;

	public bool seatflag = true;

	public Transform customerobj;

	// Use this for initialization
	void Start () {
		if (seatflag){
			InvokeRepeating("MakeCustomers", 0, 4);
		}
	}

	void MakeCustomers() {


		if (seat0 == "empty") {
            int dishPreIndex = Random.Range(0, dishPrefabs.Length);
            GameObject dish = dishPrefabs[dishPreIndex];
            Instantiate (customerobj, new Vector2 (-3, y), customerobj.rotation);
			Instantiate (dish, new Vector2(-2,y+1), dish.transform.rotation);
			seat0 = "occupied";
		} else if(seat1 == "empty") {
            int dishPreIndex = Random.Range(0, dishPrefabs.Length);
            GameObject dish = dishPrefabs[dishPreIndex];
            Instantiate (customerobj, new Vector2 (0, y), customerobj.rotation);
			Instantiate (dish, new Vector2(1,y+1), dish.transform.rotation);
			seat1 = "occupied";
		} else if(seat2 == "empty") {
            int dishPreIndex = Random.Range(0, dishPrefabs.Length);
            GameObject dish = dishPrefabs[dishPreIndex];
            Instantiate (customerobj, new Vector2 (3, y), customerobj.rotation);
			Instantiate (dish, new Vector2(4,y+1), dish.transform.rotation);
			seat2 = "occupied";
		}
	}

	void NoSeat() {
		if (seat0 == "occupied" && seat1 == "occupied" && seat2 == "occupied") {
			seatflag = false;
		}
	}
}
