using System.Collections.Generic;
using UnityEngine;

public class EvidenceManager : MonoBehaviour
{
    public static EvidenceManager Instance { get; private set; }

    [SerializeField]
    private List<EvidenceData> collectedEvidence =
        new List<EvidenceData>();

    public IReadOnlyList<EvidenceData> CollectedEvidence =>
        collectedEvidence;

    public int EvidenceCount => collectedEvidence.Count;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Keeps the evidence list alive when changing scenes later.
        DontDestroyOnLoad(gameObject);
    }

    public bool AddEvidence(EvidenceData evidence)
    {
        if (evidence == null ||
            string.IsNullOrWhiteSpace(evidence.Id))
        {
            Debug.LogError(
                "Cannot collect evidence without a valid ID."
            );

            return false;
        }

        if (HasEvidence(evidence.Id))
        {
            Debug.Log(
                $"Evidence already collected: {evidence.Title}"
            );

            return false;
        }

        collectedEvidence.Add(evidence);

        Debug.Log(
            $"Evidence collected: {evidence.Title}. " +
            $"Total evidence: {EvidenceCount}"
        );

        return true;
    }

    public bool HasEvidence(string evidenceId)
    {
        if (string.IsNullOrWhiteSpace(evidenceId))
            return false;

        foreach (EvidenceData evidence in collectedEvidence)
        {
            if (evidence != null &&
                evidence.Id == evidenceId)
            {
                return true;
            }
        }

        return false;
    }
}