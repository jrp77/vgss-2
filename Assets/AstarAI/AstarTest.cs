using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarTest : MonoBehaviour {

	public int gridSizeX;
	public int gridSizeY;
	public Vector2 offset;
	public List<Vector2> points;
	public List<Vector2> nodeList;
	public List<Vector2> wallList;

	// Use this for initialization
	void Start () 
	{
		for(int x = 0; x < gridSizeX; x++)
		{
			for(int y = 0; y < gridSizeY; y++)
			{
				Vector2 tempPoint = new Vector2(x + offset.x, y + offset.y);

				points.Add(tempPoint);
			}
		}	
	}

	void SortNodes (Vector2 node)
	{
		Collider2D hit = Physics2D.OverlapCircle(node, 0.5f);

		if(hit.tag == "Wall")
		{
			wallList.Add(node);
		}

		else
		{
			nodeList.Add(node);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
