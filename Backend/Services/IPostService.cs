using Backend.DTOs;

namespace Backend.Services {

    public interface IPostService {

        Task<IEnumerable<PostDto>?> get ();

    }

}
