using System.Collections;
using System.Collections.Generic;
using Glitch.Enemy;
using Glitch.Interactable;
using UnityEngine;

public class LastRoom : MonoBehaviour
{
	public Ajax ajax;
	public Gateway[] _gateways;
	
	// Update is called once per frame
	void Update () {
		foreach (var door in _gateways)
		{
			door.gameObject.SetActive(ajax.lifePoints < 1);
		}
			
	}
}
