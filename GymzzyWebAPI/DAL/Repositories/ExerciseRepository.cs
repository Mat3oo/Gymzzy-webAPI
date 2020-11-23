using GymzzyWebAPI.DAL.Repositories.Interfaces;
using GymzzyWebAPI.Models;
using System;
using System.Linq;

namespace GymzzyWebAPI.DAL.Repositories
{
    public class ExerciseRepository : GenericRepository<Exercise, WorkoutContext>, IExerciseRepository
    {
        public ExerciseRepository(WorkoutContext context) : base(context)
        {
        }

        public Exercise GetByName(string exerciseName)
        {
            var exercise = GetAll().FirstOrDefault(p => p.Name == exerciseName);
            if (exercise is default(Exercise))
            {
                return null;
            }

            return exercise;
        }
    }
}
