using Microsoft.EntityFrameworkCore;
using ProjetoContatosMVC.Data.Map;
using ProjetoContatosMVC.Models;

namespace ProjetoContatosMVC.Data
{
	public class bancoContext : DbContext
	{

		public bancoContext(DbContextOptions<bancoContext> options) : base(options)
		{
		}

		public DbSet<ContatoModel> Contatos { get; set; }
		public DbSet<UsuarioModel> Usuarios { get; set; }	
	
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());
			base.OnModelCreating(modelBuilder);
        }
	}
}
