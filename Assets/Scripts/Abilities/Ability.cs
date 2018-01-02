using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

/// <summary>
/// An abstract base class that provides some default behaviors for Abilities.
/// To create a new Ability, one should inherit from this Ability
/// (unless there is a class available that inherits a more specific interface).
/// </summary>
public abstract class Ability : IAbility
{
    public int ID { get; set; }

    public string Name { get; set; }

    // An abstraction that allows loading a Texture2D from a file name.
    private string imageFilePath;
    public string ImageFilePath
    {
        get
        {
            return imageFilePath;
        }
        set
        {
            imageFilePath = value;
            // Every time the path changes, make sure to refresh the reference.
            RefreshImage();
        }
    }

    // Don't save the Ability Image to Json.
    [JsonIgnore]
    public Texture2D AbilityImage { get; private set; }

    public string Description { get; set; }

    public float Cooldown { get; set; }

    /// <summary>
    /// Every Ability should define their own Cast() method to match their Description text.
    /// </summary>
    public abstract void Cast();

    /// <summary>
    /// Saves the Ability to a File Path by itself.
    /// </summary>
    /// <param name="filePath">The path to save the Ability to</param>
    public virtual void Save(string filePath)
    {
        string abilityJson = GetJsonString();
        File.WriteAllText(filePath, abilityJson);
    }

    /// <summary>
    /// Loads an Ability from a File Path. T should be the type of Ability the path contains.
    /// </summary>
    /// <typeparam name="T">T should be an Ability</typeparam>
    /// <param name="filePath">The path that contains the Ability to load</param>
    /// <returns></returns>
    public virtual T Load<T>(string filePath) where T:Ability
    {
        string abilityJson = File.ReadAllText(filePath);
        T newAbility = JsonConvert.DeserializeObject<T>(abilityJson);
        return newAbility;
    }

    /// <summary>
    /// Returns the Json string that represents this Ability.
    /// </summary>
    /// <returns></returns>
    public virtual string GetJsonString()
    {
        // TODO: Move common Json functionality to a different(Utility) class.
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;
        settings.TypeNameHandling = TypeNameHandling.Objects;
        string abilityJson = JsonConvert.SerializeObject(this, settings);
        return abilityJson;
    }

    /// <summary>
    /// This method refreshes the abilitie's image based on its ImageFilePath.
    /// If there is no image available, a Default image is used.
    /// TODO: Don't load from the Resources folder. Create a singleton to hold Art references.
    /// </summary>
    private void RefreshImage()
    {
        AbilityImage = Resources.Load(ImageFilePath) as Texture2D;
        if(AbilityImage == null)
        {
            AbilityImage = Resources.Load("Default") as Texture2D;
        }
    }
}
