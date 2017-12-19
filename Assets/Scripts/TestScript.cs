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

        // Quite possibly the best solution is to use Json.net for all serialization
        // TODO: Refactor the classes in order to reflect this
        //string jsonPath = "Assets/SaveTest/AbilityList.json";
        //List<Ability> jsonList = new List<Ability>();
        //jsonList.Add(ability);
        //jsonList.Add(channeled);

        //string jsonListString = JsonConvert.SerializeObject(jsonList);
        //File.WriteAllText(jsonPath, jsonListString);

        //List<string> jsonList = new List<string>();
        //string listPath = "Assets/SaveTest/AbilityList.json";
        //jsonList.Add(ability.GetJsonString());
        //jsonList.Add(channeled.GetJsonString());
        //string completeJsonString = "";

        //for (int i = 0; i < jsonList.Count; i++)
        //{
        //    if (i != jsonList.Count - 1)
        //    {
        //        completeJsonString += jsonList[i] + ",\n";
        //    }
        //    else
        //    {
        //        completeJsonString += jsonList[i];
        //    }
        //}
        //File.WriteAllText(listPath, completeJsonString);
    }
	
}
