

namespace Zadanie7.DTO;
    public partial class TripDto
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public int MaxPeople { get; set; }

        public ICollection<ClientDto> Clients { get; set; } = null!;

        public ICollection<CountryDto> Countries { get; set; } = null!;
    }