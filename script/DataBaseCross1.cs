using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System;
using Trafic.System;

public class DataBaseCross1 : MonoBehaviour
{

    MongoClient client = new MongoClient("mongodb+srv://bill:bill1@cluster0.peuyv.mongodb.net/diplomatikiDB?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

    Junction[] ju;
    void Start()
    {
        database = client.GetDatabase("diplomatikiDB");
        collection = database.GetCollection<BsonDocument>("diplomatikiCrossroad");

        ju = FindObjectsOfType<Junction>();
        foreach (var road2 in ju)
        {
            if (road2.car == 0)
            {
               if (road2.ID != "topali me spiridi" && road2.ID != "28h Oktobriou me spiridi" && road2.ID != "taki ikonomaki me spiridi")
                SaveCrossToDataBase(road2.ID, road2.phaseIntervalNorth, road2.phaseIntervalWest);
            }
        }

    }
    public async void SaveCrossToDataBase(string ID, int phaseIntervalShort, int phaseIntervalLong)
    {

        var document = new BsonDocument { { "Crossroad", ID }, { "NorthLight", phaseIntervalShort }, { "WestLight", phaseIntervalLong } };
        await collection.InsertOneAsync(document);

    }
    public async Task<List<LightScore>> GetCrossFromDataBase()
    {
        var allLightsTask = collection.FindAsync(new BsonDocument());
        var lightsAwaited = await allLightsTask;

        List<LightScore> lightcores = new List<LightScore>();
        foreach (var light in lightsAwaited.ToList())
        {
            lightcores.Add(DeserializeLights(light.ToString()));
        }
        return lightcores;
    }



    private LightScore DeserializeLights(string rawJson)
    {
        var lightScore = new LightScore();


        //"{ \"_id\" : ObjectId(\"61f820b5953a9309c88077f8\"), \"Crossroad\" : \"kartalh me 28ohs\", \"LongLight\" : 8, \"ShortLight\" : 2 }"           
        //"Crossroad\" : \"kartalh me 28ohs\", \"ShortLight\" : 8, \"LongLight\" : 2 }"
        //"ight\" : 8, \"ShortLight\" : 2 }"
        var stringWithoutIDlights = rawJson.Substring(rawJson.IndexOf("),") + 18);
        var usernamelight = stringWithoutIDlights.Substring(0, stringWithoutIDlights.IndexOf("\""));

        var scorelightShorttemp = stringWithoutIDlights.Substring(stringWithoutIDlights.IndexOf("NorthLight") + 14);
        var scorelightShort = scorelightShorttemp.Substring(0, scorelightShorttemp.IndexOf(","));
        lightScore.phaseIntervalShort1 = Convert.ToInt32(scorelightShort);


        var scorelightLongtemp = stringWithoutIDlights.Substring(stringWithoutIDlights.IndexOf("WestLight"));
        var scorelightLong = scorelightLongtemp.Substring(scorelightLongtemp.IndexOf(":") + 2, scorelightLongtemp.IndexOf("}") - scorelightLongtemp.IndexOf(":") - 3);
        lightScore.phaseIntervalLong1 = Convert.ToInt32(scorelightLong);

        lightScore.ID = usernamelight;




        return lightScore;

    }
    
}
