using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnketaVezbaWPF.Model
{
    public class UpisUBazu
    {
        private static string connectionString = "Server=user-PC\\SQLEXPRESS; Database=anketeDb; Integrated Security=True;";

        public static void upisOsobe(Osoba osoba) {

            DateTime time = osoba.DatumRegistracije;  
            string format = "yyyy-MM-dd HH:mm:ss";

            string upis = "insert into Osoba(OsobaID, KorisnickoIme, Sifra, TipKorisnika, DatumRegistracije, Pristup) values('" +
                osoba.OsobaID + "','" +
                osoba.KorisnickoIme + "','" +
                osoba.Sifra + "','" +
                osoba.TipKorisnika + "', '2010-01-01 12:00', '" +
                //time.ToString(format) + "','" +
                osoba.Pristup + "');";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand()) {
                command.CommandText = upis;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }


                            
        }

        public static void upisAnkete(Anketa anketa)
        {
            string upis = "insert into Anketa(AnketaID, NaslovAnkete, Aktivnost, Javnost) values('" +
                            anketa.AnketaID + "','" +
                            anketa.NaslovAnkete + "','" +
                            anketa.Aktivnost + "','" +
                            anketa.Javnost + "');";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = upis;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public static void upisPitanja(Pitanje pitanje, int AnketaID)
        {
            string upis = "insert into Pitanje(PitanjeID, AnketaID, TekstPitanja) values('" +
                            pitanje.PitanjeID + "','" +
                            AnketaID + "','" +
                            pitanje.TekstPitanja + "');";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = upis;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public static void upisOdgovora(Odgovor odg, int pitanjeID)
        {
            string upis = "insert into Odgovor(OdgovorID, PitanjeID, TekstOdg) values('" +
                            odg.OdgovorID + "','" +
                            pitanjeID + "','" +
                            odg.TekstOdg + "');";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = upis;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public static void IzmeniPitanje(Pitanje p)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "UPDATE Pitanje SET " +
                "TekstPitanja = @TekstPitanja WHERE PitanjeID= @PitanjeID";

                cmd.Parameters.AddWithValue("@TekstPitanja", p.TekstPitanja);
                cmd.Parameters.AddWithValue("@PitanjeID", p.PitanjeID);
                

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }


        }
        public static void IzmeniOdgovor(Odgovor p)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "UPDATE Odgovor SET " +
                "TekstOdg = @TekstOdg WHERE OdgovorID= @OdgovorID";

                cmd.Parameters.AddWithValue("@TekstOdg", p.TekstOdg);
                cmd.Parameters.AddWithValue("@OdgovorID", p.OdgovorID);


                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }


        }
        public static void izmeniPodatkeOOsobi(Osoba o) {

            SqlConnection connection = new SqlConnection(connectionString);
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.Text;

                cmd.CommandText = "UPDATE Osoba SET " +
                "KorisnickoIme = @KorisnickoIme, " +
                "Sifra = @Sifra, " +
                "TipKorisnika = @TipKorisnika, " +
                "DatumRegistracije = @DatumRegistracije, " +
                "Pristup = @Pristup WHERE OsobaID= @OsobaID";

                cmd.Parameters.AddWithValue("@OsobaID", o.OsobaID);
                cmd.Parameters.AddWithValue("@KorisnickoIme", o.KorisnickoIme);
                cmd.Parameters.AddWithValue("@Sifra", o.Sifra);
                cmd.Parameters.AddWithValue("@TipKorisnika", o.TipKorisnika.ToString());
                cmd.Parameters.AddWithValue("@DatumRegistracije", o.DatumRegistracije);

                int pristup = 0;
                if (o.Pristup)
                    pristup = 1;
                cmd.Parameters.AddWithValue("@Pristup", pristup);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }


        }

        public static ObservableCollection<Osoba> ucitajOsobe() {

            ObservableCollection<Osoba> listaOsoba = new ObservableCollection<Osoba>();

            SqlConnection connection=new SqlConnection(connectionString);
            SqlCommand command;
            string sql = "SELECT * FROM Osoba;";
            SqlDataReader dataReader;

            connection.Open();
            command = new SqlCommand(sql, connection);
            dataReader = command.ExecuteReader();
            while (dataReader.Read()) {
                int OsobaID = Convert.ToInt32(dataReader.GetValue(0).ToString());
                string KorisnickoIme = dataReader.GetValue(1).ToString();
                string Sifra = dataReader.GetValue(2).ToString();
                TipoviKorisnika TipKorisnika = (TipoviKorisnika)Enum.Parse(typeof(TipoviKorisnika), dataReader.GetValue(3).ToString());
                DateTime DatumRegistracije = Convert.ToDateTime(dataReader.GetValue(4).ToString());
                bool Pristup = Convert.ToBoolean(dataReader.GetValue(5).ToString());

                Osoba o = new Osoba(OsobaID, KorisnickoIme,Sifra,TipKorisnika, DatumRegistracije, Pristup);

                listaOsoba.Add(o);
            }

            dataReader.Close();
            command.Dispose();
            connection.Close();

            return listaOsoba;
        }

        public static ObservableCollection<Anketa> UcitajAnkete()
        {
            ObservableCollection<Anketa> listaAnketa = new ObservableCollection<Anketa>();

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command;
            string sql = "SELECT * FROM Anketa;";
            SqlDataReader dataReader;

            conn.Open();
            command = new SqlCommand(sql, conn);
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                int AnketaID = Convert.ToInt32(dataReader.GetValue(0).ToString());
                string NaslovAnkete = dataReader.GetValue(1).ToString();
                bool Aktivnost = Convert.ToBoolean(dataReader.GetValue(2).ToString());
                bool Javnost = Convert.ToBoolean(dataReader.GetValue(3).ToString());

                Anketa a = new Anketa(AnketaID, NaslovAnkete, Aktivnost, Javnost);

                listaAnketa.Add(a);
            }

            dataReader.Close();
            command.Dispose();
            conn.Close();

            return listaAnketa;
        }

        public static ObservableCollection<Pitanje> UcitajPitanja()
        {
            ObservableCollection<Pitanje> listaPitanja = new ObservableCollection<Pitanje>();

            SqlConnection conn = new SqlConnection(connectionString);
            string sql = "SELECT * FROM Pitanje;";
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader dataReader;

            conn.Open();
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                int PitanjeID = Convert.ToInt32(dataReader.GetValue(0).ToString());
                int AnketaID = Convert.ToInt32(dataReader.GetValue(1).ToString());
                string TekstPitanja = dataReader.GetValue(2).ToString();

                Pitanje p = new Pitanje(PitanjeID, AnketaID, TekstPitanja);
                listaPitanja.Add(p);
            }

            dataReader.Close();
            command.Dispose();
            conn.Close();

            return listaPitanja;
        }

        public static ObservableCollection<Odgovor> UcitajOdgovore()
        {
            ObservableCollection<Odgovor> listaOdgovora = new ObservableCollection<Odgovor>();

            SqlConnection conn = new SqlConnection(connectionString);
            string sql = "SELECT * FROM Odgovor;";
            SqlCommand command = new SqlCommand(sql, conn);
            SqlDataReader dataReader;

            conn.Open();
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                int OdgovorID = Convert.ToInt32(dataReader.GetValue(0).ToString());
                int PitanjeID = Convert.ToInt32(dataReader.GetValue(1).ToString());
                string TekstOdg = dataReader.GetValue(2).ToString();

                Odgovor p = new Odgovor(OdgovorID, PitanjeID, TekstOdg);
                listaOdgovora.Add(p);
            }

            dataReader.Close();
            command.Dispose();
            conn.Close();

            return listaOdgovora;
        }

        public static ObservableCollection<Anketa> DodajObjekteUListe()
        {
            ObservableCollection<Anketa> listaAnketa = UcitajAnkete();
            ObservableCollection<Pitanje> listaPitanja = UcitajPitanja();
            ObservableCollection<Odgovor> listaOdgovora = UcitajOdgovore();

            foreach (Anketa a in listaAnketa)
            {
                foreach (Pitanje p in listaPitanja)
                {
                    if (a.AnketaID == p.AnketaID)
                    {
                        a.ListaPitanja.Add(p);

                        foreach (Odgovor o in listaOdgovora)
                        {
                            if (o.PitanjeID == p.PitanjeID)
                            {
                                p.ListaOdgovora.Add(o);
                            }
                        }
                    }
                }
            }

            return listaAnketa;

        }

        public static void brisiRedTabele(int id, string nazivTabele) {

            string sqlBrisanje = "";
            int AnketaId = -1;
            if (nazivTabele == "Odgovor")
                sqlBrisanje = "DELETE FROM Odgovor WHERE OdgovorID= '" + id + "'";
            if (nazivTabele == "Osoba")
                sqlBrisanje = "DELETE FROM Osoba WHERE OsobaID= '" + id + "'";
            if (nazivTabele == "Pitanje")
                sqlBrisanje = "DELETE FROM Pitanje WHERE PitanjeID= '" + id + "'";
            if (nazivTabele == "Anketa")
            {
                sqlBrisanje = "DELETE FROM Anketa WHERE AnketaID= '" + id + "'";
                AnketaId = id;
            }

            using (SqlConnection conn = new SqlConnection(connectionString)) {

                conn.Open();

                if (nazivTabele == "Pitanje") {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Odgovor WHERE PitanjeID= '" + id + "'", conn))
                        cmd.ExecuteNonQuery();
                }

                if (nazivTabele == "Anketa")
                {
                    //ako pitanja ne sadrze odgovore
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Pitanje WHERE AnketaID= '" + id + "'", conn))
                        cmd.ExecuteNonQuery();
                }


                using (SqlCommand cmd = new SqlCommand(sqlBrisanje, conn))
                    cmd.ExecuteNonQuery();


                conn.Close();

            }
        }
    }


    //ucitajAnkete, ucitajPitanja, ucitajOdgovore, dodajObjekteUListe //odgovore u pitanja koja su u anketama
}
