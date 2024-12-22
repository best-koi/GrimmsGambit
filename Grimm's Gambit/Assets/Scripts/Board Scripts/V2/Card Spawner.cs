using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private CardHand _cardHand;
    [SerializeField] private CardTemplate _cardTemplate;
    [SerializeField] private Minion _caster;

    public void SpawnCard()
    {
        _cardHand.Add(_cardTemplate.SpawnCardObject(_caster).transform);
    }
}

#if UNITY_EDITOR

[CanEditMultipleObjects]
[CustomEditor(typeof(CardSpawner))]
public class CardSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CardSpawner tool = target as CardSpawner;

        EditorGUILayout.Space();

        if (GUILayout.Button("Spawn Card into Hand"))
            tool.SpawnCard();
    }
}

#endif