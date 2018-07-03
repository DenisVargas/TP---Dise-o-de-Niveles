using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CheckPoint))]
public class CheckPointEditor : Editor{
    CheckPoint A;
    private void OnEnable()
    {
        A = (CheckPoint)target;
        A.Coll = A.GetComponent<SphereCollider>();
    }

    private void OnSceneGUI()
    {
        Handles.BeginGUI();
        Rect SceneW = EditorWindow.GetWindow<SceneView>().camera.pixelRect;

        Vector2 ButtonA_S = new Vector2(100, 50);
        Vector2 ButtonA_Pos = new Vector2(SceneW.width / 2 - 100, SceneW.height - 100);

        Rect SliderARect = new Rect(SceneW.width / 2 + 20,SceneW.height - 80 ,180,50);
        A.Range = GUI.HorizontalSlider(SliderARect, A.Range, CheckPoint.MinimunRange, 10);
        A.Coll.radius = A.Range;

        Rect LabelARect = new Rect(SceneW.width / 2 + 20, SceneW.height - 100, 200, 100);
        GUI.Label(LabelARect, "Current Radius is " + A.Range);


        A.Range = EditorGUI.FloatField(new Rect(SceneW.width/2 + 210,SceneW.height - 90,50,20),A.Range);

        if (GUI.Button(new Rect(ButtonA_Pos, ButtonA_S), "Reset Radius"))
        {
            A.Range = CheckPoint.MinimunRange;
        }

        Handles.EndGUI();
        RaycastHit hitp;
        Physics.Raycast(A.transform.position, -A.transform.up,out hitp, - 100);
        float b = A.transform.position.y - hitp.point.y;
        if (b <= A.Range)
            Handles.color = Color.green;
        else
            Handles.color = Color.red;


        Vector3 pos = new Vector3(A.transform.position.x, hitp.point.y, A.transform.position.z);

        Handles.DrawWireDisc(pos, A.transform.up, A.Range);
        Handles.DrawLine(pos, A.transform.position);
    }
}
