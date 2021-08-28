using System.Collections;
using System.Linq;
using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Logging;
using SongDataCore.BeatStar;
using SongDataCoreSourceOverrider.HarmonyPatches;

namespace SongDataCoreSourceOverrider
{
	[Plugin(RuntimeOptions.DynamicInit)]
	public class Plugin
	{
		private const string HARMONY_ID = "be.erisapps.SongDataCoreSourceOverrider";

		private static Harmony? _harmonyInstance;

		internal static Logger Logger = null!;
		internal static PluginConfig Config = null!;

		[Init]
		public void Init(Logger logger, Config config)
		{
			Logger = logger;
			Config = config.Generated<PluginConfig>();
		}

		[OnEnable]
		public void OnEnable()
		{
			var iEnumeratorTypeDef = AccessTools.GetTypesFromAssembly(typeof(BeatStarDatabase).Assembly)
				.FirstOrDefault(x => x.FullName != null && x.FullName.Contains(nameof(BeatStarDatabase)) && x.FullName.Contains("DownloadBeatStarDatabases"));
			var original = AccessTools.Method(iEnumeratorTypeDef, nameof(IEnumerator.MoveNext));
			var transpiler = AccessTools.Method(typeof(BeatStarSourceUrlTranspiler), nameof(BeatStarSourceUrlTranspiler.Transpiler));

			_harmonyInstance = new Harmony(HARMONY_ID);
			_harmonyInstance.Patch(original, transpiler: new HarmonyMethod(transpiler));
		}

		[OnDisable]
		public void OnDisable()
		{
			_harmonyInstance?.UnpatchSelf();
			_harmonyInstance = null;
		}
	}
}