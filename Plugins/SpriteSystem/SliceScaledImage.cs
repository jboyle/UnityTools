using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Image2D))]
public class SliceScaledImage : MonoBehaviour {
	
	public float leftMargin = 36.0f;
	public float rightMargin = 36.0f;
	public float topMargin = 36.0f;
	public float bottomMargin = 36.0f;
	
	public Vector2 scaledTransform = new Vector2(0,0);
	
	//public float meshWidth = 0;
	//public float meshHeight = 0;
	
	public void RedefineMesh(){
		
		Vector3[] vertices = new Vector3[36]; //3*3*4
		Vector2[] uv = new Vector2[36];
		int[] triangles = new int[54];
		
		Rect areaRect = GetComponent<Image2D>().areaRect;
		
		float meshWidth = areaRect.width * scaledTransform.x;
		float meshHeight = areaRect.height * scaledTransform.y;
		
		if(meshWidth == 0 || meshHeight == 0){
			meshWidth = areaRect.width;
			meshHeight = areaRect.height;	
		}
		
		float unitTexWidth = 1;
		float unitTexHeight = (meshHeight / meshWidth);

		float cx = -unitTexWidth / 2;
		float cy = -unitTexHeight / 2;
		
		float[] horizontalUVPoints = new float[4];
		float[] verticalUVPoints = new float[4];
		float[] horizontalPoints = new float[4];
		float[] verticalPoints = new float[4];
		
		horizontalUVPoints[0] = 0;
		horizontalUVPoints[1] = leftMargin / areaRect.width;
		horizontalUVPoints[2] = 1 - (rightMargin / areaRect.width); //(areaRect.width - rightMargin) / areaRect.width;
		horizontalUVPoints[3] = 1;
		
		verticalUVPoints[0] = 0;
		verticalUVPoints[1] = topMargin / areaRect.height;
		verticalUVPoints[2] = 1 - (bottomMargin / areaRect.height);//(areaRect.height - bottomMargin) / areaRect.height;
		verticalUVPoints[3] = 1;
		
		horizontalPoints[0] = cx;
		horizontalPoints[1] = cx + (unitTexWidth * (leftMargin / meshWidth));
		horizontalPoints[2] = cx + (unitTexWidth - (unitTexWidth * (rightMargin / meshWidth)));
		horizontalPoints[3] = cx + unitTexWidth;
		
		verticalPoints[0] = cy;
		verticalPoints[1] = cy + (unitTexHeight * (topMargin / meshHeight));
		verticalPoints[2] = cy + (unitTexHeight - (unitTexHeight * (bottomMargin / meshHeight)));
		verticalPoints[3] = cy + unitTexHeight;
		//Debug.Log(verticalPoints);
		
		for(int i = 0; i < 3; i++){
			for(int j = 0; j < 3; j++){
				int ind = (i * 3 + j)*4;
				int tInd = (i * 3 +j)*6; 
				float nx = horizontalPoints[j];
				float ny = verticalPoints[i];
				float fx = horizontalPoints[j+1];
				float fy = verticalPoints[i+1];
				float uvnx = horizontalUVPoints[j];
				float uvny = verticalUVPoints[i];
				float uvfx = horizontalUVPoints[j+1];
				float uvfy = verticalUVPoints[i+1];
				//Debug.Log(uvnx +" "+uvny+" "+uvfx+" "+uvfy);
				vertices[ind] = new Vector3(nx, ny, 0);
				vertices[ind + 1] = new Vector3(fx, ny, 0);
				vertices[ind + 2] = new Vector3(nx, fy, 0);
				vertices[ind + 3] = new Vector3(fx, fy, 0);
					
				uv[ind] = new Vector2(uvnx,uvny);
				uv[ind+1] = new Vector2(uvfx,uvny);
				uv[ind+2] = new Vector2(uvnx,uvfy);
				uv[ind+3] = new Vector2(uvfx,uvfy);
					
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
