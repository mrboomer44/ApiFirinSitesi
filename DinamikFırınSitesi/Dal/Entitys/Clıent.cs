using System.ComponentModel.DataAnnotations.Schema;

namespace AkademiqDinamikFırınSitesiApi.Dal.Entitys
{
    [Table("Clıents")]
    public class Client
    {
        [Column("ClıentId")]
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
