var bingoDb = conn.getDB("bingo");
// Append this document with seed data.

bingoDb.exercises.save([
    {
        "_id": ObjectId("5a37c78f977cd7e92241b9b9"),
        "Name": "Bench Press",
        "ShortName": "Bench",
        "LongName": "Flat Barbell Bench Press"
    },
    {
        "_id": ObjectId("5a37c78f977cd7e92241b9ba"),
        "Name": "Barbell Squat",
        "ShortName": "Squat",
        "LongName": "Barbell Back Squat"
    },
    {
        "_id": ObjectId("5a37c78f977cd7e92241b9bb"),
        "Name": "Barbell Deadlift",
        "ShortName": "Deadlift",
        "LongName": "Conventional Barbell Deadlift"
    }
]);

bingoDb.activations.save([
    {
        "_id" : ObjectId("5a89afff78523e2618d4bc89"),
        "ExerciseId" : "5a37c78f977cd7e92241b9b9",
        "MuscleId" : "5a89ae1378523e2618d4bc85",
        "RangeOfMotion" : 100.0,
        "ForceOutputPercentage" : 100.0,
        "RepetitionTempo" : {
            "Type" : "regular",
            "Duration" : 3.0,
            "ConcentricDuration" : 1.0,
            "EccentricDuration" : 1.5,
            "IsometricDuration" : 0.5
        },
        "Electromyography" : {
            "MeanEmg" : 193.0,
            "PeakEmg" : 289.0
        },
        "LactateProduction" : {
            "LactateProduction" : 22.0,
            "AerobicRespiration" : 13.0,
            "AnaerobicRespiration" : 75.0
        }
    },
    {
        "_id" : ObjectId("5a89b7f578523e2618d4bc8b"),
        "ExerciseId" : "5a37c78f977cd7e92241b9b9",
        "MuscleId" : "5a89b72478523e2618d4bc8a",
        "RangeOfMotion" : 100.0,
        "ForceOutputPercentage" : 100.0,
        "RepetitionTempo" : {
            "Type" : "regular",
            "Duration" : 3.0,
            "ConcentricDuration" : 1.0,
            "EccentricDuration" : 1.5,
            "IsometricDuration" : 0.5
        },
        "Electromyography" : {
            "MeanEmg" : 101.0,
            "PeakEmg" : 178.0
        },
        "LactateProduction" : {
            "LactateProduction" : 18.0,
            "AerobicRespiration" : 15.0,
            "AnaerobicRespiration" : 67.0
        }
    }
]);

bingoDb.muscles.save([
    {
        "_id" : ObjectId("5a89ae1378523e2618d4bc85"),
        "ShortName" : "Pecs",
        "Name" : "Pectorals",
        "LongName" : "Pectoralis Major",
        "GroupId" : "123456789012345678905432",
        "RegionId" : "123456789012345678909810"
    },
    {
        "_id" : ObjectId("5a89ae5578523e2618d4bc86"),
        "ShortName" : "Bi's",
        "Name" : "Biceps",
        "LongName" : "Biceps Brachialis",
        "GroupId" : "72304987150894721597893",
        "RegionId" : "87584901692645925672935"
    },
    {
        "_id" : ObjectId("5a89aea678523e2618d4bc87"),
        "ShortName" : "Lats",
        "Name" : "Latissimus",
        "LongName" : "Latissimus Dorsi",
        "GroupId" : "123456789012345678901827",
        "RegionId" : "123456789012345678901239"
    },
    {
        "_id" : ObjectId("5a89b72478523e2618d4bc8a"),
        "ShortName" : "Pecs",
        "Name" : "Lower Pecs",
        "LongName" : "Pectoralis Minor",
        "GroupId" : "123456783012215678901827",
        "RegionId" : "123456989012345672901439"
    }
]);

bingoDb.exercises.createIndex({ 'Name': 1 }, {
        name: "Index_Unique_Name",
        collation: { locale: "en", strength: 2 },
        unique: true
    });