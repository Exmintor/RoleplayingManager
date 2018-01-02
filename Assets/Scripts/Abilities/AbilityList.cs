using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System;
using System.Reflection;

// An enum that should hold the name of each class of Ability that
// has been made. It allows for an easier lookup of Abilities.
// TODO: Needs to be updated with new Abilities as they are added.
public enum AbilityEnum { GenericAbility, GenericChanneled }

/// <summary>
/// A list of Abilities that can only be loaded from a Json file.
/// It allows the adding and removing of Abilities.
/// It holds a reference to the Path it was loaded from, and if there
/// are any changes made to this list, it updates the Json file it was
/// originally loaded from.
/// </summary>
public class AbilityList
{
    // The list of current Abilities. Ideally there should only be one
    // instance of each Ability in this list.
    public List<Ability> Abilities { get; private set; }
    // The Json File Path
    public string FilePath { get; private set; }

    // A Default file path is used.
    // TODO: Get rid of the default file path, or change it to reflect a built application.
    public AbilityList() : this("Assets/Json/AbilityList.json")
    {

    }

    // A more specific file path can be used when necessary
    public AbilityList(string filePath)
    {
        FilePath = filePath;
        Abilities = Load(FilePath);
    }

    /// <summary>
    /// Add an Ability to the list.
    /// </summary>
    /// <param name="ability">The Ability to add to the list</param>
    public void Add(Ability ability)
    {
        Abilities.Add(ability);
        // Make sure the Abilities are ordered by their ID's
        Abilities = Abilities.OrderBy(abil => abil.ID).ToList();
        // Save the changes made.
        Save();
    }
    /// <summary>
    /// Remove an Ability from the list.
    /// </summary>
    /// <param name="index">The index of the Ability to remove</param>
    /// <returns></returns>
    public Ability Remove(int index)
    {
        if (Abilities.Count > index)
        {
            Ability toReturn = Abilities.ElementAt(index);
            Abilities.RemoveAt(index);
            // Save the changes made.
            Save();
            return toReturn;
        }
        else
        {
            // Return null if the index is out of range.
            return null;
        }
    }

    /// <summary>
    /// Looks in the list for a specific Ability and returns a copy of that instance.
    /// TODO: Make sure that the returned Ability is a copy and not a reference.
    /// </summary>
    /// <param name="ability">Enum name of the Ability to find</param>
    /// <returns></returns>
    public Ability GetAbilityFromList(AbilityEnum ability)
    {
        // Call the method that gets the Ability from name after looking up the name
        // from the provided enum.
        Ability toReturn = GetAbilityFromList(GetAbilityNameByEnum(ability));
        return toReturn;
    }
    /// <summary>
    /// Looks in the list for a specific Ability and returns a copy of that instance.
    /// TODO: Make sure that the returned Ability is a copy and not a reference.
    /// </summary>
    /// <param name="name">The name of the Ability to find</param>
    /// <returns></returns>
    public Ability GetAbilityFromList(string name)
    {
        var ability = from abil in Abilities where abil.Name == name select abil;
        // TODO: Probably throws an exception when there are Abilities that share the same name.
        // Make sure that exception is handled.
        // TODO: This will no doubt throw a NullReferenceException when an Ability is not found. Handle it.
        Ability thisAbility = ability.SingleOrDefault();
        string abilityJson = thisAbility.GetJsonString();

        return AbilityFromJson(abilityJson);
    }
    /// <summary>
    /// Looks in the list for a specific Ability and returns a copy of that instance.
    /// TODO: Make sure that the returned Ability is a copy and not a reference.
    /// </summary>
    /// <param name="ID">The ID of the Ability to find</param>
    /// <returns></returns>
    public Ability GetAbilityFromList(int ID)
    {
        var ability = from abil in Abilities where abil.ID == ID select abil;
        // TODO: Probably throws an exception when there are Abilities that share the same name.
        // Make sure that exception is handled.
        // TODO: This will no doubt throw a NullReferenceException when an Ability is not found. Handle it.
        Ability thisAbility = ability.SingleOrDefault();
        string abilityJson = thisAbility.GetJsonString();

        return AbilityFromJson(abilityJson);
    }
    /// <summary>
    /// Takes in a Json string and convert it to an Ability.
    /// </summary>
    /// <param name="jsonString">The Ability in the form of a Json string.</param>
    /// <returns></returns>
    private Ability AbilityFromJson(string jsonString)
    {
        // TODO: Move common Json functionality to a different(Utility) class. 
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Objects;
        settings.Formatting = Formatting.Indented;
        Ability newAbility = JsonConvert.DeserializeObject<Ability>(jsonString, settings);
        return newAbility;
    }

    /// <summary>
    /// Loads the Ability List from the FilePath specified when the Ability List was created.
    /// </summary>
    /// <returns></returns>
    public virtual List<Ability> Load()
    {
        List<Ability> abilities = Load(FilePath);
        return abilities;
    }
    /// <summary>
    /// Loads an Ability List from a different File Path.
    /// TODO: Decide if this should be a public functionality.
    /// If so, it should update the FilePath property.
    /// </summary>
    /// <param name="filePath">The File Path to load from</param>
    /// <returns></returns>
    protected virtual List<Ability> Load(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Objects;
        settings.Formatting = Formatting.Indented;
        List<Ability> abilities = JsonConvert.DeserializeObject<List<Ability>>(jsonString, settings);
        return abilities;
    }

    /// <summary>
    /// Saves the Ability List to the FilePath specified when the Ability List was created.
    /// </summary>
    public virtual void Save()
    {
        Save(FilePath);
    }
    /// <summary>
    /// Saves the Ability List to a different File Path.
    /// TODO: Decide if this should be a public functionality.
    /// If so, it should update the FilePath property.
    /// </summary>
    /// <param name="filePath">The File Path to save to</param>
    protected virtual void Save(string filePath)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Objects;
        settings.Formatting = Formatting.Indented;
        string jsonString = JsonConvert.SerializeObject(Abilities, settings);
        File.WriteAllText(filePath, jsonString);
    }

    /// <summary>
    /// Converts an Enum to a name for looking up.
    /// If the name is not found, it returns Generic Ability.
    /// </summary>
    /// <param name="ability"></param>
    /// <returns></returns>
    private string GetAbilityNameByEnum(AbilityEnum ability)
    {
        switch(ability)
        {
            case AbilityEnum.GenericAbility:
                return "GenericAbility";
            case AbilityEnum.GenericChanneled:
                return "GenericChanneled";
            default:
                return "GenericAbility";
        }
    }
}
