using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace PrimordialSands.NPCs.TownNPCs
{
	[AutoloadHead]
	public class Assasin : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "PrimordialSands/NPCs/TownNPCs/Assasin";
			}
		}

		public override bool Autoload(ref string name)
		{
			name = "Assasin";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 23;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 90;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 26;
			npc.height = 44;
			npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = NPCID.Mechanic;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			int num = npc.life > 0 ? 1 : 5;
			for (int k = 0; k < num; k++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood);
			}
		}

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return true;
        }

		public override string TownNPCName()
		{
			switch (WorldGen.genRand.Next(4))
			{
				case 0:
					return "Ma'ria";
				case 1:
					return "Sofia";
				case 2:
					return "Casandra";
				default:
					return "Victoria";
			}
		}

		public override string GetChat()
		{
			switch (Main.rand.Next(3))
			{
				case 0:
					return "A keen blade will always prevail with patience...";
				case 1:
					return "My tools will not grant you success, it is your patience and skill.";
				default:
					return "I've heard rumors recently of an Orcin defensive on it's way to our very settlement. It is best to prepare for an invasion.";
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(mod.ItemType("IronSpade"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("LeadSpade"));
			nextSlot++;
			shop.item[nextSlot].SetDefaults(mod.ItemType("SteelArrow"));
			nextSlot++;
			/*if (Main.LocalPlayer.HasBuff(BuffID.Lifeforce))
			{
				shop.item[nextSlot].SetDefaults(mod.ItemType("ExampleHealingPotion"));
				nextSlot++;
			}
			if (Main.LocalPlayer.GetModPlayer<ExamplePlayer>(mod).ZoneExample)
			{
				shop.item[nextSlot].SetDefaults(mod.ItemType("ExampleWings"));
				nextSlot++;
			}*/
		}

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 25;
			knockback = 1.5f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 15;
			randExtraCooldown = 5;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = mod.ProjectileType("AssasinDaggerProjectile");
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 15f;
			randomOffset = 2.5f;
		}
	}
}