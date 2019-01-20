using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;

namespace Geekbrains.Editor
{
    public class MyWindow : EditorWindow
    {
        public GameObject ObjectInstantiate;
        private string _nameObject = "Hello World";
        private bool groupEnabled;
        private bool _randomColor = true;
        private int _countObject = 1;
        private float _radius = 10f;

        private Color[] _colors = new Color[]
        {
            Color.green, Color.black, Color.blue, Color.clear,
            Color.cyan, Color.red, Color.yellow, Color.white
        };

        [MenuItem("Geekbrains/My First Window")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(MyWindow));
        }

        private void OnGUI()
        {
            GUILayout.Label("Базовые настройки", EditorStyles.boldLabel);
            ObjectInstantiate = EditorGUILayout.ObjectField("Объект который хотим вставить", 
                ObjectInstantiate, typeof(GameObject), true)
                as GameObject;
            _nameObject = EditorGUILayout.TextField("Имя объекта", _nameObject);
            groupEnabled = EditorGUILayout.BeginToggleGroup("Дополнительные настройки", groupEnabled);
            _randomColor = EditorGUILayout.Toggle("Случайный цвет", _randomColor);
            _countObject = EditorGUILayout.IntSlider("Количество объектов", _countObject, 1, 
                100);
            _radius = EditorGUILayout.Slider("Радиус окружности", _radius, 10, 50);
            EditorGUILayout.EndToggleGroup();
            if (GUILayout.Button("Создать объекты"))
            {
                if (ObjectInstantiate)
                {
                    GameObject root = new GameObject("Root");
                    for (int i = 0; i < _countObject; i++)
                    {
                        float angle = i * Mathf.PI * 2 / _countObject;
                        Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle))*_radius;
                        GameObject temp = Instantiate(ObjectInstantiate, pos, Quaternion.identity) as GameObject;
                        temp.name = _nameObject + "(" + i + ")";
                        temp.transform.parent = root.transform;
                        if (temp.GetComponent<Renderer>() && _randomColor)
                        {
                            temp.GetComponent<Renderer>().material.color = _colors[Random.Range(0, _colors.Length - 1)];
                        }
                    }
                }
            }
        }
    }
}