using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class TestScript : MonoBehaviour
{
    // Use this for initialization
    void Start ()
    {
        GenericAbility ability = new GenericAbility();
        GenericChanneled channeled = new GenericChanneled();

        string genericPath = "Assets/SaveTest/Generic.json";
        string channeledPath = "Assets/SaveTest/Channeled.json";

        ability.Save(genericPath);
        channeled.Save(channeledPath);

        GenericAbility newAbility = new GenericAbility();
        GenericChanneled newChanneled = new GenericChanneled();

        newAbility = newAbility.Load<GenericAbility>(genericPath);
        newChanneled = newChanneled.Load<GenericChanneled>(channeledPath);

        string jsonPath = "Assets/SaveTest/AbilityList.json";
        List<Ability> jsonList = new List<Ability>();
        jsonList.Add(ability);
        jsonList.Add(channeled);

        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.Objects;
        settings.Formatting = Formatting.Indented;

        string jsonListString = JsonConvert.SerializeObject(jsonList, settings);
        File.WriteAllText(jsonPath, jsonListString);

        string jsonText = File.ReadAllText(jsonPath);
        List<Ability> listToLoad = JsonConvert.DeserializeObject<List<Ability>>(jsonText, settings);
        foreach(Ability listAbility in listToLoad)
        {
            listAbility.RefreshImage();
        }
    }
	
}
