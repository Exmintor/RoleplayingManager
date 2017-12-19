using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

[Serializable]
public abstract class Ability : IAbility
{
    public int ID { get; protected set; }

    public string Name { get; protected set; }

    protected string imageFilePath;
    public Texture2D AbilityImage { get; private set; }

    public string Description { get; protected set; }

    public float Cooldown { get; protected set; }

    public abstract void Cast();

    public virtual void Save(string filePath)
    {
        LoadImageFromPath();
        string abilityJson = JsonConvert.SerializeObject(this);
        File.WriteAllText(filePath, abilityJson);

        //FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);
        //BinaryFormatter formatter = new BinaryFormatter();
        //formatter.Serialize(stream, abilityJson);
        //stream.Close();
    }

    // TODO: Static or virtual?
    public virtual T Load<T>(string filePath) where T:Ability
    {
        string abilityJson = File.ReadAllText(filePath);
        T newAbility = JsonConvert.DeserializeObject<T>(abilityJson);
        newAbility.LoadImageFromPath();
        return newAbility;

        //FileStream stream = new FileStream(filePath, FileMode.Open);
        //BinaryFormatter formatter = new BinaryFormatter();
        //string abilityJson = (string)formatter.Deserialize(stream);
        //stream.Close();
    }

    public string GetJsonString()
    {
        string abilityJson = JsonConvert.SerializeObject(this);
        return abilityJson;
    }

    private void LoadImageFromPath()
    {
        AbilityImage = Resources.Load(imageFilePath) as Texture2D;
    }
}
