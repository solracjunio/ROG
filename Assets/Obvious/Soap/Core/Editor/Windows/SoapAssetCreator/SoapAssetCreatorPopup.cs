using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.IMGUI.Controls;


namespace Obvious.Soap.Editor
{
    public class SoapAssetCreatorPopup : PopupWindowContent
    {
        private Rect _position;
        private readonly Vector2 _dimensions = new Vector2(350, 160f);
        private SoapSettings _soapSettings;
        private string _assetName = string.Empty;
        private int _tagIndex;
        private int _destinationFolderIndex;
        private string _path;
        private Type _selectedType;
        private Texture[] _icons;
        private bool _assetCreated;
        private Action<ScriptableBase> _onAssetCreated;
        private EOrigin _origin;

        public enum EOrigin
        {
            Inspector,
            ProjectWindow,
            SoapWizard
        }

        public override Vector2 GetWindowSize()
        {
            if (_origin == EOrigin.SoapWizard || _origin == EOrigin.ProjectWindow)
            {
                return new Vector2(_dimensions.x, _dimensions.y + 140f);
            }

            return _dimensions;
        }

        public SoapAssetCreatorPopup(Rect rect, EOrigin origin)
        {
            Init(rect, origin, null, string.Empty, 0, true);
            SoapWizardWindow.IsPopupOpen = true;
        }

        public SoapAssetCreatorPopup(Rect rect, EOrigin origin, Type type, string assetName, int tag,
            Action<ScriptableBase> onAssetCreated)
        {
            Init(rect, origin, type, assetName, tag, false);
            _onAssetCreated = onAssetCreated;
        }

        private void Init(Rect rect, EOrigin origin, Type type, string assetName, int tag, bool openedFromWizard)
        {
            _position = rect;
            _origin = origin;
            _selectedType = type;
            _assetName = assetName;
            _tagIndex = tag;
            _soapSettings = SoapEditorUtils.GetOrCreateSoapSettings();
            Load();
        }

        public override void OnClose()
        {
            if (SoapWizardWindow.IsPopupOpen)
                SoapWizardWindow.IsPopupOpen = false;
        }

        private void Load()
        {
            _soapSettings = SoapEditorUtils.GetOrCreateSoapSettings();
            var isAutoPath = _soapSettings.CreatePathMode == ECreatePathMode.Auto;
            _path = isAutoPath
                ? SoapFileUtils.GetSelectedFolderPathInProjectWindow()
                : SoapEditorUtils.CustomCreationPath;
            _icons = new Texture[6];
            _icons[0] = Resources.Load<Texture>("Icons/icon_scriptableVariable");
            _icons[1] = Resources.Load<Texture>("Icons/icon_scriptableEvent");
            _icons[2] = Resources.Load<Texture>("Icons/icon_scriptableList");
            _icons[3] = Resources.Load<Texture>("Icons/icon_scriptableDictionary");
            _icons[4] = Resources.Load<Texture>("Icons/icon_scriptableEnum");
            _icons[5] = Resources.Load<Texture>("Icons/icon_scriptableSave");
        }

        public override void OnGUI(Rect rect)
        {
            editorWindow.position = SoapInspectorUtils.CenterInWindow(editorWindow.position, _position);
            SoapInspectorUtils.DrawPopUpHeader(editorWindow, "Create New Soap Asset");
            GUILayout.BeginVertical(SoapInspectorUtils.Styles.PopupContent);
            if (_origin != EOrigin.Inspector)
            {
                DrawSelectAssetButton();
            }

            if (_selectedType != null)
            {
                DrawAssetPreview(_selectedType);
                GUILayout.Space(5f);
                DrawTag();
            }
            else
            {
                if (_assetCreated)
                {
                    EditorGUILayout.HelpBox("Asset Created!", MessageType.Info);
                    GUILayout.Space(EditorGUIUtility.singleLineHeight * 2.3f);
                }
                else
                {
                    GUILayout.Space(EditorGUIUtility.singleLineHeight * 2f + 5f);
                }
            }

            if (_origin != EOrigin.Inspector)
            {
                GUILayout.FlexibleSpace();
            }
            else
            {
                GUILayout.Space(5f);
            }

            if (_selectedType != null)
            {
                DrawPath();
            }

            GUILayout.Space(5f);
            DrawCreateButton();
            GUILayout.EndVertical();
        }

