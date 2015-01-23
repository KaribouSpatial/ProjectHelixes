using Newtonsoft.Json;

namespace ProjectHelix.Data
{
    public class User
    {
        public User(string userId, int gold, string name)
        {
            Id = userId;
            Gold = gold;
            Name = name;
            HullR = "Common";
            CanonR = "Common";
            BulletR = "Common";
            SailsR = "Common";
            WheelR = "Common";
        }

        public string Id { get; set; }

        public int Gold { get; set; }

        public string Name { get; set; }

        public string HullR { get; set; }

        public string CanonR { get; set; }

        public string BulletR { get; set; }

        public string SailsR { get; set; }

        public string WheelR { get; set; }
        public int HullUN { get; set; }
        public int CanonUN { get; set; }
        public int BulletUN { get; set; }
        public int SailsUN { get; set; }
        public int WheelUN { get; set; }
        public int HullRN { get; set; }
        public int CanonRN { get; set; }
        public int BulletRN { get; set; }
        public int SailsRN { get; set; }
        public int WheelRN { get; set; }
        public int HullLN { get; set; }
        public int CanonLN { get; set; }
        public int BulletLN { get; set; }
        public int SailsLN { get; set; }
        public int WheelLN { get; set; }
    }
}
