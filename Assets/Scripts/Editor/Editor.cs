using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor : MonoBehaviour
{
    [Header("Заголовок")] 
    [Range(10, 50)]
    public int i;

    [Space(10)] 
    public int y;

    [Multiline(3)]
    private string str;
    
    [TextArea(5, 8)]
    [Tooltip("Это TextArea")]
    private string str2;
}
