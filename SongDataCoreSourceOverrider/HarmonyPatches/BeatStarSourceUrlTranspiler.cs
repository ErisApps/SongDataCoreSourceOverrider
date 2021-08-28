using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using SongDataCore.BeatStar;

namespace SongDataCoreSourceOverrider.HarmonyPatches
{
	/// <summary>
	/// Transpiler is applied manually on enabling of the plugin
	/// </summary>
	internal class BeatStarSourceUrlTranspiler
	{
		internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			foreach (var codeInstruction in instructions)
			{
				if (codeInstruction.opcode != OpCodes.Ldstr)
				{
					yield return codeInstruction;
					continue;
				}

				if (codeInstruction.operand as string == BeatStarDatabase.SCRAPED_SCORE_SABER_ALL_JSON_URL && Plugin.Config.BeatStarSourceUrlOverride != null)
				{
					codeInstruction.operand = Plugin.Config.BeatStarSourceUrlOverride;

					Plugin.Logger.Debug($"Overwrote operand to {codeInstruction.operand}");
				}

				yield return codeInstruction;
			}
		}
	}
}