using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;

//class that handles all navigation
public class NavigationController : MonoBehaviour
{
	public Text ftext;
	public Dropdown dropdown;
    public GameObject trigger; // trigger to spawn and despawn AR arrows
    public Transform[] destinations; // list of destination positions
    public GameObject person; // person indicator
    private UnityEngine.AI.NavMeshPath path; // current calculated path
    private LineRenderer line; // linerenderer to display path
    public Transform target; // current chosen destination
    private bool destinationSet; // bool to say if a destination is set

	private Color c1 = Color.yellow;
    private Color c2 = Color.red;

    //create initial path, get linerenderer.
    void Start()
    {
		ftext.text = "Start";
        path = new UnityEngine.AI.NavMeshPath();
        line = GetComponent<LineRenderer>();	
		line.material = new Material(Shader.Find("Sprites/Default"));
        line.widthMultiplier = 0.2f;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        line.colorGradient = gradient;
        destinationSet = false;
    }

    void Update()
    {
		if (dropdown.value != 0)
		{
			target = destinations[dropdown.value - 1];
		}
		else
		{
			line.positionCount = 0;
			target = null;
		}
        //if a target is set, calculate and update path
        if(target != null)
        {
			ftext.text = "В пути...";
			Debug.Log("Destenation was set");
			UnityEngine.AI.NavMesh.CalculatePath(person.transform.position, target.position, 
                          UnityEngine.AI.NavMesh.AllAreas, path);
            //lost path due to standing above obstacle (drift)
            if(path.corners.Length == 0)
            {
				ftext.text = "Error";
                Debug.Log("Try moving away for obstacles (optionally recalibrate)");
            }
            line.positionCount = path.corners.Length;
           	line.SetPositions(path.corners);
            line.enabled = true;
			if (Vector3.Distance(person.transform.position,target.transform.position) < 1)
			{
				ftext.text = "Вы пришли!";
				dropdown.value = 0;
			}
        } 
    }

    //set current destination and create a trigger for showing AR arrows
    public void setDestination(int index)
    {
        target = destinations[index];
        GameObject.Instantiate(trigger, person.transform.position, 
             person.transform.rotation);
    }

}
