using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Helpers
{
    public class LostPetsQueryParams
    {
        const int _maxSize = 25;
        private int _size = 50;

        public int Size
        {
            get { return _size; }
            set
            {
                _size = Math.Min(_maxSize, value);
            }
        }

        public int Page { get; set; } = 1;
        public string? Name { get; set; }
        public PetStatusEnum? Status { get; set; }
        public string? LastLocation { get; set; }
        public string? Breed { get; set; }
        public string? NameSearchTerm { get; set; }
        public string? LocationSearchTerm { get; set; }


        public string SortBy { get; set; } = "Id";
        public bool IsDescending { get; set; } = false;



    }
}