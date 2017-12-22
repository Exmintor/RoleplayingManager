using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

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
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Objects;
        settings.Formatting = Formatting.Indented;
        List<Ability> abilities = JsonConvert.DeserializeObject<List<Ability>>(filePath, settings);
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
}
