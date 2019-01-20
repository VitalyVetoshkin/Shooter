using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MinecraftGenerator
{
    public class GeneratorWindow : EditorWindow
    {
        private int scaleX;
        private int scaleY;
        private int scaleZ;
        private float spaces;
        
        [MenuItem("Geekbrains/Minecraft Generator Window")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(GeneratorWindow));
        }

        private void OnGUI()
        {
            GUILayout.Label("Размер мира:", EditorStyles.boldLabel);
            scaleX = EditorGUILayout.IntSlider("По оси X", scaleX, 10, 
                100);
            scaleY = EditorGUILayout.IntSlider("По оси Y", scaleY, 10, 
                100);
            scaleZ = EditorGUILayout.IntSlider("По оси Z", scaleZ, 10, 
                100);
            
            GUILayout.Label("Пористость пещер:", EditorStyles.boldLabel);
            spaces = EditorGUILayout.Slider("В процентах", spaces, 30, 
                60);
            
            if (GUILayout.Button("Сгенерировать карту"))
            {
                Debug.Log("Генерация карты...");
                
                if (MapGenerator(Vector3.zero, scaleX, scaleY, scaleZ, spaces)) 
                    Debug.Log("Карта успешно сгенерирована!");
                else Debug.Log("Возникла проблема при генерации карты!");
                               
            }
        }
        
        /// <summary>
        /// Генератор дырявого сыра
        /// </summary>
        /// <param name="startPosition">Стартовая позиция</param>
        /// <param name="scaleX">Ширина</param>
        /// <param name="scaleY">Высота</param>
        /// <param name="scaleZ">Длина</param>
        /// <param name="spaces">Пористость</param>
        /// <returns></returns>
        private bool MapGenerator(Vector3 startPosition, int scaleX, int scaleY, int scaleZ, float spaces)
        {
            Vector3 currentPosition = startPosition;
            var parent = new GameObject {name = "Map"};

            for (int i = 0; i < scaleX; i++)
            {
                for (int j = 0; j < scaleY; j++)
                {
                    for (int k = 0; k < scaleZ; k++)
                    {
                        if (Random.Range(0, 100) > spaces)
                        {
                            spaces += (spaces >= 60)? 0 : 20; 
                            continue;
                        }
                        else
                        {
                            spaces -= (spaces <= 30)? 0 : 20;
                        }
                        
                        var temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        temp.transform.position = new Vector3(i, j, k);
                        temp.transform.parent = parent.transform;
                    }
                }
            }
            
            return true;
        }
    }
}