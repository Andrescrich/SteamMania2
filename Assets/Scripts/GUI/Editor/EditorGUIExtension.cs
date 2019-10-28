using UnityEngine;
using UnityEditor;
using System;
 
public class EditorGUIExtension
{
    /// <summary>
    /// Creates an array foldout like in inspectors for SerializedProperty of array type.
    /// Counterpart for standard EditorGUILayout.PropertyField which doesn't support SerializedProperty of array type.
    /// </summary>
    public static void ArrayField (SerializedProperty property)
    {
        bool wasEnabled = GUI.enabled;
        int prevIdentLevel = EditorGUI.indentLevel;
 
        // Iterate over all child properties of array
        bool childrenAreExpanded = true;
        int propertyStartingDepth = property.depth;
        while (property.NextVisible(childrenAreExpanded) && propertyStartingDepth < property.depth)
        {
            childrenAreExpanded = EditorGUILayout.PropertyField(property);
        }
 
        EditorGUI.indentLevel = prevIdentLevel;
        GUI.enabled = wasEnabled;
    }
 
    /// <summary>
    /// Creates a filepath textfield with a browse button. Opens the open file panel.
    /// </summary>
    public static string FileLabel(string name, float labelWidth, string path, string extension)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(name, GUILayout.MaxWidth(labelWidth));
        string filepath = EditorGUILayout.TextField(path);
        if (GUILayout.Button("Browse"))
        {
            filepath = EditorUtility.OpenFilePanel(name, path, extension);
        }
        EditorGUILayout.EndHorizontal();
        return filepath;
    }
 
    /// <summary>
    /// Creates a folder path textfield with a browse button. Opens the save folder panel.
    /// </summary>
    public static string FolderLabel(string name, float labelWidth, string path)
    {
        EditorGUILayout.BeginHorizontal();
        string filepath = EditorGUILayout.TextField(name, path);
        if (GUILayout.Button("Browse", GUILayout.MaxWidth(60)))
        {
            filepath = EditorUtility.SaveFolderPanel(name, path, "Folder");
        }
        EditorGUILayout.EndHorizontal();
        return filepath;
    }
 
    /// <summary>
    /// Creates an array foldout like in inspectors. Hand editable ftw!
    /// </summary>
    public static string[] ArrayFoldout(string label, string[] array, ref bool foldout)
    {
        EditorGUILayout.BeginVertical();
        EditorGUIUtility.LookLikeInspector();
        foldout = EditorGUILayout.Foldout(foldout, label);
        string[] newArray = array;
        if (foldout)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();
            int arraySize = EditorGUILayout.IntField("Size", array.Length);
            if (arraySize != array.Length)
                newArray = new string[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                string entry = "";
                if (i < array.Length)
                    entry = array[i];
                newArray[i] = EditorGUILayout.TextField("Element " + i, entry);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        return newArray;
    }
 
    /// <summary>
    /// Creates a toolbar that is filled in from an Enum. Useful for setting tool modes.
    /// </summary>
    public static Enum EnumToolbar(Enum selected)
    {
        string[] toolbar = System.Enum.GetNames(selected.GetType());
        Array values = System.Enum.GetValues(selected.GetType());
 
		for (int i=0; i  < toolbar.Length; i++)
		{
			string toolname = toolbar[i];
			toolname = toolname.Replace("_", " ");
			toolbar[i] = toolname;
		}
 
        int selected_index = 0;
        while (selected_index < values.Length)
        {
            if (selected.ToString() == values.GetValue(selected_index).ToString())
            {
                break;
            }
            selected_index++;
        }
        selected_index = GUILayout.Toolbar(selected_index, toolbar);
        return (Enum) values.GetValue(selected_index);
    }
    
    private static Color greenColor = new Color(0.4008751f,0.7924f,0.3850125f,1);
    private static Color redColor = new Color(0.8867924f, 0.3555f,0.3609208f, 1);
 
	/// <summary>
	/// Creates a button that can be toggled. Looks nice than GUI.toggle
	/// </summary>
	/// <returns>
	/// Toggle state
	/// </returns>
	/// <param name='state'>
	/// If set to <c>true</c> state.
	/// </param>
	/// <param name='label'>
	/// If set to <c>true</c> label.
	/// </param>
	public static bool ToggleButton(bool state, string label)
	{
		BuildStyle();
 
		bool out_bool;

		var normalColor = GUI.color;
		if (state)
		{
			GUI.color = greenColor;
			out_bool = GUILayout.Button(label, toggled_style);
		}
		else
		{
			
			normalColor = GUI.color;
			GUI.color = redColor;
			out_bool = GUILayout.Button(label);
		}
		
		GUI.color = normalColor;

		if (out_bool)
			return !state;
		else
			return state;
	}
 
	public class ModalPopupWindow : EditorWindow
	{
		public event System.Action<bool> OnChosen;
		string popText = "";
		string trueText = "Yes";
		string falseText = "No";
 
		public void SetValue(string text, string accept, string no)
		{
			this.popText = text;
			this.trueText = accept;
			this.falseText = no;
		}
 
		void OnGUI()
		{
			GUILayout.BeginVertical();
			GUILayout.Label(popText);
			GUILayout.BeginHorizontal();
			if (GUILayout.Button(trueText))
			{
				if (OnChosen != null)
					OnChosen(true);
				this.Close();
			}
			if (GUILayout.Button(falseText))
			{
				if (OnChosen != null)
					OnChosen(false);
				this.Close();
			}
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
		}
	}
 
//	public static bool ModalPopup(string text, string trueText, string falseText)
//	{
//		ModalPopupWindow popwindow = (ModalPopupWindow) EditorWindow.GetWindow(typeof(EditorGUIExtension.ModalPopupWindow));
//		popwindow.title = "Modal";
//		popwindow.SetValue(text, trueText, falseText);
//		popwindow.OnChosen += delegate(bool retValue) {
//			return retValue;
//		};
//	}
 
	static GUIStyle toggled_style;
	public GUIStyle StyleButtonToggled
	{
		get
		{
			BuildStyle();
			return toggled_style;
		}
	}
 
	static GUIStyle labelText_style;
	public static GUIStyle StyleLabelText
	{
		get
		{
			BuildStyle();
			return labelText_style;
		}
	}
 
	private static void BuildStyle() {
		
		toggled_style = new GUIStyle(GUI.skin.button);
		
		
		if (toggled_style == null)
		{
			var normalColor = GUI.color;
			toggled_style = new GUIStyle(GUI.skin.button);
			toggled_style.normal.background = toggled_style.onActive.background;
			toggled_style.normal.textColor = toggled_style.onActive.textColor;


		}


		if (labelText_style == null)
		{
			labelText_style = new GUIStyle(EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).textField);
			labelText_style.normal = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).button.onNormal;
		}
	}
}