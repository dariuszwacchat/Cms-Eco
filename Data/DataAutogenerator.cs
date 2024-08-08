using System.Globalization;

namespace Data
{
    public class DataAutogenerator
    {

        private string lettersTable = "abcdefghijklłmnoprstuóxyz";
        private string slowo = "";
        private string zdanie = "";
        private string wylosowaneSlowo = "";
        private int dlugoscSlowaFrom = 1;
        private int dlugoscSlowaTo = 10;
        private List<string> listaSlow = new List<string>();
        private List<string> listaZdan = new List<string>();
        private Random rand = new Random();

        public string Word()
        {
            slowo = "";
            slowo = Word(LosowaIlosc());
            return slowo;
        }

        public string Word(int length)
        {
            slowo = "";
            for (var i = 0; i < length; i++)
            {
                wylosowaneSlowo = lettersTable[LosowaLitera].ToString();
                slowo += wylosowaneSlowo;
            }
            return slowo;
        }

        public string Word(int dlugoscSlowaFrom, int dlugoscSlowaTo)
        {
            slowo = "";
            slowo = Word(LosowaIlosc(dlugoscSlowaFrom, dlugoscSlowaTo));
            return slowo;
        }


        /// <summary>
        /// Metoda generuje losową ilość słów z zakresu od 1 do 10
        /// </summary> 
        public List<string> Words()
        {
            slowo = "";
            listaSlow.Clear();
            for (var i = 0; i < LosowaIlosc(); i++)
            {
                slowo = Word(dlugoscSlowaFrom, dlugoscSlowaTo);
                listaSlow.Add(slowo);
            }
            return
                listaSlow;
        }

        /// <summary>
        /// Metoda generuje określoną ilość słów
        /// </summary> 
        public List<string> Words(int iloscSlow)
        {
            slowo = "";
            listaSlow.Clear();
            for (var i = 0; i < iloscSlow; i++)
            {
                slowo = Word(4, 11);
                listaSlow.Add(slowo);
            }
            return
                listaSlow;
        }

        public List<string> Words(int iloscSlow, int wordLengthFrom, int wordLengthTo)
        {
            slowo = "";
            listaSlow.Clear();
            for (var i = 0; i < iloscSlow; i++)
            {
                slowo = Word(wordLengthFrom, wordLengthTo);
                listaSlow.Add(slowo);
            }
            return
                listaSlow;
        }


        public string Zdanie()
        {
            zdanie = "";
            foreach (var slowo in Words(LosowaIlosc()))
                zdanie += slowo + " ";
            zdanie = ZdanieSupport(zdanie);
            return
                zdanie;
        }

        public string Zdanie(int iloscZdan)
        {
            zdanie = "";
            for (var i = 0; i < iloscZdan; i++)
            {
                foreach (var slowo in Words(LosowaIlosc()))
                    zdanie += slowo + " ";
                zdanie = ZdanieSupport(zdanie);
            }
            return
                zdanie;
        }

        public string Zdanie(int iloscZdan, int iloscSlowWzdaniu)
        {
            zdanie = "";
            for (var i = 0; i < iloscZdan; i++)
            {
                foreach (var slowo in Words(iloscSlowWzdaniu))
                    zdanie += slowo + " ";
                zdanie = ZdanieSupport(zdanie);
            }
            return
                zdanie;
        }

        public string Zdanie(int iloscZdan, int iloscSlowWzdaniuFrom, int iloscSlowWzdaniuTo)
        {
            zdanie = "";
            for (var i = 0; i < iloscZdan; i++)
            {
                foreach (var slowo in Words(LosowaIlosc(iloscSlowWzdaniuFrom, iloscSlowWzdaniuTo))) ;
                zdanie += slowo + " ";
                zdanie = ZdanieSupport(zdanie);
            }
            return
                zdanie;
        }


