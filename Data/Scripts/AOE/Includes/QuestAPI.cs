using System;
using System.IO;
using Sandbox.ModAPI;
using VRage.Game.Components;
using System.Collections.Generic;
using System.Collections.Generic;
using ProtoBuf;
using VRageMath;
using System;
using System.Text;
using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.ModAPI;
using VRage.Game;
using System.Xml.Serialization;

using static AOE.CampaignDefinitions;

namespace AOE{
    [ProtoContract]
    public class CampaignDefinitions{

        [ProtoContract]
        public struct CampaignDefinition{
            [ProtoMember(1)] internal string Name;
            [ProtoMember(2)] internal string Author;
        }

    }

    partial class CampaignData {
        internal List<CampaignDefinition> CampaignDefinition = new List<CampaignDefinition>();
        internal void ConfigFiles(params CampaignDefinition[] defs) { foreach (var def in defs) CampaignDefinition.Add(def); }
        internal CampaignDefinition[] GetDefs()
        {
            var CampaignDefinitions = new CampaignDefinition[CampaignDefinition.Count];
            for (int i = 0; i < CampaignDefinition.Count; i++) CampaignDefinitions[i] = CampaignDefinition[i];
            CampaignDefinition.Clear();
            return CampaignDefinitions;
    }}

    [MySessionComponentDescriptor(MyUpdateOrder.NoUpdate, int.MaxValue)]
    public class Session : MySessionComponentBase{

        internal byte[] Storage;
        internal CampaignDefinitions.CampaignDefinition[] CampaignDefinitions;

        public override void LoadData() { 
            MyAPIGateway.Utilities.RegisterMessageHandler(7782, Handler);
            Init();
            SendModMessage(true);
        }

        internal void Init(){
            var data = new CampaignData();
            CampaignDefinitions = data.GetDefs();
            Storage = MyAPIGateway.Utilities.SerializeToBinary(CampaignDefinitions);
            Array.Clear(CampaignDefinitions, 0, CampaignDefinitions.Length);
            CampaignDefinitions = null;
        }

        void Handler(object o) { if (o == null) SendModMessage(false); }
        void SendModMessage(bool sending) { MyAPIGateway.Utilities.SendModMessage(7781, Storage); }

        protected override void UnloadData(){
            MyAPIGateway.Utilities.UnregisterMessageHandler(7782, Handler);
            Array.Clear(Storage, 0, Storage.Length);
            Storage = null;
        }
}}
