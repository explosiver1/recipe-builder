namespace RecipeBuilder.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public List<User> Members { get; set; }

        public void AddMember(User user)
        {
            Members.Add(user);
        }

        public void RemoveMember(User user)
        {
            Members.Remove(user);
        }

        public void SetAdmin(User user)
        {
        }

        public List<User> GetMembers()
        {
            return Members;
        }
    }
}