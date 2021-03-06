﻿using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.EditorGUILayout;

namespace AddressableManager.AddressableSetter.Editor
{
    internal class ListsEditor<T> where T : ScriptableObject
    {
        private bool AutoUpdate { get; set; }
        private UnityEditor.Editor MainEditor { get; }
        private bool ShowList { get; set; }
        private CreateDisplayList DisplayList { get; } = new CreateDisplayList();
        internal ListsEditor(UnityEditor.Editor editor, bool showList = false)
        {
            MainEditor = editor;
            ShowList = showList;
        }
        internal bool Init(string status, Dictionary<string, Tuple<List<AData>, AutoLoad>> lists)
        {
            if (lists.Count <= 0) return false;
            ShowList = BeginFoldoutHeaderGroup(ShowList, status);
            if (ShowList)
            {
                AutoUpdate = GUILayout.Toggle(AutoUpdate, "Auto Update");
                BeginVertical("Box");
                lists.ForEach(o => DisplayList.Create(o.Value.Item1, MainEditor.serializedObject.FindProperty(o.Key), o.Value.Item2, MainEditor));
                EndVertical();
            }
            EndFoldoutHeaderGroup();
            Space(5);
            return AutoUpdate;
        }

    }
}