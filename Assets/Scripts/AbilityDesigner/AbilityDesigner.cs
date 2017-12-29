using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

[Binding]
public class AbilityDesigner : MonoBehaviour, INotifyPropertyChanged
{
    [SerializeField]
    private GameObject abilityPrefab;
    [SerializeField]
    private Transform abilityListPanel;

    private AbilityList list;
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
            UpdateAllProperties();
        }
    }

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
            if(CurrentSelected.ID != idText)
            {
                CurrentSelected.ID = idText;
                list.Save();
            }
            OnPropertyChanged("IDText");
        }
    }
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
            if(CurrentSelected.Name != nameText)
            {
                CurrentSelected.Name = nameText;
                list.Save();
            }
            OnPropertyChanged("NameText");
        }
    }
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
            if(CurrentSelected.ImageFilePath != imagePathText)
            {
                CurrentSelected.ImageFilePath = imagePathText;
                list.Save();
            }
            OnPropertyChanged("ImagePathText");
        }
    }
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
            if(CurrentSelected.Cooldown != cooldownText)
            {
                CurrentSelected.Cooldown = cooldownText;
                list.Save();
            }
            OnPropertyChanged("CooldownText");
        }
    }
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
            if(CurrentSelected.Description != descriptionText)
            {
                CurrentSelected.Description = descriptionText;
                list.Save();
            }
            OnPropertyChanged("DescriptionText");
        }
    }

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

    public void UpdateDesignerSelected(Ability ability)
    {
        CurrentSelected = ability;
    }

    private void UpdateAllProperties()
    {
        IDText = CurrentSelected.ID;
        NameText = CurrentSelected.Name;
        ImagePathText = CurrentSelected.ImageFilePath;
        CooldownText = CurrentSelected.Cooldown;
        DescriptionText = CurrentSelected.Description;
    }
    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        if(PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
