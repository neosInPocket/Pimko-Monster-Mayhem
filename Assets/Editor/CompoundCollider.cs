using UnityEditor;
using UnityEngine;

public class CompoundCollider : EditorWindow
{
    int count = 24;
    float largeRadius = 2.0f;
    float smallRadius = 0.5f;

    [MenuItem("GameObject/Add Ring Of Sphere Colliders")]
    static void Init()
    {
        CompoundCollider window = (CompoundCollider)EditorWindow.GetWindow(typeof(CompoundCollider));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Toroidal SphereCollider Settings", EditorStyles.boldLabel);
        GUILayout.Space(10);
        GUILayout.Label("Select the GameObject(s) you want to operate on first!");
        GUILayout.Space(10);

        count = (int)EditorGUILayout.Slider("Sphere Count", count, 10, 50);

        largeRadius = EditorGUILayout.Slider("Large Radius", largeRadius, 1.0f, 5.0f);
        smallRadius = EditorGUILayout.Slider("Small Radius", smallRadius, 0.1f, 2.0f);

        if (GUILayout.Button("REPLACE COLLIDERS"))
        {
            if (EditorUtility.DisplayDialog("CONFIRM!",
                "Wipe out all previous SphereColliders, add a fresh ring of SphereColliders.",
                "REPLACE COLLIDERS", "Cancel"))
            {
                foreach (var go in Selection.gameObjects)
                {
                    var allc = go.GetComponents<SphereCollider>();
                    foreach (var col in allc)
                    {
                        DestroyImmediate(col);
                    }

                    // add new
                    for (int i = 0; i < count; i++)
                    {
                        float angle = (Mathf.PI * 2 * i) / count;

                        float sin = Mathf.Sin(angle) * largeRadius;
                        float cos = Mathf.Cos(angle) * largeRadius;

                        var sp = go.AddComponent<SphereCollider>();

                        sp.radius = smallRadius;
                        sp.center = new Vector3(sin, 0, cos);
                    }
                }
            }
        }
    }
}
