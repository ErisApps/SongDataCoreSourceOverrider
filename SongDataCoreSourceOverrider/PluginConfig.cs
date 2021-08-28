using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace SongDataCoreSourceOverrider
{
	internal class PluginConfig
	{
		public virtual string? BeatStarSourceUrlOverride { get; set; } = "https://static.eyskens.me/bssb/v2-all.json.gz";
	}
}