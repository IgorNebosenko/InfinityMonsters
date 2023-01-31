using IM.Addressabless.Pool;
using UnityEditor;
using UnityEngine;

namespace IM.Pooling.Editor
{
	[CustomEditor(typeof(SubPoolContainer))]
	public class SubPoolContainerEditor : UnityEditor.Editor
	{
		public SerializedProperty SP_list;
    
		public void OnEnable()
		{
			SP_list = serializedObject.FindProperty("list");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			var link = target as AddressablesPoolContainer;
			SP_list = serializedObject.FindProperty("list");

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Min"), GUILayout.MaxWidth(64));
			EditorGUILayout.LabelField(new GUIContent("Max"), GUILayout.MaxWidth(64));
			EditorGUILayout.LabelField(new GUIContent("Name"),  GUILayout.MinWidth(64));
			EditorGUILayout.LabelField(new GUIContent("Sample"), GUILayout.MinWidth(64));
			EditorGUILayout.EndHorizontal();


			for (int i = 0; i < SP_list.arraySize; i++)
			{
				EditorGUILayout.BeginHorizontal();
				var element = SP_list.GetArrayElementAtIndex(i);
				var minSize = element.FindPropertyRelative("minSize");
				var maxSize = element.FindPropertyRelative("maxSize");
				var obj     = element.FindPropertyRelative("sample");
				var name    = element.FindPropertyRelative("name");
				var nameOpt = element.FindPropertyRelative("useObjectName");
				EditorGUILayout.PropertyField(minSize,  GUIContent.none, GUILayout.MaxWidth(64));
				EditorGUILayout.PropertyField(maxSize, GUIContent.none, GUILayout.MaxWidth(64));
				EditorGUILayout.PropertyField(nameOpt, GUIContent.none, GUILayout.MaxWidth(16));
				GUI.enabled = !nameOpt.boolValue;
				EditorGUILayout.PropertyField(name, GUIContent.none);
				GUI.enabled = true;
				EditorGUILayout.ObjectField(obj, GUIContent.none, GUILayout.MinWidth(64));
				if (GUILayout.Button("x", GUILayout.MaxWidth(24)))
				{
					SP_list.DeleteArrayElementAtIndex(i);
				}
				EditorGUILayout.EndHorizontal();
			}

			if (GUILayout.Button("+"))
			{
				SP_list.InsertArrayElementAtIndex(SP_list.arraySize);
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}
