using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System.Reflection;
using UnityEngine;
using WellFired.Guacamole.Databinding.Unity.Runtime;

namespace WellFired.Guacamole.Databinding.Unity.Editor
{
	[CustomEditor(typeof(ObservableScriptableObject), true)]
	public class ObservableScriptableObjectEditor : UnityEditor.Editor
	{
		public class PropertyField
		{
			readonly object _obj;
			readonly PropertyInfo _info;
			readonly SerializedPropertyType _type;

			readonly MethodInfo _getter;
			readonly MethodInfo _setter;

			public PropertyInfo Info
			{
				get { return _info; }
			}

			public SerializedPropertyType Type
			{
				get { return _type; }
			}

			public String Name
			{
				get { return ObjectNames.NicifyVariableName(_info.Name); }
			}

			public PropertyField(object obj, PropertyInfo info, SerializedPropertyType type)
			{
				_obj = obj;
				_info = info;
				_type = type;

				_getter = _info.GetGetMethod();
				_setter = _info.GetSetMethod();
			}

			public object GetValue() { return _getter.Invoke(_obj, null); }
			public void SetValue(object value) { _setter.Invoke(_obj, new[] { value }); }

			public static bool GetPropertyType(PropertyInfo info, out SerializedPropertyType propertyType)
			{
				var type = info.PropertyType;
				propertyType = SerializedPropertyType.Generic;
				if (type == typeof(int))
					propertyType = SerializedPropertyType.Integer;
				else if (type == typeof(float))
					propertyType = SerializedPropertyType.Float;
				else if (type == typeof(bool))
					propertyType = SerializedPropertyType.Boolean;
				else if (type == typeof(string))
					propertyType = SerializedPropertyType.String;
				else if (type == typeof(Vector2))
					propertyType = SerializedPropertyType.Vector2;
				else if (type == typeof(Vector3))
					propertyType = SerializedPropertyType.Vector3;
				else if (type.IsEnum)
					propertyType = SerializedPropertyType.Enum;
				else if (typeof(MonoBehaviour).IsAssignableFrom(type))
					propertyType = SerializedPropertyType.ObjectReference;
				return propertyType != SerializedPropertyType.Generic;
			}
		}

		public static PropertyField[] GetProperties(object obj)
		{
			var fields = new List<PropertyField>();

			var infos = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

			foreach (var info in infos)
			{
				if (!(info.CanRead && info.CanWrite))
					continue;

				var attributes = info.GetCustomAttributes(true);

				var isExposed = attributes.Any(o => o.GetType() == typeof (ExposePropertyAttribute));
				if (!isExposed)
					continue;

				SerializedPropertyType type;
				if (!PropertyField.GetPropertyType(info, out type))
					continue;

				var field = new PropertyField(obj, info, type);
				fields.Add(field);
			}

			return fields.ToArray();
		}

		ObservableScriptableObject _instance;
		PropertyField[] _fields;

		public override void OnInspectorGUI()
		{
			_instance = target as ObservableScriptableObject;
			_fields = GetProperties(_instance);

			if (_instance == null)
				return;
			if (_fields == null)
				return;

			DrawDefaultInspector();
			Expose(_fields);
		}

		public static void Expose(PropertyField[] properties)
		{
			var emptyOptions = new GUILayoutOption[0];
			EditorGUILayout.BeginVertical(emptyOptions);
			foreach (var field in properties)
			{
				EditorGUILayout.BeginHorizontal(emptyOptions);
				if (field.Type == SerializedPropertyType.Integer)
				{
					var oldValue = (int)field.GetValue();
					var newValue = EditorGUILayout.IntField(field.Name, oldValue, emptyOptions);
					if (oldValue != newValue)
						field.SetValue(newValue);
				}
				else if (field.Type == SerializedPropertyType.Float)
				{
					var oldValue = (float)field.GetValue();
					var newValue = EditorGUILayout.FloatField(field.Name, oldValue, emptyOptions);
					if (Math.Abs(oldValue - newValue) > 0.001f)
						field.SetValue(newValue);
				}
				else if (field.Type == SerializedPropertyType.Boolean)
				{
					var oldValue = (bool)field.GetValue();
					var newValue = EditorGUILayout.Toggle(field.Name, oldValue, emptyOptions);
					if (oldValue != newValue)
						field.SetValue(newValue);
				}
				else if (field.Type == SerializedPropertyType.String)
				{
					var oldValue = (string)field.GetValue();
					var newValue = EditorGUILayout.TextField(field.Name, oldValue, emptyOptions);
					if (oldValue != newValue)
						field.SetValue(newValue);
				}
				else if (field.Type == SerializedPropertyType.Vector2)
				{
					var oldValue = (Vector2)field.GetValue();
					var newValue = EditorGUILayout.Vector2Field(field.Name, oldValue, emptyOptions);
					if (oldValue != newValue)
						field.SetValue(newValue);
				}
				else if (field.Type == SerializedPropertyType.Vector3)
				{
					var oldValue = (Vector3)field.GetValue();
					var newValue = EditorGUILayout.Vector3Field(field.Name, oldValue, emptyOptions);
					if (oldValue != newValue)
						field.SetValue(newValue);
				}
				else if (field.Type == SerializedPropertyType.Enum)
				{
					var oldValue = (Enum)field.GetValue();
					var newValue = EditorGUILayout.EnumPopup(field.Name, oldValue, emptyOptions);
					if (!Equals(oldValue, newValue))
						field.SetValue(newValue);
				}
				else if (field.Type == SerializedPropertyType.ObjectReference)
				{
					var oldValue = (UnityEngine.Object)field.GetValue();
					var newValue = EditorGUILayout.ObjectField(field.Name, oldValue, field.Info.PropertyType, emptyOptions);
					if (oldValue != newValue)
						field.SetValue(newValue);
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.EndVertical();
		}
	}
}