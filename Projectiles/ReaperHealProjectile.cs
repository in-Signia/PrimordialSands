using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrimordialSands.Projectiles
{
    public class ReaperHealProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reaper Heal");
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 120;
        }
        public override void Kill(int timeLeft)
        {
            for (int num2 = 0; num2 < 15; num2++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 266, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num].velocity *= 3f;
                Main.dust[num].noGravity = true;
            }
        }
        int num1 = 0;
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            Player P = Main.player[Main.myPlayer];
            float closestDist = 10000;
            int chosenPlayer = projectile.owner;
            for (int i = 0; i < 255; i++)
            {
                if (i == 0) closestDist = Vector2.Distance(Main.player[i].Center, projectile.Center);
                else if (Vector2.Distance(Main.player[i].Center, projectile.Center) < closestDist)
                {
                    closestDist = Vector2.Distance(Main.player[i].Center, projectile.Center);
                    chosenPlayer = i;
                }
            }
            P = Main.player[chosenPlayer];
            if (num1 <= 6)
            {
                projectile.velocity = Vector2.Normalize(P.Center - projectile.Center) * 8;
            }
            if (num1 == 8)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedByRandom(MathHelper.ToRadians(25));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale;
                projectile.velocity.Y = perturbedSpeed.Y;
                projectile.velocity.X = perturbedSpeed.X;
            }
            num1 += 1;
            if (num1 == 20)
            {
                num1 = 0;
            }
                int num4;
            for (int num93 = 0; num93 < 1; num93 = num4 + 1)
            {
                float num94 = projectile.velocity.X / 3f * (float)num93;
                float num95 = projectile.velocity.Y / 3f * (float)num93;
                int num96 = 4;
                int num97 = Dust.NewDust(new Vector2(projectile.position.X + (float)num96, projectile.position.Y + (float)num96), projectile.width - num96 * 2, projectile.height - num96 * 2, 235, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num97].noGravity = true;
                Dust dust3 = Main.dust[num97];
                dust3.velocity *= 0.2f;
                dust3 = Main.dust[num97];
                dust3.velocity += projectile.velocity * 0.2f;
                Dust dust6 = Main.dust[num97];
                dust6.position.X = dust6.position.X - num94;
                Dust dust7 = Main.dust[num97];
                dust7.position.Y = dust7.position.Y - num95;
                num4 = num93;
            }
            if (projectile.ai[1] == 0)
            {
                projectile.ai[1] = 2 + Main.rand.Next(1, 2);
            }
            int num492 = (int)projectile.ai[0];
            float num493 = 4f;
            Vector2 vector39 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num494 = Main.player[num492].Center.X - vector39.X;
            float num495 = Main.player[num492].Center.Y - vector39.Y;
            float num496 = (float)Math.Sqrt((double)(num494 * num494 + num495 * num495));
            if (num496 < 50f && projectile.position.X < Main.player[num492].position.X + (float)Main.player[num492].width && projectile.position.X + (float)projectile.width > Main.player[num492].position.X && projectile.position.Y < Main.player[num492].position.Y + (float)Main.player[num492].height && projectile.position.Y + (float)projectile.height > Main.player[num492].position.Y)
            {
                if (projectile.owner == Main.myPlayer && !Main.player[Main.myPlayer].moonLeech)
                {
                    int num497 = (int)projectile.ai[1];
                    Main.player[num492].HealEffect(num497, false);
                    Player player = Main.player[num492];
                    player.statLife += num497;
                    NetMessage.SendData(66, -1, -1, null, num492, (float)num497, 0f, 0f, 0, 0, 0);
                }
                projectile.Kill();
            }
            num496 = num493 / num496;
            num494 *= num496;
            num495 *= num496;
        }
    }
}