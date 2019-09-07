using Terraria;
using Terraria.ModLoader;

namespace PrimordialSands.Buffs
{

    public class ReaperHealBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Artifact: Reaper's Ward");
            Description.SetDefault("Reapers provide automatic healing");
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            bool NoreaperRosario = ((PrimordialSandsPlayer)player.GetModPlayer(mod, "PrimordialSandsPlayer")).reaperRosario = false;
            player.buffTime[buffIndex] = 18000;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("ReaperProjectile")] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer && !NoreaperRosario)
            {
                int num1 = Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("ReaperProjectile"), 0, 0f, player.whoAmI, 0f, 0f);
                int num2 = Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("ReaperProjectile"), 0, 0f, player.whoAmI, 0f, 0f);
                int num3 = Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("ReaperProjectile"), 0, 0f, player.whoAmI, 0f, 0f);
                int num4 = Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("ReaperProjectile"), 0, 0f, player.whoAmI, 0f, 0f);
                int num5 = Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("ReaperProjectile"), 0, 0f, player.whoAmI, 0f, 0f);
                int num6 = Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("ReaperProjectile"), 0, 0f, player.whoAmI, 0f, 0f);
                Main.projectile[num1].ai[1] = 60;
                Main.projectile[num2].ai[1] = 120;
                Main.projectile[num3].ai[1] = 180;
                Main.projectile[num4].ai[1] = 240;
                Main.projectile[num5].ai[1] = 300;
                Main.projectile[num6].ai[1] = 360;
            }
        }
    }
}