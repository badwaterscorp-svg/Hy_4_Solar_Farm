using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerPrefsCaller : MonoBehaviour
{
    [Serializable]
    public class KeyValueEntry
    {
        public string Key;
        public KeyType Type;
        public string StringValue;
        public int IntValue;
        public float FloatValue;
    }

    public enum KeyType
    {
        String,
        Int,
        Float
    }

    [SerializeField] private List<KeyValueEntry> _entries = new List<KeyValueEntry>();
    [SerializeField] private KeyValueEntry _toSendEntry;

    [ContextMenu("Read KeyStorage Values")]
    public void ReadKeyStorageValues()
    {
        _entries.Clear();
        FieldInfo[] fields = typeof(KeyStorage).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            if (field.IsLiteral || !field.IsInitOnly) continue;
            if (field.FieldType != typeof(string)) continue;

            string key = (string)field.GetValue(null);
            _entries.Add(new KeyValueEntry
            {
                Key = key,
                Type = KeyType.String,
                StringValue = PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : string.Empty
            });
        }
    }

    [ContextMenu("Send to PlayerPrefs")]
    public void Send()
    {
        switch (_toSendEntry.Type)
        {
            case KeyType.String:
                PlayerPrefs.SetString(_toSendEntry.Key, _toSendEntry.StringValue);
                break;
            case KeyType.Int:
                PlayerPrefs.SetInt(_toSendEntry.Key, _toSendEntry.IntValue);
                break;
            case KeyType.Float:
                PlayerPrefs.SetFloat(_toSendEntry.Key, _toSendEntry.FloatValue);
                break;
        }
        PlayerPrefs.Save();
    }
}
