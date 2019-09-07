using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrimordialSands.Projectiles
{
    public class InfernoProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno");
        }
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.friendly = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 1200;
            projectile.alpha = 100;
            projectile.magic = true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(45, (int)projectile.position.X, (int)projectile.position.Y, 14);
            for (int num621 = 0; num621 < 15; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num622].velocity *= 0.5f;
            }
        }

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            bool flag1 = true;
            int num3;
            for (int num432 = 0; num432 < 1000; num432 = num3 + 1)
            {
                if (num432 != projectile.whoAmI && Main.projectile[num432].active && Main.projectile[num432].owner == projectile.owner && Main.projectile[num432].type == projectile.type && projectile.timeLeft > Main.projectile[num432].timeLeft && Main.projectile[num432].timeLeft > 30)
                {
                    flag1 = false;
                    projectile.netUpdate = true;
                    Main.projectile[num432].timeLeft = 30;
                }
                num3 = num432;
            }
            projectile.velocity.X *= 0f;
            projectile.velocity.Y *= 0f;
            float num33 = 10;
            float num44 = 6.28318548f * Main.rand.NextFloat();
            Vector2 value = new Vector2(32f, 32f);
            for (float num5 = 0f; num5 < num33; num5 += 1f)
            {
                Dust dust = Main.dust[Dust.NewDust(projectile.Center, 0, 0, 269, 0f, 0f, 0, default(Color), 1f)];
                Vector2 vector32 = Vector2.UnitY.RotatedBy((double)(num5 * 6.28318548f / num33 + num44), default(Vector2));
                dust.position = projectile.Center + vector32 * value / 2f;
                dust.velocity = vector32;
                dust.noGravity = true;
                dust.scale = 1.2f;
                dust.velocity *= dust.scale;
                dust.fadeIn = Main.rand.NextFloat() * 0.6f;
            }
            PrimordialSandsPlayer modPlayer = (PrimordialSandsPlayer)player.GetModPlayer(mod, "PrimordialSandsPlayer");

            if (modPlayer.infernoSummoned && flag1)
            {
                modPlayer.infernoSummoned = false;
                projectile.netUpdate = true;
                Main.PlaySound(45, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
                int num626 = 1;
                for (int num627 = 0; num627 < num626; num627 = num3 + 1)
                {
                    float num628 = (float)Main.rand.Next(-35, 36) * 0.02f;
                    float num629 = (float)Main.rand.Next(-35, 36) * 0.02f;
                    num628 *= 10f;
                    num629 *= 10f;
                    int data = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, num628, num629, mod.ProjectileType("InfernoBollProjectile"), projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                    Main.projectile[data].penetrate = 1;
                    Main.projectile[data].netUpdate = true;
                    num3 = num627;

                }
            }
        }
    }
}