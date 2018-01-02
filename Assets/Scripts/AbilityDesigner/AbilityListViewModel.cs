using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityWeld.Binding;

/// <summary>
/// This should be attached to a Panel which holds an Ability Viewer and another Panel with UI elements.
/// </summary>
[Binding]
public class AbilityListViewModel : MonoBehaviour, INotifyPropertyChanged
{
    [Tooltip("Reference to a Panel that lists Abilities")]
    [SerializeField]
    private AbilityViewer viewer;
    // Reference to the currently selected AbilityDesignerItem
    private AbilityDesignerItem currentPrefabSelected;
    // Reference to the currently selected Ability.
    // This is updated when the currently selected Ability is updated in the Ability Viewer
    private Ability CurrentSelected; // Model

    // ID Property for binding to the UI
    private int idText;
    [Binding]
    public int IDText
    {
        get
        {
            return idText;
        }
        set
        {
            idText = value;
            // Update the model and save the changes.
            viewer.CurrentSelected.ID = idText;
            viewer.Save();
            OnPropertyChanged("IDText");
        }
    }

    // Name property for binding to the UI
    private string nameText;
    [Binding]
    public string NameText
    {
        get
        {
            return nameText;
        }
        set
        {
            nameText = value;
            // Update the model and save the changes.
            viewer.CurrentSelected.Name = nameText;
            viewer.Save();
            // Refresh the Prefab name in the UI
            currentPrefabSelected.RefreshName();
            OnPropertyChanged("NameText");
        }
    }

    // Image Path property for binding to the UI
    private string imagePathText;
    [Binding]
    public string ImagePathText
    {
        get
        {
            return imagePathText;
        }
        set
        {
            imagePathText = value;
            // Update the model and save the changes.
            viewer.CurrentSelected.ImageFilePath = imagePathText;
            viewer.Save();
            // Refresh the Prefab image in the UI
            currentPrefabSelected.RefreshImage();
            OnPropertyChanged("ImagePathText");
        }
    }

    // Cooldown property for binding to the UI
    private float cooldownText;
    [Binding]
    public float CooldownText
    {
        get
        {
            return cooldownText;
        }
        set
        {
            cooldownText = value;
            // Update the model and save the changes
            viewer.CurrentSelected.Cooldown = cooldownText;
            viewer.Save();
            OnPropertyChanged("CooldownText");
        }
    }

    // Description property for binding to the UI
    private string descriptionText;
    [Binding]
    public string DescriptionText
    {
        get
        {
            return descriptionText;
        }
        set
        {
            descriptionText = value;
            // Update the model and save the changes
            viewer.CurrentSelected.Description = descriptionText;
            viewer.Save();
            OnPropertyChanged("DescriptionText");
        }
    }

    // Use this for initialization
    void Start ()
    {
        // Subscribe to the Ability Viewer's event to update the currently selected Prefab and Ability.
        AbilityViewer.OnSelectedPrefabChanged += OnCurrentSelectedChanged;
	}

    // Property changed event handler for updating the UI
    public event PropertyChangedEventHandler PropertyChanged;
    /// <summary>
    /// Property changed event that is invoked when a property is changes
    /// </summary>
    /// <param name="propertyName">Name of the property changed</param>
    private void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Updates all the properties in the viewmodel based on values in the model (CurrentSelected Ability)
    /// </summary>
    private void UpdateAllProperties()
    {
        IDText = CurrentSelected.ID;
        NameText = CurrentSelected.Name;
        ImagePathText = CurrentSelected.ImageFilePath;
        CooldownText = CurrentSelected.Cooldown;
        DescriptionText = CurrentSelected.Description;
    }

    /// <summary>
    /// Add a new Ability to the list. Binds to a button in the UI.
    /// </summary>
    [Binding]
    public void AddNewAbility()
    {
        // Delegate the work to the viewer.
        viewer.AddNewAbility();
    }
    /// <summary>
    /// Remove selected Ability from the list. Binds to a button in the UI.
    /// </summary>
    [Binding]
    public void RemoveAbility()
    {
        // Delegate the work to the viewer.
        viewer.RemoveAbility();
    }

    /// <summary>
    /// This method should be subscribed to the OnPrefabSelecdedChanged in the Ability Viewer.
    /// It updates the references to the selected Prefab, and the Current Selected Ability (Model).
    /// </summary>
    /// <param name="newPrefab">The New Prefab Selected</param>
    private void OnCurrentSelectedChanged(AbilityDesignerItem newPrefab)
    {
        // Update the Prefab selected
        currentPrefabSelected = newPrefab;
        // Update the Model based on which Prefab was selected.
        CurrentSelected = currentPrefabSelected.AbilityRepresented;
        // Make sure to update all properties so that the UI reflects the new changes.
        UpdateAllProperties();
    }
}
