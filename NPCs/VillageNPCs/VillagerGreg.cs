using System;
using PrimordialSands.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.ModLoader;

namespace PrimordialSands.NPCs.VillageNPCs
{
	// [AutoloadHead] and npc.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
	[AutoloadHead]
		public class VillagerGreg : ModNPC
		{
		public override string Texture
		{
			get
			{
				return "PrimordialSands/NPCs/VillageNPCs/VillagerGreg";
			}
		}

			public override bool Autoload(ref string name) 
			{
				name = "Villager";
				return mod.Properties.Autoload;
			}

			public override void SetStaticDefaults() 
			{
				Main.npcFrameCount[npc.type] = 12;
				NPCID.Sets.ExtraFramesCount[npc.type] = 0;
				NPCID.Sets.AttackFrameCount[npc.type] = 0;
				NPCID.Sets.DangerDetectRange[npc.type] = 30;
				NPCID.Sets.AttackType[npc.type] = 0;
				NPCID.Sets.AttackTime[npc.type] = 0;
				NPCID.Sets.AttackAverageChance[npc.type] = 0;
				NPCID.Sets.HatOffsetY[npc.type] = 4;
			}

			public override void SetDefaults() 
			{
				npc.townNPC = true;
				npc.friendly = true;
				npc.width = 22;
				npc.height = 46;
				npc.aiStyle = 7;
				npc.damage = 10;
				npc.defense = 15;
				npc.lifeMax = 250;
				npc.HitSound = SoundID.NPCHit1;
				npc.DeathSound = SoundID.NPCDeath1;
				npc.knockBackResist = 0.5f;
				animationType = NPCID.Guide;
			}

			public override void HitEffect(int hitDirection, double damage) 
			{
				int num = npc.life > 0 ? 1 : 5;
				for (int k = 0; k < num; k++) {
					Dust.NewDust(npc.position, npc.width, npc.height, 5);
				}
			}

			public override bool CanTownNPCSpawn(int numTownNPCs, int money) 
			{	
				return false;
			}

			public override string TownNPCName() {
				switch (WorldGen.genRand.Next(4)) {
					case 0:
						return "Villager";
					case 1:
						return "Greg";
					case 2:
						return "Villager";
					default:
						return "Villager";
				}
			}

			public override void FindFrame(int frameHeight) 
			{

			}

			public override string GetChat()
			{
				WeightedRandom<string> chat = new WeightedRandom<string>();
				chat.Add("We don't have much resources here in our village, please reframe from any harvesting.");
				chat.Add("Is it as dangerous as they say it is outside the village?");
				chat.Add("you want a shop? What do i look like to you, a flocking minecraft villager? Oh, wait.");
				return chat;
			}

			public override void OnChatButtonClicked(bool firstButton, ref bool shop) {
			{
					Main.playerInventory = true;
					Main.npcChatText = "";
			}
		}
	}
}
