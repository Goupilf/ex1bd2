using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestORMCodeFirst.Entities;
using TestORMCodeFirst.Persistence;
using Xunit;

namespace TestORMCodeFirst.DAL
{
    public class EFCoursRepositoryTest
    {
        private EFEtudiantRepository repoEtudiants;
        private EFInscCoursRepository repoInscriptions;
        private EFCoursRepository repoCours;
        private void SetUp()
        {
            // Initialiser les objets nécessaires aux tests
            var builder = new DbContextOptionsBuilder<CegepContext>();
            builder.UseInMemoryDatabase(databaseName: "testEtudiant_db");   // Database en mémoire
            var contexte = new CegepContext(builder.Options);
            repoEtudiants = new EFEtudiantRepository(contexte);
            repoInscriptions = new EFInscCoursRepository(contexte);
            repoCours = new EFCoursRepository(contexte);
        }

        [Fact]
        public void AjouterCours() 
        {
            //Arrange
            SetUp();
            Cours cours = new Cours
            {
                CodeCours = "W49",
                NomCours = "sessionH20"
            };

            // Act
            repoCours.AjouterCours(cours);

            //Assert
            var result = repoCours.ObtenirListeCours();
            Assert.Single(result);
            Assert.Same(cours, result.First());
        }

        [Fact]
        public void ObtenirListeCoursEmpty() 
        {
            //Arrange
            SetUp();

            // Act
            var result =repoCours.ObtenirListeCours();

            //Assert
            Assert.Empty(result);

        }
        [Fact]
        public void ObtenirListeCoursNotEmpty()
        {
            //Arrange
            SetUp();
            Cours cours = new Cours
            {
                CodeCours = "W49",
                NomCours = "sessionH20"
            };
            repoCours.AjouterCours(cours);

            // Act
            var result = repoCours.ObtenirListeCours();

            //Assert
            Assert.NotEmpty(result);
            Assert.Same(cours, result.First());

        }
    }
}
