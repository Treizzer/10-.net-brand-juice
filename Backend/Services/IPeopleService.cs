using Backend.Controllers;

// Business logic
namespace Backend.Services {
    public interface IPeopleService {

        // Implement business rules
        bool validate (People people);

    }
}
