using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public abstract class Ability : IAbility
{
    public int ID { get; protected set; }

    public string Name { get; protected set; }

    public string ImageFilePath { get; protected set; }

    [JsonIgnore]
    public Texture2D AbilityImage { get; private set; }

    public string Description { get; protected set; }

    public float Cooldown { get; protected set; }

    public abstract void Cast();

    public virtual void Save(string filePath)
    {
        RefreshImage();
        string abilityJson = GetJsonString();
        File.WriteAllText(filePath, abilityJson);
    }

    public virtual T Load<T>(string filePath) where T:Ability
    {
        string abilityJson = File.ReadAllText(filePath);
        T newAbility = JsonConvert.DeserializeObject<T>(abilityJson);
        newAbility.RefreshImage();
        return newAbility;
    }

    public virtual string GetJsonString()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;
        settings.TypeNameHandling = TypeNameHandling.Objects;
        string abilityJson = JsonConvert.SerializeObject(this, settings);
        return abilityJson;
    }

    public void RefreshImage()
    {
        AbilityImage = Resources.Load(ImageFilePath) as Texture2D;
    }
}
