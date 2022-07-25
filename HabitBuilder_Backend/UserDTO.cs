namespace HabitBuilder_Backend
{
    public class UserDTO
    {
        public UserDTO(string firstName,string lastName,string email, string userName, DateTime dateCreated)
        {
            FirstName = firstName;
            LastName = lastName;    
            Email = email;
            UserName = userName;
            DateCreated = dateCreated; 


        }

        public string FirstName { get; set; }   
       public string LastName { get; set; } 
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Token { get; set; }
    }
}