        public List<string> Zdania(int iloscZdan)
        {
            listaZdan.Clear();
            zdanie = "";
            for (var i = 0; i < iloscZdan; i++)
            {
                foreach (var slowo in Words(LosowaIlosc()))
                    zdanie += slowo + " ";
                zdanie = ZdanieSupport(zdanie);
                listaZdan.Add(zdanie);
                zdanie = "";
            }
            return
                listaZdan;
        }

        public List<string> Zdania(int iloscZdan, int iloscSlowWzdaniuFrom, int iloscSlowWzdaniuTo)
        {
            listaZdan.Clear();
            zdanie = "";
            for (var i = 0; i < iloscZdan; i++)
            {
                foreach (var slowo in Words(LosowaIlosc(iloscSlowWzdaniuFrom, iloscSlowWzdaniuTo)))
                    zdanie += slowo + " ";
                zdanie = ZdanieSupport(zdanie);
                listaZdan.Add(zdanie);
                zdanie = "";
            }
            return
                listaZdan;
        }

        public string Title() => Word(8, 20);
        public string Title(int wordLengthFrom, int wordLengthTo) => Word(wordLengthFrom, wordLengthTo);

        public string Description() => Zdanie(7);
        public string Description(int iloscZdan) => Zdanie(iloscZdan);

        public string Imie() => Word(7, 10);
        public string Nazwisko() => Word(7, 12);

        public int Number() => rand.Next(1, 100);
        public int Quantity() => rand.Next(1, 100);

        public int Quantity(int from, int to) => rand.Next(from, to);
        public string Ulica() => Zdanie(1, LosowaIlosc(1, 3));

        public long Pesel()
        {
            string stringNumbers = "";
            for (var i = 0; i < 9; i++)
                stringNumbers += rand.Next(1, 9).ToString();
            return
                long.Parse(stringNumbers);
        }

        public double Price()
        {
            string left = "";
            string right = "";
            for (var i = 0; i < 4; i++)
                left += rand.Next(1, 9);

            for (var i = 0; i < 2; i++)
                right += rand.Next(1, 9);
            return
                double.Parse($"{left},{right}");
        }

        public double Price(double from, double to)
        {
            return
                double.Parse(
                    rand.Next(int.Parse(from.ToString()), int.Parse(to.ToString()))
                    .ToString()
                    );
        }

        public double PriceWithoutComma()
        {
            string left = "";
            for (var i = 0; i < 4; i++)
                left += rand.Next(1, 9);
            return
                double.Parse($"{left}");
        }


        public string ImagePath()
        {
            string path = @"C:\Testy\Images";
            string[] imagesPaths = Directory.GetFiles(path, "*.png");
            return
                imagesPaths[rand.Next(0, imagesPaths.Length)];
        }

        public DateTime RandomDateTime()
        {
            int year = rand.Next(2020, 2022);
            int month = rand.Next(1, 12);
            int day = rand.Next(1, CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(year, month));

            int second = rand.Next(1, 60);
            int minute = rand.Next(1, 60);
            int hour = rand.Next(1, 24);

            return
                new DateTime(year, month, day, hour, minute, second);
        }


        public DateTime RandomFutureDateTime()
        {
            var now = DateTime.Now;
            int year = DateTime.Now.Year;

            int mo = now.Month;
            int d = now.Day;

            int month = rand.Next(mo, rand.Next(mo, 12));
            int day = rand.Next(d, rand.Next(d, 29));

            int s = now.Second;
            int m = now.Minute;
            int h = now.Hour;

            int second = rand.Next(s, 60);
            int minute = rand.Next(m, 60);
            int hour = rand.Next(h, 24);

            return
                new DateTime(year, month, day, hour, minute, second);
        }

        public string IlePozostaloRoznica(DateTime dataObecna, DateTime dataPrzyszla)
        {
            var result = dataPrzyszla - dataObecna;
            return
                result.Days.ToString() + " dni, " +
                result.Hours.ToString() + ":" +
                result.Minutes.ToString() + ":" +
                result.Seconds.ToString();
        }

