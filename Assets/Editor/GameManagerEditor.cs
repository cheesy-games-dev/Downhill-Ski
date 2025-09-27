using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameManager gm = (GameManager)target;
        if (GUILayout.Button("Start Game"))
        {
            gm.StartGame();
        }
        if (GUILayout.Button("End Game"))
        {
            gm.EndGame();
        }
        GUILayout.Label("DATA (JSON)");
        string data = JsonConvert.SerializeObject(gm.Data, Formatting.Indented);
        EditorGUILayout.TextArea(data, GUILayout.Height(300));
    }
}
