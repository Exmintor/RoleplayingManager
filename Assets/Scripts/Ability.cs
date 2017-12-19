using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class Ability : IAbility
{
    [SerializeField, HideInInspector]
    private int id;
    public int ID
    {
        get
        {
            return id;
        }
        protected set
        {
            id = value;
        }
    }

    [SerializeField, HideInInspector]
    private string name;
    public string Name
    {
        get
        {
            return name;
        }
        protected set
        {
            name = value;
        }
    }

    [SerializeField, HideInInspector]
    protected string imageFilePath;
    public Texture2D AbilityImage { get; private set; }

    [SerializeField, HideInInspector]
    private string description;
    public string Description
    {
        get
        {
            return description;
        }
        protected set
        {
            description = value;
        }
    }

    [SerializeField, HideInInspector]
    private float cooldown;
    public float Cooldown
    {
        get
        {
            return cooldown;
        }
        protected set
        {
            cooldown = value;
        }
    }

    public abstract void Cast();

    public virtual void Save(string filePath)
    {
        string abilityJson = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, abilityJson);
        //FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);
        //BinaryFormatter formatter = new BinaryFormatter();
        //formatter.Serialize(stream, abilityJson);
        //stream.Close();
    }

    public virtual T Load<T>(string filePath) where T:Ability
    {
        //FileStream stream = new FileStream(filePath, FileMode.Open);
        //BinaryFormatter formatter = new BinaryFormatter();
        //string abilityJson = (string)formatter.Deserialize(stream);
        //stream.Close();
        string abilityJson = File.ReadAllText(filePath);
        T newAbility = JsonUtility.FromJson<T>(abilityJson);
        newAbility.LoadImageFromPath();
        return newAbility;
    }

    public string GetJsonString()
    {
        string abilityJson = JsonUtility.ToJson(this);
        return abilityJson;
    }

    private void LoadImageFromPath()
    {
        AbilityImage = Resources.Load(imageFilePath) as Texture2D;
    }
}
