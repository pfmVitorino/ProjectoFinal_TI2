namespace GamingStore_Projectoti2.Migrations
{
    using GamingStore_Projectoti2.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GamingStore_Projectoti2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GamingStore_Projectoti2.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //#############################################################
            // criação das classes Cliente, Plataforma, Jogos e Compra
            //#############################################################

            // Configuration --- SEED
            //=============================================================

            //            // ############################################################################################
            //            // adiciona Clientes
            var cliente = new List<Clientes> {
                  new Clientes  {Id = 1,Nome = "Pedro Vitorino", NIF ="23343434", Email="pedro@hotmail.com", Morada="Rua Mota Lima" ,CodPostal="2300-455 Tomar"},

         };
            cliente.ForEach(dd => context.Clientes.AddOrUpdate(d => d.Nome, dd));
            context.SaveChanges();

            //            // ############################################################################################
            //            // adiciona Jogos
            var jogos = new List<Jogos> {
              new Jogos  {Id=1, Nome = "Hitman2", Plataforma ="XBOX ONE",Fotografia="hit.jpg", Preco= 55},
              new Jogos  {Id=2, Nome = "Red Dead Redemptiom 2", Plataforma ="PS4",Fotografia="red.png", Preco= 70},
              new Jogos  {Id=3, Nome = "Fifa 19", Plataforma ="PS4",Fotografia="fifa.jpg", Preco= 60},
              new Jogos  {Id=4, Nome = "Resident Evil 7", Plataforma ="PC",Fotografia="res.jpg", Preco= 65},
              new Jogos  {Id=5, Nome = "Call of Duty Black OPS4", Plataforma ="PS4",Fotografia="call.jpg", Preco= 69},




};

            jogos.ForEach(aa => context.Jogos.AddOrUpdate(a => a.Nome, aa));
            context.SaveChanges();


            //            // ############################################################################################
            //            // adiciona Plataformas
            var plataformas = new List<Plataformas> {
                             new Plataformas  {Id=1, Nome = "PS4" },
                             new Plataformas  {Id=2, Nome = "PC" },
                             new Plataformas  {Id=3, Nome = "XBOX ONE" },

};

            plataformas.ForEach(vv => context.Plataformas.AddOrUpdate(v => v.Nome, vv));
            context.SaveChanges();

            //            // ############################################################################################
            //            // adiciona Compra
            var compras = new List<Compras> {
                         new Compras  {Id= 1, Data = new DateTime(), Preco =70 , ClientesFK = 1},

};

            compras.ForEach(cc => context.Compras.Add(cc));
            context.SaveChanges();




        }





    }
}
