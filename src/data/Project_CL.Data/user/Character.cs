namespace Project_CL.Data.user
{
    public class Character
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        //Character 
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Class { get; set; } = default!;
        //Stats
        public int Level { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Defense { get; set; }
        public int Luck { get; set; }
        public int Speed { get; set; }
        public int Experience { get; set; }
        public int Gold { get; set; }


    }
}
