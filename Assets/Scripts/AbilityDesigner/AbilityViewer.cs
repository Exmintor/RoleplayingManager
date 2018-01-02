using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

/// <summary>
/// This should be attached to a Panel that holds AbilityDesignerItem prefabs.
/// </summary>
public class AbilityViewer : MonoBehaviour
{
    [Tooltip("Prefab of the AbilityDesignerItem to load")]
    [SerializeField]
    private GameObject abilityPrefab;
    [Tooltip("Panel to load the Ability prefabs into")]
    [SerializeField]
    private Transform abilityListPanel;

    // Hold a list to all of the prefabs so that it can be refreshed when it changes.
    protected List<GameObject> prefabList;

    // Event that is invoked when the Current Selected Ability is changed.
    public static event Action<AbilityDesignerItem> OnSelectedPrefabChanged;
    // Holds a reference to the most recently selected Ability
    private Ability currentSelected;
    public Ability CurrentSelected
    {
        get
        {
            return currentSelected;
        }
        private set
        {
            currentSelected = value;
        }
    }

    // A list of all the Abilities
    public AbilityList Abilities { get; set; }

    // Use this for initialization
    void Start ()
    {
        prefabList = new List<GameObject>();
        // Load the Ability list from a default Json path
        // TODO: Load it from a chosen path instead of a default one
        Abilities = new AbilityList();
        InitializeMenuItems();
	}

    /// <summary>
    /// Refresh the menu items when there is an item added or removed from the list.
    /// This is achieved by deleting the whole list and then reloading it.
    /// TODO: Find a more efficcient way to reload the items?
    /// </summary>
    public void RefreshMenuItems()
    {
        DeleteMenuItems();
        InitializeMenuItems();
    }
    /// <summary>
    /// Delete all the menu item Prefabs in the list.
    /// </summary>
    private void DeleteMenuItems()
    {
        foreach(GameObject prefab in prefabList)
        {
            Destroy(prefab);
        }
        prefabList.Clear();
    }
    /// <summary>
    /// Initialize the Panel with all of the Abilities by creating new Prefabs for each Ability in the list.
    /// </summary>
    protected virtual void InitializeMenuItems()
    {
        foreach(Ability abil in Abilities.Abilities)
        {
            // Create a new Prefab.
            GameObject newAbility = Instantiate(abilityPrefab, abilityListPanel) as GameObject;
            // Set the prefab's Toggle group to the Group in the AbilityList Panel.
            // This way, each Ability belongs to th same Toggle Group.
            newAbility.GetComponent<Toggle>().group = abilityListPanel.GetComponent<ToggleGroup>();
            // Create a reference to the current Ability in the new Prefab
            newAbility.GetComponent<AbilityDesignerItem>().AbilityRepresented = abil;
            // Refresh the Text and Image of each Prefab, using the name and image from the current Ability
            newAbility.GetComponentInChildren<Text>().text = abil.Name;
            if(abil.AbilityImage != null)
            {
                Texture2D image = abil.AbilityImage;
                newAbility.GetComponent<Image>().sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
            }
            // Add the new Prefab to a list, to keep track of it (so that it can be deleted when the list is refreshed)
            prefabList.Add(newAbility);
        }
    }

    /// <summary>
    /// This method should be called by the AbilityDesignerItem when it is clicked.
    /// It updates the reference to the Currently Selected Ability.
    /// </summary>
    /// <param name="currentPrefab">The prefab that was just selected</param>
    public void UpdateDesignerSelected(AbilityDesignerItem currentPrefab)
    {
        // Update the reference
        CurrentSelected = currentPrefab.AbilityRepresented;
        // Broadcast the change
        if(OnSelectedPrefabChanged != null)
        {
            OnSelectedPrefabChanged.Invoke(currentPrefab);
        }
    }

    /// <summary>
    /// Adds a new Generic Ability to the Ability List.
    /// Then it saves the list and refreshes the Panel.
    /// </summary>
    public void AddNewAbility()
    {
        GenericAbility ability = new GenericAbility();
        Abilities.Add(ability);
        Save();
        RefreshMenuItems();
    }
    /// <summary>
    /// Removes the currently selected Ability from the Ability List.
    /// Then it saves the list and refreshed the Panel.
    /// </summary>
    public void RemoveAbility()
    {
        Abilities.Abilities.Remove(CurrentSelected);
        Save();
        RefreshMenuItems();
    }

    /// <summary>
    /// Save the viewer's Ability List
    /// </summary>
    public void Save()
    {
        Abilities.Save();
    }
}
