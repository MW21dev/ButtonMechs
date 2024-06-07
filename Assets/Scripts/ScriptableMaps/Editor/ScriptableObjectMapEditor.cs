
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ScriptableObjectMap))]
public class ScriptableObjectMapEditor : Editor
{
    private void OnEnable()
    {
        
    }
    public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();
		{
			DrawDefaultInspector();

			if (GUILayout.Button("Fill"))
			{
				(target as ScriptableObjectMap).Fill();
			}

			DrawMapEditor(target as ScriptableObjectMap);


		}

		if(EditorGUI.EndChangeCheck() && EditorApplication.isPlaying)
		{
			EditorUtility.SetDirty(target);
			GameObject.FindAnyObjectByType<LevelManager>().LaunchLevel();
		}
	}

	private void DrawMapEditor(ScriptableObjectMap map)
	{
		for(int y = ScriptableObjectMap.MAP_SIZE - 1; y >= 0; --y)
		{
			using(new EditorGUILayout.HorizontalScope())
			{
				for(int x = 0; x < ScriptableObjectMap.MAP_SIZE; ++x)
				{
					int tile = map.ground[ScriptableObjectMap.MAP_SIZE * y + x];
					int newTile = EditorGUILayout.IntField(tile, GUILayout.Width(20));
					if(tile != newTile)
					{
						map.ground[y * ScriptableObjectMap.MAP_SIZE + x] = newTile;
					}
				}
			}
		}
	}
}
