using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.Projectiles
{
    public class NecroticSceptreProjectile : AbsorptionProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necrotic Sceptre");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.penetrate = 1;
            projectile.aiStyle = 8;
            projectile.timeLeft = 120;
            projectile.friendly = true;
            projectile.tileCollide = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            NPC npc = Main.npc[target.target];
            AbsorptionPlayer.ModPlayer(player).absorptionDamage = (int)(target.damage * 4);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item66, projectile.position);
            Dust.NewDust(projectile.Center, 0, 0, 127, 0f, 0f, 100, default(Color), 1f);
        }
    }
}