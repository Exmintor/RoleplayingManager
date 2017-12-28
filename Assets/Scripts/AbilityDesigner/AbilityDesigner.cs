using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDesigner : MonoBehaviour
{
    [SerializeField]
    private GameObject abilityPrefab;
    [SerializeField]
    private Transform abilityListPanel;

    private AbilityList list;
	// Use this for initialization
	void Start ()
    {
        list = new AbilityList();
        InitializeMenuItems();
	}

    private void InitializeMenuItems()
    {
        foreach(Ability abil in list.Abilities)
        {
            GameObject newAbility = Instantiate(abilityPrefab, abilityListPanel) as GameObject;
            newAbility.GetComponent<Toggle>().group = abilityListPanel.GetComponent<ToggleGroup>();
            newAbility.GetComponentInChildren<Text>().text = abil.Name;
            newAbility.GetComponent<AbilityDesignerItem>().AbilityRepresented = abil;
        }
    }

    public void UpdateEditFields()
    {

    }
}
