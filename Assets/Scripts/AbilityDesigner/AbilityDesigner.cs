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

    private List<GameObject> prefabList;
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

    private AbilityDesignerItem currentPrefabSelected;

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
                currentPrefabSelected.RefreshName();
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
                currentPrefabSelected.RefreshImage();
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
        prefabList = new List<GameObject>();
        list = new AbilityList();
        InitializeMenuItems();
	}

    private void RefreshMenuItems()
    {
        DeleteMenuItems();
        InitializeMenuItems();
    }
    private void DeleteMenuItems()
    {
        foreach(GameObject prefab in prefabList)
        {
            Destroy(prefab);
        }
        prefabList.Clear();
    }
    private void InitializeMenuItems()
    {
        foreach(Ability abil in list.Abilities)
        {
            GameObject newAbility = Instantiate(abilityPrefab, abilityListPanel) as GameObject;
            newAbility.GetComponent<Toggle>().group = abilityListPanel.GetComponent<ToggleGroup>();
            newAbility.GetComponentInChildren<Text>().text = abil.Name;
            if(abil.AbilityImage != null)
            {
                Texture2D image = abil.AbilityImage;
                newAbility.GetComponent<Image>().sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
            }
            newAbility.GetComponent<AbilityDesignerItem>().AbilityRepresented = abil;
            prefabList.Add(newAbility);
        }
    }

    public void UpdateDesignerSelected(AbilityDesignerItem currentPrefab, Ability ability)
    {
        CurrentSelected = ability;
        currentPrefabSelected = currentPrefab;
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

    [Binding]
    public void AddNewAbility()
    {
        GenericAbility ability = new GenericAbility();
        list.Add(ability);
        list.Save();
        RefreshMenuItems();
    }
    [Binding]
    public void RemoveAbility()
    {
        list.Abilities.Remove(CurrentSelected);
        list.Save();
        RefreshMenuItems();
    }
}
