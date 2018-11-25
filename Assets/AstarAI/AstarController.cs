using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarController : MonoBehaviour {
	public int gridSizeX;
	public int gridSizeY;
	public Vector2 offset;
	public List<Vector2> nodeList;
	public List<Vector2> wallList;
	public Transform startPoint;
	public Transform endPoint;
	public List<float> distances;
	public float minDist = Mathf.Infinity;
	
	[Header("Debug")]
	[SerializeField] private float _nodeSize;
	[SerializeField] private float _nodeSpread;
	[SerializeField] private List<float> fCostValues;
	[SerializeField] private List<Vector2> finalPath;
	[SerializeField] private Vector2 currentNode;

	// Use this for initialization
	void Start () 
	{
		for(int x = 0; x < gridSizeX; x++)
		{
			for(int y = 0; y < gridSizeY; y++)
			{
				Vector2 tempPoint = new Vector2(x + offset.x, y + offset.y);

				SortNodes(tempPoint);
			}
		}

		Calculate();
	}

	void SortNodes (Vector2 node)
	{
		Collider2D hits = Physics2D.OverlapCircle(node, _nodeSpread);

		if(hits != null)
		{
			if(hits.tag == "Wall")
			{
				wallList.Add(node);
			}

			else
			{
				nodeList.Add(node);
			}
		}

		else
		{
			Debug.Log("Error");
		}
	}
	
	void OnDrawGizmos ()
	{
		Gizmos.color = new Color(1f, 0f, 0f, 0.25f);

		foreach(Vector2 point in nodeList)
		{
			Gizmos.DrawSphere(point, _nodeSize);
		}

		foreach(Vector2 point in finalPath)
		{
			Gizmos.DrawCube(point, Vector3.one);
		}
	}

	void Calculate ()
	{
		Debug.Log("Calculating fCost, gCost, hCost, minDist...");

		foreach (Vector2 node in nodeList)
		{
			// *MANHATTAN*
			float h = Mathf.Abs(node.x - endPoint.position.x) + Mathf.Abs(node.y - endPoint.position.y);

			// *DIAGONAL DISTANCE*
			//float h = Mathf.Max(Mathf.Abs(node.x - endPoint.position.x), Mathf.Abs(node.y - endPoint.position.y));

			// *EUCLIDIAN*	
			//float h = Mathf.Sqrt(Mathf.Pow((node.x - endPoint.position.x), 2) + Mathf.Pow((node.y - endPoint.position.y), 2));
			float g = Vector2.Distance(startPoint.position, node);

			float f = g + h;

			fCostValues.Add(f);

			float dist = Vector2.Distance(startPoint.position, node);
			distances.Add(dist);
		}

		Debug.Log("F Cost calculated");

		foreach(float num in distances)
		{
			if(num < minDist)
			{
				minDist = num;
			}
		}

		int index = distances.IndexOf(minDist);
		Debug.Log("Next point dist = " + minDist.ToString() + ", index = " + index.ToString());
		Debug.Log("All values calulated, finding path...");
		currentNode = nodeList[index];
		finalPath.Add(currentNode);
		FindNextNode();
	}

	void FindNextNode ()
	{
		if (Vector2.Distance(currentNode, endPoint.position) >= 1)
		{
			List<Vector2> options = new List<Vector2>();

			options.Add(new Vector2(currentNode.x + 1, currentNode.y));
			options.Add(new Vector2(currentNode.x + 1, currentNode.y - 1));
			options.Add(new Vector2(currentNode.x, currentNode.y - 1));
			options.Add(new Vector2(currentNode.x - 1, currentNode.y - 1));
			options.Add(new Vector2(currentNode.x - 1, currentNode.y));
			options.Add(new Vector2(currentNode.x - 1, currentNode.y + 1));
			options.Add(new Vector2(currentNode.x, currentNode.y + 1));
			options.Add(new Vector2(currentNode.x + 1, currentNode.y + 1));

			float minFcost = 500f;

			foreach(Vector2 op in options)
			{
				if(nodeList.Contains(op))
				{
					int index = nodeList.IndexOf(op);
				
					if(fCostValues[index] < minFcost)
					{
						minFcost = fCostValues[index];
					}
				}

				else
				{
					continue;
				}
			}

			int newPoint = fCostValues.IndexOf(minFcost);

			currentNode = nodeList[newPoint];
			finalPath.Add(currentNode);
			options.Clear();
			FindNextNode();
		}
	}
}
