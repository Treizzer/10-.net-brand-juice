namespace Backend.DTOs {

    public class JuiceDto {

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int BrandId { get; set; }

        public decimal Milliliter { get; set; }

    }

}