        public bool CzyDataJestPrzeszla(DateTime dataObecna, DateTime dataPrzyszla)
        {
            var result = dataPrzyszla - dataObecna;
            string dataString = result.ToString();
            if (dataString[0] == '-')
                return
                    true;
            else
                return false;
        }

        public bool IsElapsed(DateTime futureDateTime)
        {
            var elapsed = futureDateTime - DateTime.Now;
            string elapsedString = elapsed.ToString();
            if (elapsedString[0] == '-')
                return true;
            else
                return false;
        }

        public TimeSpan IlePozostalo(DateTime futureDateTime)
        {
            var elapsed = futureDateTime - DateTime.Now;
            return new TimeSpan(
                elapsed.Days,
                elapsed.Hours,
                elapsed.Minutes,
                elapsed.Seconds
                );
        }

        public TimeSpan IleUplynelo(DateTime pastData)
        {
            var elapsed = DateTime.Now - pastData;
            return new TimeSpan(
                elapsed.Days,
                elapsed.Hours,
                elapsed.Minutes,
                elapsed.Seconds
                );
        }


        public TimeSpan IleUplynelo(DateTime fromPast, DateTime toFuture)
        {
            var elapsed = toFuture - fromPast;
            return new TimeSpan(
                elapsed.Days,
                elapsed.Hours,
                elapsed.Minutes,
                elapsed.Seconds
                );
        }



        public int IleUplyneloDni(DateTime pastData)
        {
            // wyliczane dni z godzin
            return IleUplyneloGodzin(pastData) / 24;
        }

        public int IleUplyneloGodzin(DateTime pastData)
        {
            // Wyliczane godzin z dni i godzin lub z samych godzin
            var elapsed = DateTime.Now - pastData;
            var ts = new TimeSpan(
                elapsed.Days,
                elapsed.Hours,
                elapsed.Minutes,
                elapsed.Seconds
                );
            int ile = 0;
            if (ts.Days == 0)
            {
                ile = ts.Hours;
            }
            if (ts.Days > 0)
            {
                ile = (ts.Days * 24) + ts.Hours;
            }
            return ile;
        }


        public int IleUplyneloGodzin(DateTime fromPast, DateTime toFuture)
        {
            // Wyliczane godzin z dni i godzin lub z samych godzin
            var elapsed = toFuture - fromPast;
            var ts = new TimeSpan(
                elapsed.Days,
                elapsed.Hours,
                elapsed.Minutes,
                elapsed.Seconds
                );
            int ile = 0;
            if (ts.Days == 0)
            {
                ile = ts.Hours;
            }
            if (ts.Days > 0)
            {
                ile = (ts.Days * 24) + ts.Hours;
            }
            return ile;
        }

        public int IleUplyneloTygodni(DateTime pastData)
        {
            // Wyliczane tygodni z godzin 
            int dni = IleUplyneloGodzin(pastData) / 24;
            int tygodnie = IleUplyneloGodzin(pastData) / (7 * 24);
            int tygodnie2 = IleUplynelo(pastData).Days / 7;

            return IleUplyneloDni(pastData) / 7;
        }




        private int LosowaLitera
        {
            get
            {
                return
                    rand.Next(0, lettersTable.Length);
            }
            set { }
        }


        private int LosowaIlosc()
            => rand.Next(dlugoscSlowaFrom, dlugoscSlowaTo);
        private int LosowaIlosc(int from, int to)
            => rand.Next(from, to);

        private string JoinWordsInList(List<string> lista)
        {
            slowo = "";
            if (lista.Count > 0 && lista != null)
            {
                for (var i = 0; i < lista.Count; i++)
                {
                    slowo += lista[i].Trim() + " ";
                }
            }

            return
                slowo;
        }


        private string ZdanieSupport(string zdanieInter)
        {
            zdanieInter = zdanieInter.Trim() + ". ";
            return
                zdanieInter;
        }


        public string Email()
        {
            string left = this.Word(5, 10);
            string right = this.Word(5, 10);
            string email = $"{left}@{right}.pl";
            return
                email;

        }


    }
}
