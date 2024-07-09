using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class obavijestReportRepository : Repository<obavijestREPORT>
    {
        public obavijestReportRepository() : base(new FOIHangoutModel())
        {
        }

        public IQueryable<obavijestREPORT> GetObavijestReportById(int id)
        {
            var query = from p in Entities where p.id == id select p;
            return query;
        }
        public obavijestREPORT GetSpecificNotificationREPORTById(int id)
        {
            return Entities.FirstOrDefault(p => p.id == id);
        }

        public override int Add(obavijestREPORT entity, bool saveChanges = true)
        {
            var obavijestReport = new obavijestREPORT();

            obavijestReport.id_korisnika_koji_prijavljuje_korisnika = entity.id_korisnika_koji_prijavljuje_korisnika;
            obavijestReport.id_korisnika_koji_je_prijavljen = entity.id_korisnika_koji_je_prijavljen;
            obavijestReport.report_naslov = entity.report_naslov;
            obavijestReport.report_opis = entity.report_opis;

            Entities.Add(obavijestReport);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public override int Update(obavijestREPORT entity, bool saveChanges = true)
        {
            var obavijestReport = Entities.SingleOrDefault(u => u.id == entity.id);

            obavijestReport.id_korisnika_koji_prijavljuje_korisnika = entity.id_korisnika_koji_prijavljuje_korisnika;
            obavijestReport.id_korisnika_koji_je_prijavljen = entity.id_korisnika_koji_je_prijavljen;
            obavijestReport.report_naslov = entity.report_naslov;
            obavijestReport.report_opis = entity.report_opis;

            Entities.Add(obavijestReport);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
    }
}
