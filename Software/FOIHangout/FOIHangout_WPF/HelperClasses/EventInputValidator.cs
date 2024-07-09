using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FOIHangout_WPF.HelperClasses
{
 public class EventInputValidator
    {
            public static bool ValidateInput(string naziv, string opis, string posebanDogadaj, DateTime? pocetak, DateTime? zavrsetak)
            {
                if (string.IsNullOrWhiteSpace(naziv) ||
                    string.IsNullOrWhiteSpace(opis) ||
                    string.IsNullOrWhiteSpace(posebanDogadaj))
                {
                    MessageBox.Show("Obavezno popunite naziv, opis i poseban događaj!");
                    return false;
                }

                if (!pocetak.HasValue ||
                    !zavrsetak.HasValue ||
                    zavrsetak < pocetak)
                {
                    MessageBox.Show("Odaberite validne datume!\nDatum završetka nemože biti manji od datuma početka.");
                    return false;
                }

                return true;
            }
        }
}