        private void DrawAssetPreview(Type type)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Asset:", GUILayout.Width(127f));
            var icon = _icons[GetIconIndex(type)];
            var style = new GUIStyle(GUIStyle.none);
            GUILayout.Box(icon, style, GUILayout.Width(18), GUILayout.Height(18));
            _assetName = EditorGUILayout.TextField(_assetName);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void DrawSelectAssetButton()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Select Asset To Create", GUILayout.Height(35f)))
            {
                ShowAssetCreatorDropdown(type =>
                {
                    _selectedType = type;
                    var prefix = _soapSettings.GetPrefix(type);
                    _assetName = $"{prefix}{type.Name}";
                    _tagIndex = 0;
                    _assetCreated = false;
                });
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawTag()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Tag:", GUILayout.Width(147f));
            var style = new GUIStyle(EditorStyles.popup);
            var tag = _soapSettings.Tags[_tagIndex];
            if (GUILayout.Button(tag, style))
            {
                SoapInspectorUtils.ShowTagMenu(_tagIndex, newTag => { _tagIndex = newTag; });
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawPath()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Destination Folder:");
            var style = new GUIStyle(EditorStyles.popup);
            var isDefinedFromSettings = _soapSettings.CreatePathMode == ECreatePathMode.Manual
                                        && _origin != EOrigin.ProjectWindow;
            var options = new[]
            {
                isDefinedFromSettings ? "Defined from Settings" : "Selected in Project",
                "Custom"
            };

            if (GUILayout.Button(options[_destinationFolderIndex], style))
            {
                SoapInspectorUtils.ShowPathMenu(options, _destinationFolderIndex,
                    newTag => { _destinationFolderIndex = newTag; });
            }

            EditorGUILayout.EndHorizontal();

            if (_destinationFolderIndex == 0)
            {
                GUI.enabled = false;
                _path = isDefinedFromSettings
                    ? SoapEditorUtils.CustomCreationPath
                    : SoapFileUtils.GetSelectedFolderPathInProjectWindow();
                EditorGUILayout.TextField($"{_path}");
                GUI.enabled = true;
            }
            else
            {
                _path = EditorGUILayout.TextField(_path);
            }
        }

        private void DrawCreateButton()
        {
            var canCreate = _selectedType != null && _assetName.Length > 0;
            GUI.enabled = canCreate;
            if (SoapInspectorUtils.DrawCallToActionButton("Create", SoapInspectorUtils.ButtonSize.Medium))
            {
                CreateSoapSoAtPath(_selectedType, _assetName, _path, _tagIndex);
                _selectedType = null;
                _assetName = string.Empty;
                _tagIndex = 0;
                _assetCreated = true;
                if (_origin == EOrigin.Inspector)
                    editorWindow.Close();
            }

            GUI.enabled = true;
        }

        private void CreateSoapSoAtPath(Type type, string name, string path, int tagIndex)
        {
            var scriptable = SoapEditorUtils.CreateScriptableObjectAt(type, name, path);
            var scriptableBase = (ScriptableBase)scriptable;
            scriptableBase.TagIndex = tagIndex;
            _onAssetCreated?.Invoke(scriptableBase);
            AssetDatabase.SaveAssets();
        }

        private int GetIconIndex(Type type)
        {
            if (type.IsSubclassOf(typeof(ScriptableVariableBase)))
                return 0;
            if (type.IsSubclassOf(typeof(ScriptableEventBase)))
                return 1;
            if (type.IsSubclassOf(typeof(ScriptableListBase)))
                return 2;
            if (type.IsSubclassOf(typeof(ScriptableDictionaryBase)))
                return 3;
            if (type.IsSubclassOf(typeof(ScriptableEnumBase)))
                return 4;
            if (type.IsSubclassOf(typeof(ScriptableSaveBase)))
                return 5;

            return 0;
        }


        private void ShowAssetCreatorDropdown(Action<Type> callback)
        {
            Vector2 dropdownSize = new Vector2(300, 300);

            PathTree<Type> typeTree = new PathTree<Type>();

            foreach (var type in TypeCache.GetTypesWithAttribute<CreateAssetMenuAttribute>()
                         .Where(t => t.IsSubclassOf(typeof(ScriptableBase))))
            {
                var name = type.GetCustomAttribute<CreateAssetMenuAttribute>().menuName;
                var i = name.LastIndexOf('/');
                name = (i == -1) ? name : name.Substring(0, i + 1) + type.Name;
                typeTree.AddEntry(name, type, 1);
            }

            var dropdown = new TypeSelectorDropdown(new AdvancedDropdownState(), typeTree,
                (s) => { callback?.Invoke(s); })
            {
                DropdownSize = dropdownSize
            };

            dropdown.Show(new Rect());
            var widthOffset = (_position.width - dropdownSize.x) / 2f;
            var heightOffset = (_position.height - dropdownSize.y) / 2f;
            Vector2 xy = new Vector2(_position.x + widthOffset, _position.y + heightOffset);
            var rect = new Rect(xy.x, xy.y, _position.width, _position.height);

            var window =
                typeof(TypeSelectorDropdown)
                    .GetField("m_WindowInstance", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.GetValue(dropdown) as EditorWindow;

            window.position = rect;
        }


        [MenuItem(itemName: "Assets/Create/Soap Asset Creator %&a", isValidateFunction: false, priority: -1)]
        public static void SoapSearchMenu()
        {
            var project =
                typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.ProjectBrowser");
            var projectWindow = EditorWindow.GetWindow(project);
            EditorApplication.delayCall += () =>
            {
                PopupWindow.Show(new Rect(), new SoapAssetCreatorPopup(projectWindow.position, EOrigin.ProjectWindow));
            };
        }

        private class PathTree<T>
        {
            public Dictionary<string, PathTree<T>> Branches { get; } = new Dictionary<string, PathTree<T>>();
            public T Data { get; private set; }

            public void AddEntry(string route, T data, int depth)
            {
                InsertRecursive(route.Split('/'), depth, data);
            }

            private void InsertRecursive(string[] segments, int depth, T data)
            {
                if (depth >= segments.Length)
                {
                    Data = data;
                    return;
                }

                if (!Branches.TryGetValue(segments[depth], out PathTree<T> subtree))
                {
                    subtree = new PathTree<T>();
                    Branches.Add(segments[depth], subtree);
                }

                subtree.InsertRecursive(segments, depth + 1, data);
            }
        }

        private class TypeSelectorDropdown : AdvancedDropdown
        {
            private readonly PathTree<Type> _typeTree;
            private readonly Action<Type> _onTypeSelected;
            private readonly List<Type> _typeRegistry = new List<Type>();

            public Vector2 DropdownSize
            {
                get => minimumSize;
                set => minimumSize = value;
            }

            public TypeSelectorDropdown(AdvancedDropdownState state, PathTree<Type> typeTree,
                Action<Type> onTypeSelected)
                : base(state)
            {
                _typeTree = typeTree;
                _onTypeSelected = onTypeSelected;
            }

            protected override void ItemSelected(AdvancedDropdownItem item)
            {
                base.ItemSelected(item);
                if (item.id >= 0 && item.id < _typeRegistry.Count)
                {
                    _onTypeSelected?.Invoke(_typeRegistry[item.id]);
                }
            }

            protected override AdvancedDropdownItem BuildRoot()
            {
                var root = new AdvancedDropdownItem("Soap Asset Type Selector");

                foreach (KeyValuePair<string, PathTree<Type>> branch in _typeTree.Branches)
                {
                    PopulateDropdown(branch.Value, branch.Key, root);
                }

                return root;
            }

            private void PopulateDropdown(PathTree<Type> node, string label, AdvancedDropdownItem parentItem)
            {
                if (node.Data != null)
                {
                    _typeRegistry.Add(node.Data);
                    parentItem.AddChild(new AdvancedDropdownItem(label) { id = _typeRegistry.Count - 1 });
                    return;
                }

                var categoryItem = new AdvancedDropdownItem(label);
                foreach (KeyValuePair<string, PathTree<Type>> branch in node.Branches)
                {
                    PopulateDropdown(branch.Value, branch.Key, categoryItem);
                }

                parentItem.AddChild(categoryItem);
            }
        }
    }
}