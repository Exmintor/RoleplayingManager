using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public enum AbilityEnum { GenericAbility, GenericChanneled }
public class AbilityList
{
    public List<Ability> Abilities { get; private set; }
    public string FilePath { get; private set; }

    public AbilityList() : this("Assets/SaveTest/AbilityList.json")
    {

    }

    public AbilityList(string filePath)
    {
        FilePath = filePath;
        Abilities = Load(FilePath);
    }

    public List<Ability> Load()
    {
        List<Ability> abilities = Load(FilePath);
        return abilities;
    }
    public List<Ability> Load(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Objects;
        settings.Formatting = Formatting.Indented;
        List<Ability> abilities = JsonConvert.DeserializeObject<List<Ability>>(jsonString, settings);
        foreach (Ability instance in abilities)
        {
            instance.RefreshImage();
        }
        return abilities;
    }

    private void Save(string filePath)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Objects;
        settings.Formatting = Formatting.Indented;
        string jsonString = JsonConvert.SerializeObject(Abilities, settings);
        File.WriteAllText(filePath, jsonString);
    }

    public void Add(Ability ability)
    {
        Abilities.Add(ability);
        Abilities = (List<Ability>)Abilities.OrderBy(abil => abil.ID);
        Save(FilePath);
    }
    public Ability Remove(int index)
    {
        if(Abilities.Count > index)
        {
            Ability toReturn = Abilities.ElementAt(index);
            Abilities.RemoveAt(index);
            Save(FilePath);
            return toReturn;
        }
        else
        {
            return null;
        }
    }

    public Ability GetAbilityFromList(AbilityEnum ability)
    {
        Ability toReturn = GetAbilityFromList(GetAbilityNameByEnum(ability));
        return toReturn;
    }
    public Ability GetAbilityFromList(string name)
    {
        var ability = from abil in Abilities where abil.Name == name select abil;
        Ability thisAbility = ability.SingleOrDefault();
        string abilityJson = thisAbility.GetJsonString();

        return AbilityFromJson(abilityJson);
    }
    public Ability GetAbilityFromList(int ID)
    {
        var ability = from abil in Abilities where abil.ID == ID select abil;
        Ability thisAbility = ability.SingleOrDefault();
        string abilityJson = thisAbility.GetJsonString();

        return AbilityFromJson(abilityJson);
    }
    private Ability AbilityFromJson(string jsonString)
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Objects;
        settings.Formatting = Formatting.Indented;
        Ability newAbility = JsonConvert.DeserializeObject<Ability>(jsonString, settings);
        return newAbility;
    }

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

    //public Ability GetAbilityByName(string abilityName)
    //{
    //    switch(abilityName)
    //    {
    //        case "GenericAbility":
    //            return new GenericAbility();
    //        case "GenericChanneled":
    //            return new GenericChanneled();
    //        default:
    //            return new GenericAbility();
    //    }
    //}
}
