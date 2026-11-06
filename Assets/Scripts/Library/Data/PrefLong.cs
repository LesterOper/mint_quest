using SimpleSQL;

namespace Swimmer.Library.Data
{
    public class PrefLong
    {
        [PrimaryKey]
        public string PrefName { get; set; }
        public long Value { get; set; }
    }
}