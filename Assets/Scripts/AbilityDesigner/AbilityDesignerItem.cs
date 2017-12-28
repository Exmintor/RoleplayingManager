using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDesignerItem : MonoBehaviour
{
    private AbilityDesigner designer;

    public Ability AbilityRepresented { get; set; }

	// Use this for initialization
	void Start ()
    {
        designer = FindObjectOfType<AbilityDesigner>();
	}

    public void AbilityWasClicked()
    {
        designer.UpdateEditFields();
    }
}
