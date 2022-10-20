using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu]
public class TextAssets : ScriptableObject
{
    [System.Serializable]
    public class TextParameter
    {
        [SerializeField] private string _key;
        [SerializeField] private string _japanese;
        [SerializeField] private string _english;

        public string Key => _key;
        public string Japanese => _japanese;
        public string English => _english;
    }

    [SerializeField] private List<TextParameter> _texts = new List<TextParameter>();
    public List<TextParameter> Texts => _texts;

    public string GetString(string key, bool isEnglish = false)
    {
        var text = _texts.Where(x => x.Key == key).Select(x => isEnglish ? x.English : x.Japanese).FirstOrDefault();
        return text;
    }
}
