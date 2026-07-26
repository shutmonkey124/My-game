using System;
using UnityEngine;

[Serializable]
public class EvidenceData
{
    [SerializeField] private string id;
    [SerializeField] private string title;

    [TextArea(2, 5)]
    [SerializeField] private string description;

    public string Id => id;
    public string Title => title;
    public string Description => description;
}