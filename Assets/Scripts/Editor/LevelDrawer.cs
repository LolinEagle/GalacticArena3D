using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PlayerStats.Level))]
public class LevelDrawer : PropertyDrawer{
	public static float	line = EditorGUIUtility.singleLineHeight;

	public override void	OnGUI(
		Rect p, SerializedProperty property, GUIContent label
	){
		EditorGUI.BeginProperty(p, label, property);

		// Draw the foldout arrow for the array element
		property.isExpanded = EditorGUI.Foldout(
			new Rect(p.x, p.y, p.width, line), property.isExpanded, label
		);

		if (property.isExpanded){
			EditorGUI.indentLevel++;
			float	yOffset = line + 2;

			Rect	ennemieRect = new Rect(p.x, p.y + yOffset, p.width, line);
			EditorGUI.PropertyField(
				ennemieRect, property.FindPropertyRelative("ennemieSpawn")
			);
			yOffset += line + 2;
			Rect	bonusRect = new Rect(p.x, p.y + yOffset, p.width, line);
			EditorGUI.PropertyField(
				bonusRect, property.FindPropertyRelative("bonusSpawn")
			);
			yOffset += line + 2;

			// 1. Draw "useScore" toggle
			SerializedProperty	useScoreProp =
				property.FindPropertyRelative("useScore");
			Rect	useScoreRect = new Rect(p.x, p.y + yOffset, p.width, line);
			EditorGUI.PropertyField(useScoreRect, useScoreProp);
			yOffset += line + 2;

			// 2. Draw "scoreToPass" ONLY if useScore is true
			if (useScoreProp.boolValue){
				Rect	scoreRect = new Rect(p.x, p.y + yOffset, p.width, line);
				EditorGUI.PropertyField(
					scoreRect, property.FindPropertyRelative("scoreToPass")
				);
				yOffset += line + 2;
			}

			// 3. Draw "targetRoom"
			Rect	roomRect = new Rect(p.x, p.y + yOffset, p.width, line);
			EditorGUI.PropertyField(
				roomRect, property.FindPropertyRelative("targetRoom")
			);
			
			EditorGUI.indentLevel--;
		}

		EditorGUI.EndProperty();
	}

	public override float	GetPropertyHeight(
		SerializedProperty property, GUIContent label
	){
		if (!property.isExpanded)
			return (line);

		int	lines = 5;
		if (property.FindPropertyRelative("useScore").boolValue)
			lines += 1;// scoreToPass
		return (lines * (line + 2));
	}
}
