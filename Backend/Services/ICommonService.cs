using Backend.DTOs;

namespace Backend.Services {

    public interface ICommonService<T, TI, TU> {

        List<string>? Errors { get; }

        Task<IEnumerable<T>> get ();

        Task<T?> getById (int id);

        Task<T> add (TI insertDto);

        Task<T?> update (int id, TU updateDto);

        Task<T?> delete (int id);

        bool validate (TI dto);

        bool validate (TU dto);

    }

}
