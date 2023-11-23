namespace CansolveANK.Controllers
{
    public class TagNameRequestModel
    {
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        //public int TimeIntervalRate { get; set; }
        //public string TimeIntervalUnit { get; set; }
        //public long SampleRate { get; set; }
        //public string SampleUnit { get; set; }
        public List<string> tag_name { get; set; } = new List<string>();

        public string[] GetArrayFromList()
        {
            return tag_name.ToArray();
        }
    }

}
