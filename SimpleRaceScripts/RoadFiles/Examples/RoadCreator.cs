using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using System.Linq;

[RequireComponent(typeof(PathCreator))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RoadCreator:MonoBehaviour {

    [Range(.05f, 1.5f)]
    public float spacing = 1; 
    public float roadWidth = 1; 
    public bool autoUpdate; 
    public float tiling = 1; 

    public float roadBorderWidth = 3;

    public Vector2[] points;

    public void UpdateRoad() {
        Path path = GetComponent < PathCreator > ().path; 
        points = path.CalculateEvenlySpacedPoints(spacing); 
        GetComponent < MeshFilter > ().mesh = CreateRoadMesh(points, path.IsClosed); 

        int textureRepeat = Mathf.RoundToInt(tiling * points.Length * spacing * .05f); 
        GetComponent < MeshRenderer > ().sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat); 

        // GameObject.Find("RoadBorderRight").GetComponent < MeshFilter > ().mesh = CreateRoadBorder(points, path.IsClosed, false); 
        // GameObject.Find("RoadBorderLeft").GetComponent < MeshFilter > ().mesh = CreateRoadBorder(points, path.IsClosed, true); 
        // DestroyImmediate(GameObject.Find("RoadBorderRight").GetComponent<MeshCollider>());
        // DestroyImmediate(GameObject.Find("RoadBorderLeft").GetComponent<MeshCollider>());
        // GameObject.Find("RoadBorderRight").AddComponent<MeshCollider>();
        // GameObject.Find("RoadBorderLeft").AddComponent<MeshCollider>();
        // GameObject.Find("RoadBorderRight").GetComponent < MeshRenderer > ().sharedMaterial.mainTextureScale = new Vector3(textureRepeat, textureRepeat, textureRepeat); 
        // GameObject.Find("RoadBorderLeft").GetComponent < MeshRenderer > ().sharedMaterial.mainTextureScale = new Vector3(textureRepeat, textureRepeat, textureRepeat);  

        GameObject.Find("RoadGrass").GetComponent < MeshFilter > ().mesh = CreateRoadGrass(points, path.IsClosed);
        DestroyImmediate(GameObject.Find("RoadGrass").GetComponent<MeshCollider>()); 
        GameObject.Find("RoadGrass").AddComponent<MeshCollider>();

        GameObject.Find("LeftWallBorderLeft").GetComponent < MeshFilter > ().mesh = CreateOneWallInWall(points, path.IsClosed, false, false);
        DestroyImmediate(GameObject.Find("LeftWallBorderLeft").GetComponent<MeshCollider>()); 
        GameObject.Find("LeftWallBorderLeft").AddComponent<MeshCollider>();

        GameObject.Find("RightWallBorderLeft").GetComponent < MeshFilter > ().mesh = CreateOneWallInWall(points, path.IsClosed, false, false);
        DestroyImmediate(GameObject.Find("RightWallBorderLeft").GetComponent<MeshCollider>()); 
        GameObject.Find("RightWallBorderLeft").AddComponent<MeshCollider>();

        GameObject.Find("TopWallBorderLeft").GetComponent < MeshFilter > ().mesh = CreateOneWallInWall(points, path.IsClosed, true, false);
        DestroyImmediate(GameObject.Find("TopWallBorderLeft").GetComponent<MeshCollider>()); 
        GameObject.Find("TopWallBorderLeft").AddComponent<MeshCollider>();

        GameObject.Find("LeftWallBorderRight").GetComponent < MeshFilter > ().mesh = CreateOneWallInWall(points, path.IsClosed, false, true);
        DestroyImmediate(GameObject.Find("LeftWallBorderRight").GetComponent<MeshCollider>()); 
        GameObject.Find("LeftWallBorderRight").AddComponent<MeshCollider>();

        GameObject.Find("RightWallBorderRight").GetComponent < MeshFilter > ().mesh = CreateOneWallInWall(points, path.IsClosed, false, true);
        DestroyImmediate(GameObject.Find("RightWallBorderRight").GetComponent<MeshCollider>()); 
        GameObject.Find("RightWallBorderRight").AddComponent<MeshCollider>();

        GameObject.Find("TopWallBorderRight").GetComponent < MeshFilter > ().mesh = CreateOneWallInWall(points, path.IsClosed, true, true);
        DestroyImmediate(GameObject.Find("TopWallBorderRight").GetComponent<MeshCollider>()); 
        GameObject.Find("TopWallBorderRight").AddComponent<MeshCollider>();

        GameObject.Find("WaypointsParent").GetComponent<WaypointsManager>().CreateWaypoints(points);
         GameObject.Find("FinishLine").GetComponent<FinishLineManager>().SetFinishLine(points);
    }

    //=====================CHANGES
    Mesh CreateOneWallInWall(Vector2[] points, bool isClosed, bool isTop, bool isRight) {
        Vector3[] verts = new Vector3[points.Length * 2]; 
        Vector2[] uvs = new Vector2[verts.Length]; 
        int numTris = 2 * (points.Length - 1) + ((isClosed)?2:0); 
        int[] tris = new int[numTris * 3]; 
        int vertIndex = 0; 
        int triIndex = 0; 

        for (int i = 0; i < points.Length; i++) {
            Vector2 forward = Vector2.zero; 
            if (i < points.Length - 1 || isClosed) {
                forward += points[(i + 1) % points.Length] - points[i]; 
            }
            if (i > 0 || isClosed) {
                forward += points[i] - points[(i - 1 + points.Length) % points.Length]; 
            }

            forward.Normalize(); 
            Vector2 left = new Vector2( - forward.y, forward.x); 

            float offset = 1.017f;

            if(isRight) {
                left *= -1;
                offset = 0.982f;
            }    
            
            if(!isTop) {
                verts[vertIndex] = points[i] + left * roadWidth/roadBorderWidth * .5f; 
                verts[vertIndex + 1] = points[i] + left * roadWidth/roadBorderWidth * .5f; 
                verts[vertIndex + 1].z = 2;
            }      
            else {
                verts[vertIndex] = points[i] + left * roadWidth/roadBorderWidth * .5f; 
                verts[vertIndex + 1] = points[i] + left * roadWidth/roadBorderWidth * .5f * offset; 
            }  

            float completionPercent = i / (float)(points.Length - 1); 
            float v = 1 - Mathf.Abs(2 * completionPercent - 1); 
            uvs[vertIndex] = new Vector2(0, v); 
            uvs[vertIndex + 1] = new Vector2(1, v); 

            if (i < points.Length - 1 || isClosed) {
                if(isRight){
                    tris[triIndex] = vertIndex; 
                    tris[triIndex + 1] = (vertIndex + 2) % verts.Length; 
                    tris[triIndex + 2] = vertIndex + 1; 

                    tris[triIndex + 3] = vertIndex + 1; 
                    tris[triIndex + 4] = (vertIndex + 2) % verts.Length; 
                    tris[triIndex + 5] = (vertIndex + 3) % verts.Length; 
                }
                else{
                    tris[triIndex] = (vertIndex + 3) % verts.Length; 
                    tris[triIndex + 1] = (vertIndex + 2) % verts.Length; 
                    tris[triIndex + 2] = vertIndex + 1; 

                    tris[triIndex + 3] = vertIndex + 1; 
                    tris[triIndex + 4] = (vertIndex + 2) % verts.Length; 
                    tris[triIndex + 5] = vertIndex; 
                }
            }

            vertIndex += 2; 
            triIndex += 6; 
        }

        Mesh mesh = new Mesh();
        mesh.Clear(); 
        mesh.vertices = verts;  
        mesh.triangles = tris.Reverse().ToArray(); 

        mesh.uv = uvs;

        return mesh; 
    }

    // Mesh CreateRoadBorder(Vector2[] points, bool isClosed, bool isLeft) {
    //     Vector3[] verts = new Vector3[points.Length * 8]; 
    //     Vector2[] uvs = new Vector2[verts.Length]; 
    //     int numTris = 6 * (points.Length - 1) + ((isClosed)?2:0); 
    //     int[] tris = new int[numTris * 3]; 
    //     int vertIndex = 0; 
    //     int triIndex = 0; 

    //     for (int i = 0; i < points.Length; i++) {
    //         Vector2 forward = Vector2.zero; 
    //         if (i < points.Length - 1 || isClosed) {
    //             forward += points[(i + 1) % points.Length] - points[i]; 
    //         }
    //         if (i > 0 || isClosed) {
    //             forward += points[i] - points[(i - 1 + points.Length) % points.Length]; 
    //         }

    //         forward.Normalize(); 
    //         float sign = -1;
    //         Vector3 left = new Vector3( -forward.y, 0, forward.x); 
    //         if(isLeft)
    //            sign = 1;
            
    //         left*= sign;
    //         //front
    //         verts[vertIndex].x = points[i].x + left.x * roadWidth/roadBorderWidth * .5f + 1f + .25f;
    //         verts[vertIndex].y = 2;
    //         verts[vertIndex].z = points[i].y + left.z * roadWidth/roadBorderWidth * .5f + .25f;

    //         verts[vertIndex+1].x = points[i].x + left.x * roadWidth/roadBorderWidth * .5f + .5f + .25f;
    //         verts[vertIndex+1].y = 2;
    //         verts[vertIndex+1].z = points[i].y + left.z * roadWidth/roadBorderWidth * .5f + .5f + .25f;

    //         verts[vertIndex+2].x = points[i].x + left.x * roadWidth/roadBorderWidth * .5f + .5f + .25f;
    //         verts[vertIndex+2].y = 0;
    //         verts[vertIndex+2].z = points[i].y + left.z * roadWidth/roadBorderWidth * .5f + .5f + .25f;

    //         verts[vertIndex+3].x = points[i].x + left.x * roadWidth/roadBorderWidth * .5f + 1f + .25f;
    //         verts[vertIndex+3].y = 0;
    //         verts[vertIndex+3].z = points[i].y + left.z * roadWidth/roadBorderWidth * .5f + .25f;

    //         //back

    //         verts[vertIndex+4].x = points[i].x + left.x * roadWidth/roadBorderWidth * .5f + .5f;
    //         verts[vertIndex+4].y = 2;
    //         verts[vertIndex+4].z = points[i].y + left.z * roadWidth/roadBorderWidth * .5f - .5f;

    //         verts[vertIndex+5].x = points[i].x + left.x * roadWidth/roadBorderWidth * .5f;
    //         verts[vertIndex+5].y = 2;
    //         verts[vertIndex+5].z = points[i].y + left.z * roadWidth/roadBorderWidth * .5f;

    //         verts[vertIndex+6].x = points[i].x + left.x * roadWidth/roadBorderWidth * .5f;
    //         verts[vertIndex+6].y = 0;
    //         verts[vertIndex+6].z = points[i].y + left.z * roadWidth/roadBorderWidth * .5f;

    //         verts[vertIndex+7].x = points[i].x + left.x * roadWidth/roadBorderWidth * .5f + .5f;
    //         verts[vertIndex+7].y = 0;
    //         verts[vertIndex+7].z = points[i].y + left.z * roadWidth/roadBorderWidth * .5f - .5f;

    //         float completionPercent = i / (float)(points.Length - 1); 
    //         float v = 1 - Mathf.Abs(4 * completionPercent - 1); 
    //         uvs[vertIndex] = new Vector2(0, v); 
    //         uvs[vertIndex + 1] = new Vector2(1, v); 

    //         if (i < points.Length - 1 || isClosed) {

    //             //front
	// 			/*tris[triIndex] = vertIndex % verts.Length; 
    //             tris[triIndex + 1] = (vertIndex + 1) % verts.Length; 
	// 			tris[triIndex + 2] = (vertIndex + 2) % verts.Length; 

	// 			tris[triIndex + 3] = (vertIndex + 2) % verts.Length; 
    //             tris[triIndex + 4] = (vertIndex + 3) % verts.Length;
    //             tris[triIndex + 5] = vertIndex % verts.Length; */
                
    //             //right
    //             tris[triIndex] = (vertIndex + 1) % verts.Length; 
    //             tris[triIndex + 1] = (vertIndex + 5) % verts.Length; 
    //             tris[triIndex + 2] = (vertIndex + 6) % verts.Length; 

    //             tris[triIndex + 3] = (vertIndex + 6) % verts.Length; 
    //             tris[triIndex + 4] = (vertIndex + 2) % verts.Length; 
    //             tris[triIndex + 5] = (vertIndex + 1) % verts.Length; 

    //             //back
    //             /*tris[triIndex + 12] = (vertIndex + 7) % verts.Length; 
    //             tris[triIndex + 13] = (vertIndex + 6) % verts.Length; 
    //             tris[triIndex + 14] = (vertIndex + 5) % verts.Length; 

    //             tris[triIndex + 15] = (vertIndex + 5) % verts.Length; 
    //             tris[triIndex + 16] = (vertIndex + 4) % verts.Length; 
    //             tris[triIndex + 17] = (vertIndex + 7) % verts.Length; */

    //             //left
	// 			tris[triIndex + 6] = (vertIndex + 4) % verts.Length; 
    //             tris[triIndex + 7] = vertIndex % verts.Length; 
	// 			tris[triIndex + 8] = (vertIndex + 3) % verts.Length; 

	// 			tris[triIndex + 9] = (vertIndex + 3) % verts.Length; 
    //             tris[triIndex + 10] = (vertIndex + 7) % verts.Length; 
    //             tris[triIndex + 11] = (vertIndex + 4) % verts.Length; 
                
    //             //bottom
    //             tris[triIndex + 12] = (vertIndex + 4) % verts.Length; 
    //             tris[triIndex + 13] = (vertIndex + 5) % verts.Length; 
    //             tris[triIndex + 14] = (vertIndex + 1) % verts.Length; 

    //             tris[triIndex + 15] = (vertIndex + 1) % verts.Length; 
    //             tris[triIndex + 16] = vertIndex % verts.Length;  
    //             tris[triIndex + 17] = (vertIndex + 4) % verts.Length; 

    //             //top
    //             /*tris[triIndex + 30] = (vertIndex + 3) % verts.Length; 
    //             tris[triIndex + 31] = (vertIndex + 2) % verts.Length; 
    //             tris[triIndex + 32] = (vertIndex + 6) % verts.Length; 

    //             tris[triIndex + 33] = (vertIndex + 6) % verts.Length; 
    //             tris[triIndex + 34] = (vertIndex + 7) % verts.Length; 
    //             tris[triIndex + 35] = (vertIndex + 3) % verts.Length; */
    //         }

    //         vertIndex += 8; 
    //         triIndex += 18; 
    //     }

    //     Mesh mesh = new Mesh(); 
    //     mesh.vertices = verts; 
    //     mesh.triangles = tris; 
    //     mesh.uv = uvs; 
    //     return mesh; 
    // }

     Mesh CreateRoadGrass(Vector2[] points, bool isClosed) {
        Vector3[] verts = new Vector3[points.Length * 2]; 
        Vector2[] uvs = new Vector2[verts.Length]; 
        int numTris = 2 * (points.Length - 1) + ((isClosed)?2:0); 
        int[] tris = new int[numTris * 3]; 
        int vertIndex = 0; 
        int triIndex = 0; 

        for (int i = 0; i < points.Length; i++) {
            Vector2 forward = Vector2.zero; 
            if (i < points.Length - 1 || isClosed) {
                forward += points[(i + 1) % points.Length] - points[i]; 
            }
            if (i > 0 || isClosed) {
                forward += points[i] - points[(i - 1 + points.Length) % points.Length]; 
            }

            forward.Normalize(); 
            Vector2 left = new Vector2( - forward.y, forward.x); 

            verts[vertIndex] = points[i] + left * roadWidth * .5f; 
            verts[vertIndex + 1] = points[i] - left * roadWidth * .5f; 

            float completionPercent = i / (float)(points.Length - 1); 
            float v = 1 - Mathf.Abs(2 * completionPercent - 1); 
            uvs[vertIndex] = new Vector2(0, v); 
            uvs[vertIndex + 1] = new Vector2(1, v); 

            if (i < points.Length - 1 || isClosed) {
				tris[triIndex] = vertIndex; 
                tris[triIndex + 1] = (vertIndex + 2) % verts.Length; 
				tris[triIndex + 2] = vertIndex + 1; 

				tris[triIndex + 3] = vertIndex + 1; 
                tris[triIndex + 4] = (vertIndex + 2) % verts.Length; 
                tris[triIndex + 5] = (vertIndex + 3) % verts.Length; 
            }

            vertIndex += 2; 
            triIndex += 6; 
        }

        Mesh mesh = new Mesh(); 
        mesh.vertices = verts; 
        mesh.triangles = tris; 
        mesh.uv = uvs; 
        

        return mesh; 
    }
    //=====================CHANGES ^^^

    Mesh CreateRoadMesh(Vector2[] points, bool isClosed) {
        Vector3[] verts = new Vector3[points.Length * 2]; 
        Vector2[] uvs = new Vector2[verts.Length]; 
        int numTris = 2 * (points.Length - 1) + ((isClosed)?2:0); 
        int[] tris = new int[numTris * 3]; 
        int vertIndex = 0; 
        int triIndex = 0; 

        for (int i = 0; i < points.Length; i++) {
            Vector2 forward = Vector2.zero; 
            if (i < points.Length - 1 || isClosed) {
                forward += points[(i + 1) % points.Length] - points[i]; 
            }
            if (i > 0 || isClosed) {
                forward += points[i] - points[(i - 1 + points.Length) % points.Length]; 
            }

            forward.Normalize(); 
            Vector2 left = new Vector2( - forward.y, forward.x); 

            verts[vertIndex] = points[i] + left * roadWidth/20 * .5f; 
            verts[vertIndex + 1] = points[i] - left * roadWidth/20 * .5f; 

            float completionPercent = i / (float)(points.Length - 1); 
            float v = 1 - Mathf.Abs(2 * completionPercent - 1); 
            uvs[vertIndex] = new Vector2(0, v); 
            uvs[vertIndex + 1] = new Vector2(1, v); 

            if (i < points.Length - 1 || isClosed) {
				tris[triIndex] = vertIndex; 
                tris[triIndex + 1] = (vertIndex + 2) % verts.Length; 
				tris[triIndex + 2] = vertIndex + 1; 

				tris[triIndex + 3] = vertIndex + 1; 
                tris[triIndex + 4] = (vertIndex + 2) % verts.Length; 
                tris[triIndex + 5] = (vertIndex + 3) % verts.Length; 
            }

            vertIndex += 2; 
            triIndex += 6; 
        }

        Mesh mesh = new Mesh(); 
        mesh.vertices = verts; 
        mesh.triangles = tris; 
        mesh.uv = uvs; 
        

        return mesh; 
    }

    
}