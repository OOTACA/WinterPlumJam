using UnityEngine;
using SonicBloom.Koreo;
using System.Collections.Generic;

public class NoteSpawner2D : MonoBehaviour
{
    public List<GameObject> notePrefabs; // 0 = tap, 1 = hold
    public string eventID = "TopLeft";
    private Vector2 requiredDirection;

    void Start()
    {
        SetRequiredDirection();
        Koreographer.Instance.RegisterForEvents(eventID, OnKoreographyEvent);
    }

    void OnDestroy()
    {
        if (Koreographer.Instance != null)
            Koreographer.Instance.UnregisterForAllEvents(this);
    }

    void OnKoreographyEvent(KoreographyEvent evt)
    {
        int index = evt.IsOneOff() ? 0 : 1;
        GameObject prefab = notePrefabs[index];

        GameObject note = Instantiate(prefab, transform.position, Quaternion.identity);

        if (!evt.IsOneOff())
        {
            NoteHoldChecker checker = note.GetComponent<NoteHoldChecker>();
            if (checker != null)
            {
                checker.requiredDirection = requiredDirection;
            }
        }
    }

    void SetRequiredDirection()
    {
        string name = gameObject.name.ToLower();

        if (name.Contains("topleft")) requiredDirection = new Vector2(-1, 1);
        else if (name.Contains("topright")) requiredDirection = new Vector2(1, 1);
        else if (name.Contains("bottomleft")) requiredDirection = new Vector2(-1, -1);
        else if (name.Contains("bottomright")) requiredDirection = new Vector2(1, -1);
        else if (name.Contains("top")) requiredDirection = new Vector2(0, 1);
        else if (name.Contains("bottom")) requiredDirection = new Vector2(0, -1);
        else if (name.Contains("left")) requiredDirection = new Vector2(-1, 0);
        else if (name.Contains("right")) requiredDirection = new Vector2(1, 0);
        else requiredDirection = Vector2.zero;
    }
}
