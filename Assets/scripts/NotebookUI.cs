using System.Text;
using TMPro;
using UnityEngine;

public class NotebookUI : MonoBehaviour
{
    public static NotebookUI Instance { get; private set; }

    [SerializeField] private GameObject notebookPanel;
    [SerializeField] private TMP_Text notebookText;

    public bool IsOpen =>
        notebookPanel != null && notebookPanel.activeSelf;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HideNotebook();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (IsOpen)
            {
                HideNotebook();
            }
            else
            {
                // Do not open the notebook over dialogue.
                if (DialogueUI.Instance != null &&
                    DialogueUI.Instance.IsOpen)
                {
                    return;
                }

                ShowNotebook();
            }

            return;
        }

        if (IsOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            HideNotebook();
        }
    }

    public void ShowNotebook()
    {
        if (notebookPanel == null || notebookText == null)
        {
            Debug.LogError(
                "NotebookUI references are not connected."
            );

            return;
        }

        RefreshNotebook();
        notebookPanel.SetActive(true);
    }

    public void HideNotebook()
    {
        if (notebookPanel != null)
        {
            notebookPanel.SetActive(false);
        }
    }

    private void RefreshNotebook()
    {
        StringBuilder display = new StringBuilder();

        display.AppendLine("<b>CASE NOTES</b>");
        display.AppendLine();

        if (EvidenceManager.Instance == null)
        {
            display.AppendLine("Evidence system not found.");
            notebookText.text = display.ToString();
            return;
        }

        var evidenceList =
            EvidenceManager.Instance.CollectedEvidence;

        display.AppendLine(
            $"Evidence collected: {evidenceList.Count}"
        );

        display.AppendLine();

        if (evidenceList.Count == 0)
        {
            display.AppendLine("No evidence collected.");
        }
        else
        {
            for (int i = 0; i < evidenceList.Count; i++)
            {
                EvidenceData evidence = evidenceList[i];

                if (evidence == null)
                    continue;

                display.AppendLine(
                    $"<b>{i + 1}. {evidence.Title}</b>"
                );

                display.AppendLine(evidence.Description);
                display.AppendLine();
            }
        }

        notebookText.text = display.ToString();
    }
}