namespace Vessel.Config;

public class ConfigCliParser : IConfigCliParser
{
    public VesselConfig ApplyOverrides(VesselConfig config, string[] args)
    {
        for (var i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "--url" when i + 1 < args.Length:
                    config.Url = args[++i];
                    break;
                case "--title" when i + 1 < args.Length:
                    config.Title = args[++i];
                    break;
                case "--width" when i + 1 < args.Length:
                    if (int.TryParse(args[++i], out var w))
                        config.Width = w;
                    break;
                case "--height" when i + 1 < args.Length:
                    if (int.TryParse(args[++i], out var h))
                        config.Height = h;
                    break;
                case "--float":
                    config.AlwaysOnTop = true;
                    break;
                case "--no-float":
                    config.AlwaysOnTop = false;
                    break;
            }
        }

        return config;
    }
}
