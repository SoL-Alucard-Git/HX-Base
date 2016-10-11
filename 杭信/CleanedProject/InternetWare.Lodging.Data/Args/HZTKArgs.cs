
namespace InternetWare.Lodging.Data
{
    public class HZTKArgs:BaseArgs
    {
        public override ArgsType Type
        {
            get
            {
                return ArgsType.HongZi;
            }
        }
        public HZType hztype { get; set; }
    }

    public enum HZType
    {
        GFYDK,
        GFWDK,
        XF
    }
}
