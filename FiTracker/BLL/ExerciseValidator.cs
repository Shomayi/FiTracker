using System.Collections.Generic;
using FiTracker.ViewModels;

namespace FiTracker.BLL
{
    public static class ExerciseValidator
    {
        public static List<string> Validate(ExerciseViewModel model)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(model.Name))
                errors.Add("Exercise name can't be empty");

            if (model.Sets <= 0)
                errors.Add("Amount of sets can't be 0");

            if (model.Reps <= 0)
                errors.Add("Amount of reps can't be 0");

            if (model.Weight < 0)
                errors.Add("Weight has to be between 0 and 1000");

            return errors;
        }
    }
}
