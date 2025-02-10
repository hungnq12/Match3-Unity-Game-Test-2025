#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomEditor(typeof(UnityEngine.Object), true)]
public class ButtonAttributeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Type type = target.GetType();
        MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (MethodInfo method in methods)
        {
            if (method.GetCustomAttribute(typeof(ButtonAttribute)) != null)
            {
                if (GUILayout.Button(ObjectNames.NicifyVariableName(method.Name)))
                {
                    if (method.IsStatic)
                        method.Invoke(null, null);
                    else
                        method.Invoke(target, null);
                }
            }
        }
        base.OnInspectorGUI();
    }
}
#endif

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class ButtonAttribute : Attribute
{
    public ButtonAttribute() { }
}
