namespace EX.Model.DbLayer
{
    public class Command
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}