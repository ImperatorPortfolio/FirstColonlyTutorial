using System;
using System.Text;
using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.Game.Entity;
using VRageMath;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using VRage.Utils;
using Sandbox.ModAPI.Interfaces.Terminal;
using System.Collections.Generic;

using static AOE.CampaignDefinitions;


namespace AOE
{
	partial class CampaignData
	{
		CampaignDefinition FirstColonyTutorialDefinition => new CampaignDefinition
		{
			Name = "First Colony Survival Tutorial",
			Author = "Imperator"
		};
	}
}