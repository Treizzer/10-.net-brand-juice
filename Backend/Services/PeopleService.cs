using Backend.Controllers;

// Business logic
namespace Backend.Services {
    public class PeopleService: IPeopleService {

        // Business rule
        public bool validate (People people) { 
            if (string.IsNullOrEmpty(people.Name)) {
                return false;
            }

            return true;
        }

    }
}
