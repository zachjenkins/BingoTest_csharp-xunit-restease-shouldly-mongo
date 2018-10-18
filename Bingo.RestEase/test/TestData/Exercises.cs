using Bingo.RestEase.Models.Request;
using Bingo.RestEase.Models.Response;
using Bingo.RestEase.Support;
using System.Collections.Generic;

namespace Bingo.RestEase.Test.TestData
{
    public static class Exercises
    {
        public static IEnumerable<Exercise> GetAllExercisesForCollection()
        {
            return ContractExercises;
        }

        public static Exercise RandomContractExercise => new Exercise
        {
            Id = Utilities.GetRandomHexString(),
            Name = $"Random_{Utilities.GetRandomString()}",
            LongName = $"Random_{Utilities.GetRandomString()}",
            ShortName = $"Random_{Utilities.GetRandomString()}"
        };

        public static PostExerciseDto RandomPostExerciseDto => new PostExerciseDto
        {
            Name = $"Random_{Utilities.GetRandomString()}",
            LongName = $"Random_{Utilities.GetRandomString()}",
            ShortName = $"Random_{Utilities.GetRandomString()}"
        };

        public static Exercise ContractExercise => new Exercise
        {
            Id = "012345678901234567894542",
            Name = "Bench Press",
            LongName = "Barbell Bench Press",
            ShortName = "Bench"
        };

        public static Exercise ContractExercise2 => new Exercise
        {
            Id = "012345678901234567894578",
            Name = "Barbell Curls",
            LongName = "EZ Bar Curls",
            ShortName = "Curls"
        };

        public static List<Exercise> ContractExercises => new List<Exercise>
        {
            ContractExercise,
            ContractExercise2
        };

        public static PostExerciseDto ContractExercisePostDto => new PostExerciseDto
        {
            Name = "Tricep Extensions",
            LongName = "ZBar Tricep Pushdowns",
            ShortName = "Pushdowns"
        };

        public static Exercise ContractExercisePostDtoResponseMock => new Exercise
        {
            Id = "123456789012345678904578",
            Name = "Tricep Extensions",
            LongName = "ZBar Tricep Pushdowns",
            ShortName = "Pushdowns"
        };

        public static Exercise ExerciseWithoutId => new Exercise
        {
            Name = "Tricep Extensions",
            LongName = "ZBar Tricep Pushdowns",
            ShortName = "Tricep Extensions"
        };

        public static Exercise RandomizedExercise => new Exercise
        {
            ShortName = Utilities.GetRandomString(),
            Name = Utilities.GetRandomString(),
            LongName = Utilities.GetRandomString()
        };
    }
}
