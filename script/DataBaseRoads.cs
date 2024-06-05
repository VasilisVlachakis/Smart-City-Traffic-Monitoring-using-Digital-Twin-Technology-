using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using Trafic.System;
using System;

public class DataBaseRoads : MonoBehaviour
{

    MongoClient client = new MongoClient("mongodb+srv://bill:bill1@cluster0.peuyv.mongodb.net/diplomatikiDB?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

    Road[] ro12;
  
    void Start()
    {
        database = client.GetDatabase("diplomatikiDB");
        collection = database.GetCollection<BsonDocument>("diplomatiki");
        ro12 = FindObjectsOfType<Road>();
        foreach (var road in ro12) {
            if (road.car >0) {
        
            SaveScoreToDataBase(road.ID, road.car);
            }
        }

    }
    public async void SaveScoreToDataBase(string ID, int car)
    {
        var document = new BsonDocument { { ID, car } };
        await collection.InsertOneAsync(document);

    }
    public async Task<List<HighScore>> GetScoresFromDataBase()
    {
        var allScoresTask = collection.FindAsync(new BsonDocument());
        var scoresAwaited = await allScoresTask;

        List<HighScore> highscores = new List<HighScore>();
        foreach (var score in scoresAwaited.ToList())
        {
            highscores.Add(Deserialize(score.ToString()));
        }
        return highscores;
    }

    private HighScore Deserialize(string rawJson)
    {
        var highScore = new HighScore();

        // "{ \"_id\" : ObjectId(\"614a08d9953a930f5c0ef7d2\"), \"sdsd\" : 14 }"

        var stringWithoutID = rawJson.Substring(rawJson.IndexOf("),") + 4);
        var username = stringWithoutID.Substring(0, stringWithoutID.IndexOf(":") - 2);
        var score = stringWithoutID.Substring(stringWithoutID.IndexOf(":") + 2, stringWithoutID.IndexOf("}") - stringWithoutID.IndexOf(":") - 3);
        highScore.ID = username;
        highScore.car = Convert.ToInt32(score);

        
        return highScore;
    }
}
