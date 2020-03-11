using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
	public Text ftext;
	public Transform[] destinations;
	private NavigationController nc = new NavigationController();

    public void OnChanged(int index)
	{
		Debug.Log("Dropdown changed value " + index.ToString());
		nc = NavigationController.GetComponent<>
		nc.target = destinations[index - 1];
		ftext.text = "Set";
	}
}
