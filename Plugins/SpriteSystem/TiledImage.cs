using UnityEngine;
using System.Collections;

public class TiledImage : MonoBehaviour {
	
	public int numColumns;
	public int numRows;
	
	public void RedefineMesh(){
		
		Rect areaRect = GetComponent<Image2D>().areaRect;
		float unitTexWidth = 1;
		float unitTexHeight = areaRect.height / areaRect.width;
		
		Vector3[] vertices;
		Vector2[] uv;
		int[] triangles;
		
		vertices = new Vector3[numColumns * numRows * 4];
		uv = new Vector2[numColumns * numRows * 4];
		triangles = new int[numColumns * numRows * 6];
			
		float cx = -unitTexWidth / 2.0f;
		float cy = -unitTexHeight / 2.0f;
		// generate repeated texture
		for(int i = 0; i < numRows; i++){
			for(int j = 0; j < numColumns; j++){
				//Debug.Log(i + " " + j);
				int ind = (i * numColumns + j)*4;
				int tInd = (i * numColumns +j)*6; 
				float nx = j * unitTexWidth;
				float ny = i * unitTexHeight;
				float fx = (j+1) * unitTexWidth;
				float fy = (i+1) * unitTexHeight;
				float uvx = 1.0f;
				float uvy = 1.0f;
				//Debug.Log(nx +" "+ny+" "+fx+" "+fy);
				vertices[ind] = new Vector3(nx + cx, ny + cy, 0);
				vertices[ind + 1] = new Vector3(fx + cx, ny+cy, 0);
				vertices[ind + 2] = new Vector3(nx + cx, fy+cy, 0);
				vertices[ind + 3] = new Vector3(fx + cx, fy+cy, 0);
					
				uv[ind] = new Vector2(0,0);
				uv[ind+1] = new Vector2(uvx,0);
				uv[ind+2] = new Vector2(0,uvy);
				uv[ind+3] = new Vector2(uvx,uvy);
				
				triangles[tInd] = ind;
				triangles[tInd + 1] = ind + 2;
				triangles[tInd + 2] = ind + 1;
				triangles[tInd + 3] = ind + 2;
				triangles[tInd + 4] = ind + 3;
				triangles[tInd + 5] = ind + 1;
			}
		}
		
		GetComponent<Image2D>().currentMesh.vertices = vertices;                        
		GetComponent<Image2D>().currentMesh.uv = uv;
		GetComponent<Image2D>().currentMesh.triangles = triangles;
		GetComponent<Image2D>().currentMesh.RecalculateNormals();
	}
}
