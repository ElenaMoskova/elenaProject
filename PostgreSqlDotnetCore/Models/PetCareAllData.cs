namespace PostgreSqlDotnetCore.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PetCareAllData
    {
     
        public List<Pet_CaresClass> PetCares { get; set; }
        public List<VetCenter> VetCenters { get; set; }
        public List<UsersClass> Users { get; set; }

        public PetCareAllData()
        {
            Users = new List<UsersClass>();
            VetCenters = new List<VetCenter>();
            PetCares = new List<Pet_CaresClass>();
        }
    }
}
