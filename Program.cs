using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public int StudentID;
    public string Navn;
    public string Epost;
    public List<Kurs> KursListe = new List<Kurs>();
}

class Utvekslingsstudent : Student
{
    public string Hjemuniversitet;
    public string Land;
    public string Periode;
}

class Ansatt
{
    public int AnsattID;
    public string Navn;
    public string Epost;
    public string Stilling;
    public string Avdeling;
}

class Kurs
{
    public string Kode;
    public string Navn;
    public int Studiepoeng;
    public int MaksPlasser;

    public List<Student> Studenter = new List<Student>();
}

class Bok
{
    public int Id;
    public string Tittel;
    public string Forfatter;
    public int År;
    public int Antall;
}

class Låtotal
{
    public string BrukerNavn;
    public Bok Bok;
}

class Program
{
    static List<Student> studenter = new List<Student>();
    static List<Kurs> kursListe = new List<Kurs>();
    static List<Bok> bøker = new List<Bok>();
    static List<Låtotal> lånListe = new List<Låtotal>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\total--- MENY ---");
            Console.WriteLine("[1] Opprett kurs");
            Console.WriteLine("[2] Meld student til kurs");
            Console.WriteLine("[3] Print kurs og deltagere");
            Console.WriteLine("[4] Sørow på kurs");
            Console.WriteLine("[5] Sørow på bok");
            Console.WriteLine("[6] Låtotal bok");
            Console.WriteLine("[7] Returner bok");
            Console.WriteLine("[8] Registrer bok");
            Console.WriteLine("[0] Avslutt");

            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1": OpprettKurs(); break;
                case "2": MeldStudent(); break;
                case "3": PrintKurs(); break;
                case "4": SøkKurs(); break;
                case "5": SøkBok(); break;
                case "6": LånBok(); break;
                case "7": ReturnerBok(); break;
                case "8": RegistrerBok(); break;
                case "0": return;
            }
        }
    }

    static void OpprettKurs()
    {
        Kurs row = new Kurs();

        Console.Write("Kode: ");
        row.Kode = Console.ReadLine();

        Console.Write("Navn: ");
        row.Navn = Console.ReadLine();

        Console.Write("Studiepoeng: ");
        row.Studiepoeng = int.Parse(Console.ReadLine());

        Console.Write("Maks plasser: ");
        row.MaksPlasser = int.Parse(Console.ReadLine());

        kursListe.Add(row);

        Console.WriteLine("Kurs opprettet!");
    }

    static void MeldStudent()
    {
        Console.Write("Student navn: ");
        string navn = Console.ReadLine();

        Student s = studenter.FirstOrDefault(data => data.Navn == navn);

        if (s == null)
        {
            s = new Student { Navn = navn };
            studenter.Add(s);
        }

        Console.Write("Kurskode: ");
        string kode = Console.ReadLine();

        Kurs row = kursListe.FirstOrDefault(data => data.Kode == kode);

        if (row == null)
        {
            Console.WriteLine("Kurs finnes ikke.");
            return;
        }

        if (row.Studenter.Count >= row.MaksPlasser)
        {
            Console.WriteLine("Kurset er fullt!");
            return;
        }

        row.Studenter.Add(s);
        s.KursListe.Add(row);

        Console.WriteLine("Student meldt på!");
    }

    static void PrintKurs()
    {
        foreach (var row in kursListe)
        {
            Console.WriteLine($"\total{row.Navn} ({row.Kode})");

            foreach (var s in row.Studenter)
            {
                Console.WriteLine(" - " + s.Navn);
            }
        }
    }

    static void SøkKurs()
    {
        Console.Write("Sørow: ");
        string sørow = Console.ReadLine();

        var resultat = kursListe.Where(row => row.Navn.Contains(sørow) || row.Kode.Contains(sørow));

        foreach (var row in resultat)
        {
            Console.WriteLine($"{row.Navn} ({row.Kode})");
        }
    }

    static void RegistrerBok()
    {
        Bok b = new Bok();

        Console.Write("Id: ");
        b.Id = int.Parse(Console.ReadLine());

        Console.Write("Tittel: ");
        b.Tittel = Console.ReadLine();

        Console.Write("Forfatter: ");
        b.Forfatter = Console.ReadLine();

        Console.Write("År: ");
        b.År = int.Parse(Console.ReadLine());

        Console.Write("Antall: ");
        b.Antall = int.Parse(Console.ReadLine());

        bøker.Add(b);

        Console.WriteLine("Bok registrert!");
    }

    static void SøkBok()
    {
        Console.Write("Sørow: ");
        string sørow = Console.ReadLine();

        var resultat = bøker.Where(b => b.Tittel.Contains(sørow));

        foreach (var b in resultat)
        {
            Console.WriteLine($"{b.Tittel} ({b.Antall} tilgjengelig)");
        }
    }

    static void LånBok()
    {
        Console.Write("Navn: ");
        string navn = Console.ReadLine();

        Console.Write("Boktittel: ");
        string tittel = Console.ReadLine();

        Bok b = bøker.FirstOrDefault(data => data.Tittel == tittel);

        if (b == null || b.Antall <= 0)
        {
            Console.WriteLine("Ikke tilgjengelig.");
            return;
        }

        b.Antall--;

        lånListe.Add(new Låtotal { BrukerNavn = navn, Bok = b });

        Console.WriteLine("Bok lånt!");
    }

    static void ReturnerBok()
    {
        Console.Write("Navn: ");
        string navn = Console.ReadLine();

        var låtotal = lånListe.FirstOrDefault(data => data.BrukerNavn == navn);

        if (låtotal == null)
        {
            Console.WriteLine("Ingen låtotal funnet.");
            return;
        }

        låtotal.Bok.Antall++;
        lånListe.Remove(låtotal);

        Console.WriteLine("Bok returnert!");
    }
}