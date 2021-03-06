﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestORMCodeFirst.Entities;
using TestORMCodeFirst.Persistence;

namespace TestORMCodeFirst.DAL
{
    public class EFInscCoursRepository
    {
        private CegepContext contexte;

        public EFInscCoursRepository(CegepContext ctx)
        {
            contexte = ctx;
        }

        public void AjouterInscription(short etudiantId, string codeCours, string session)
        {
            InscriptionCours inscription = new InscriptionCours { EtudiantID = etudiantId, CodeCours = codeCours, CodeSession = session };

            contexte.InscCours.Add(inscription);
            contexte.SaveChanges();
        }

        public ICollection<InscriptionCours> ObtenirInscriptions()
        {
            return contexte.InscCours.ToList();
        }

        public InscriptionCours ObtenirInscription(short etudiantID, string codeCours, string session)   //TO DO: à tester
        {
            return contexte.InscCours.Find(etudiantID, codeCours, session);
        }

        public void SupprimerToutesLesInscriptions()
        {
            contexte.InscCours.RemoveRange(contexte.InscCours);
            contexte.SaveChanges();
        }

        public int NombreEtudiantsInscritsAuCegep(string session)
        {
            return contexte.InscCours.Where(insc => insc.CodeSession == session)
                                            .GroupBy(insc => insc.EtudiantID)
                                            .Select(groupe => new { groupe.Key })
                                            .Count();
        }

        public void AjouterCours(InscriptionCours cours)
        {
            contexte.InscCours.Add(cours);
            contexte.SaveChanges();
        }
        public int NombreEtudiantInscrits(String codeCours, String session) 
        {
            return contexte.InscCours.Where(insc => insc.CodeSession == session && insc.CodeCours == codeCours)
                                            .Count();
        }
        public int NombreInscriptionsCours(short etudiantId, string session) 
        {
            return contexte.InscCours.Where(insc => insc.EtudiantID == etudiantId && insc.CodeSession == session)
                                            .Count();
        }
        public void MettreAJourNoteFinale(short etudiantId, string cours, string session, short note) 
        {

            
            InscriptionCours inscToChange = ObtenirInscription(etudiantId, cours, session);
            inscToChange.NoteFinale = note;
            contexte.InscCours.Update(inscToChange);
            contexte.SaveChanges();


        }
        public double? ObtenirPourUneClasseLaMoyenne(string cours, string session) 
        {
            return contexte.InscCours.Where(insc => insc.CodeCours == cours && insc.CodeSession == session)
                                           .Average(insc => insc.NoteFinale);

            
        }

        public int? ObtenirPourUneClasseNombreEchecs(string cours, string session) 
        {
            return contexte.InscCours.Where(insc => insc.CodeCours == cours && insc.CodeSession == session && insc.NoteFinale < 60)
                                            .Count();
        }

    }
}
