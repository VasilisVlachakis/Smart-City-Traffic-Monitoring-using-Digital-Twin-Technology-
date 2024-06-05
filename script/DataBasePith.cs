using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System;
using Trafic.System;
public class DataBasePith : MonoBehaviour
{

    MongoClient client = new MongoClient("mongodb+srv://bill:bill1@cluster0.peuyv.mongodb.net/diplomatikiDB?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

    NavConnection[] na;
    // Start is called before the first frame update
    void Start()
    {
        database = client.GetDatabase("diplomatikiDB");
        collection = database.GetCollection<BsonDocument>("diplomatikiPithanothtes");

        na = FindObjectsOfType<NavConnection>();
        foreach (var road3 in na)
        {
            if (road3.ID!="") 
           SavePithanothtesToDataBase(road3.ID, road3.Left, road3.Right, road3.Straight);
        }
        

    }

    public async void SavePithanothtesToDataBase(string ID, int Left, int Right, int Straight)
    {
        var document = new BsonDocument { { "Point", ID }, { "Left", Left }, { "Right", Right }, { "Straight", Straight } };
        await collection.InsertOneAsync(document);

    }
    public async Task<List<PithanotitesScore1>> GetPithanothtesFromDataBase()
    {

        var allPithanotitesTask = collection.FindAsync(new BsonDocument());
        var PithanotitesAwaited = await allPithanotitesTask;

        List<PithanotitesScore1> pithcores = new List<PithanotitesScore1>();
        foreach (var pith in PithanotitesAwaited.ToList())
        {
            pithcores.Add(Deserialize(pith.ToString()));
        }

        return pithcores;
    }



    public PithanotitesScore1 Deserialize(string rawJson)
    {

        var pithaScore = new PithanotitesScore1();
        //"{ \"_id\" : ObjectId(\"61fe757c953a9320e0c12235\"), \"Point\" : \"kartalh me sokratous north right\", \"Left\" : 30, \"Right\" : 30, \"Straight\" : 40 }"
        var noid = rawJson.Substring(rawJson.IndexOf(": \"") + 3);
        var noid2 = noid.Substring(0, noid.IndexOf("\","));
        pithaScore.ID2 = noid2;

        var left = rawJson.Substring(rawJson.IndexOf("Left")+8);
        var left2 = left.Substring(0, left.IndexOf(","));
        pithaScore.left2 = Convert.ToInt32(left2);

        var right = rawJson.Substring(rawJson.IndexOf("Right")+9);
        var right2 = right.Substring(0, right.IndexOf(","));
        pithaScore.right2 = Convert.ToInt32(right2);

        var straight = rawJson.Substring(rawJson.IndexOf("Straight")+12);
        var straight2 = straight.Substring(0, straight.IndexOf("}")-1);
        pithaScore.straight2 = Convert.ToInt32(straight2);

        return pithaScore;
    }
}
