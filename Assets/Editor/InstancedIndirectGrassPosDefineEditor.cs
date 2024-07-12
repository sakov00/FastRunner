using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(InstancedIndirectGrassPosDefine))]
public class InstancedIndirectGrassPosDefineEditor : Editor
{
    private const float thumbnailSize = 64f;
    private const float padding = 10f;
    InstancedIndirectGrassPosDefine script;

    public override void OnInspectorGUI()
    {
        // Отрисовываем стандартный инспектор
        DrawDefaultInspector();

        // Получаем ссылку на целевой скрипт
        script = (InstancedIndirectGrassPosDefine)target;

        // Добавляем кнопку для вызова метода UpdatePos
        if (GUILayout.Button("Update Grass Positions"))
        {
            script.UpdatePos();
        }

    }
}