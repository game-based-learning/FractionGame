using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageBoxManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleUI, contentUI;
    [SerializeField] private string _title, _content;
    public string title
    {
        get { return _title; }
        set { _title = value; titleUI.text = _title; }
    }
    public string content
    {
        get { return _content; }
        set { _content = value; contentUI.text = _content; }
    }

    private void Start()
    {
        titleUI.text = _title;
        contentUI.text = _content;
    }

    public void ChangeMessage(string newTitle, string newContent)
    {
        title = newTitle;
        content = newContent;
    }
}
