using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archipelago.Core.Models;

namespace MedievilArchipelago.Models
{
    public class GenericItemsData
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public uint Address { get; set; }

        public string LevelId { get; set; }

        public string Check {  get; set; }

        public LocationCheckType CheckType { get; set; }

        public uint RipperShiftAddress { get; set; }

        public GenericItemsData(string name, uint locationAddress, string levelId, string check, LocationCheckType checkType, int id = 0, uint ripperShiftAddress = 0)
        {
            Name = name;
            Id = id;
            Address = locationAddress;
            LevelId = levelId;
            Check = check;
            CheckType = checkType;
            RipperShiftAddress = ripperShiftAddress;
        }
    }
}
