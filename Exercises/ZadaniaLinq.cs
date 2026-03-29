using LinqConsoleLab.PL.Data;

namespace LinqConsoleLab.PL.Exercises;

public sealed class ZadaniaLinq
{
    /// <summary>
    /// Zadanie:
    /// Wyszukaj wszystkich studentów mieszkających w Warsaw.
    /// Zwróć numer indeksu, pełne imię i nazwisko oraz miasto.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko, Miasto
    /// FROM Studenci
    /// WHERE Miasto = 'Warsaw';
    /// </summary>
    public IEnumerable<string> Zadanie01_StudenciZWarszawy()
    {
        return DaneUczelni.Studenci
            .Where(x => x.Miasto == "Warsaw")
            .Select(x => $"{x.NumerIndeksu}, {x.Imie}, ${x.Nazwisko}, {x.Miasto}");
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj listę adresów e-mail wszystkich studentów.
    /// Użyj projekcji, tak aby w wyniku nie zwracać całych obiektów.
    ///
    /// SQL:
    /// SELECT Email
    /// FROM Studenci;
    /// </summary>
    public IEnumerable<string> Zadanie02_AdresyEmailStudentow()
    {
        return DaneUczelni.Studenci
            .Select(x => $"{x.Email}");
    }

    /// <summary>
    /// Zadanie:
    /// Posortuj studentów alfabetycznie po nazwisku, a następnie po imieniu.
    /// Zwróć numer indeksu i pełne imię i nazwisko.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko
    /// FROM Studenci
    /// ORDER BY Nazwisko, Imie;
    /// </summary>
    public IEnumerable<string> Zadanie03_StudenciPosortowani()
    {
        return DaneUczelni.Studenci
            .OrderBy(x => x.Nazwisko)
            .ThenBy(x => x.Imie)
            .Select(x => $"{x.NumerIndeksu}, {x.Imie}, {x.Nazwisko}");
    }

    /// <summary>
    /// Zadanie:
    /// Znajdź pierwszy przedmiot z kategorii Analytics.
    /// Jeżeli taki przedmiot nie istnieje, zwróć komunikat tekstowy.
    ///
    /// SQL:
    /// SELECT TOP 1 Nazwa, DataStartu
    /// FROM Przedmioty
    /// WHERE Kategoria = 'Analytics';
    /// </summary>
    public IEnumerable<string> Zadanie04_PierwszyPrzedmiotAnalityczny()
    {
        return DaneUczelni.Przedmioty
            .Where(x => x.Kategoria == "Analytics")
            .Select(x => $"{x.Nazwa}, {x.DataStartu}")
            .Take(1);
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy w danych istnieje przynajmniej jeden nieaktywny zapis.
    /// Zwróć jedno zdanie z odpowiedzią True/False albo Tak/Nie.
    ///
    /// SQL:
    /// SELECT CASE WHEN EXISTS (
    ///     SELECT 1
    ///     FROM Zapisy
    ///     WHERE CzyAktywny = 0
    /// ) THEN 1 ELSE 0 END;
    /// </summary>
    public IEnumerable<string> Zadanie05_CzyIstniejeNieaktywneZapisanie()
    {
        return [DaneUczelni.Zapisy.
            Any(x => x.CzyAktywny == false) ? "Tak" : "Nie"];
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy każdy prowadzący ma uzupełnioną nazwę katedry.
    /// Warto użyć metody, która weryfikuje warunek dla całej kolekcji.
    ///
    /// SQL:
    /// SELECT CASE WHEN COUNT(*) = COUNT(Katedra)
    /// THEN 1 ELSE 0 END
    /// FROM Prowadzacy;
    /// </summary>
    public IEnumerable<string> Zadanie06_CzyWszyscyProwadzacyMajaKatedre()
    {
        return [DaneUczelni.Prowadzacy
            .All(x => x.Katedra != null) ? "Tak" : "Nie"];
    }

    /// <summary>
    /// Zadanie:
    /// Policz, ile aktywnych zapisów znajduje się w systemie.
    ///
    /// SQL:
    /// SELECT COUNT(*)
    /// FROM Zapisy
    /// WHERE CzyAktywny = 1;
    /// </summary>
    public IEnumerable<string> Zadanie07_LiczbaAktywnychZapisow()
    {
        return [DaneUczelni.Zapisy
            .Count(x => x.CzyAktywny == true)
            .ToString()];
    }

    /// <summary>
    /// Zadanie:
    /// Pobierz listę unikalnych miast studentów i posortuj ją rosnąco.
    ///
    /// SQL:
    /// SELECT DISTINCT Miasto
    /// FROM Studenci
    /// ORDER BY Miasto;
    /// </summary>
    public IEnumerable<string> Zadanie08_UnikalneMiastaStudentow()
    {
        return DaneUczelni.Studenci
            .OrderBy(x => x.Miasto)
            .Select(x => x.Miasto)
            .Distinct();
    }

    /// <summary>
    /// Zadanie:
    /// Zwróć trzy najnowsze zapisy na przedmioty.
    /// W wyniku pokaż datę zapisu, identyfikator studenta i identyfikator przedmiotu.
    ///
    /// SQL:
    /// SELECT TOP 3 DataZapisu, StudentId, PrzedmiotId
    /// FROM Zapisy
    /// ORDER BY DataZapisu DESC;
    /// </summary>
    public IEnumerable<string> Zadanie09_TrzyNajnowszeZapisy()
    {
        return DaneUczelni.Zapisy
            .OrderByDescending(x => x.DataZapisu)
            .Select(x => $"{x.DataZapisu}, {x.StudentId}, {x.PrzedmiotId}")
            .Take(3);
    }

    /// <summary>
    /// Zadanie:
    /// Zaimplementuj prostą paginację dla listy przedmiotów.
    /// Załóż stronę o rozmiarze 2 i zwróć drugą stronę danych.
    ///
    /// SQL:
    /// SELECT Nazwa, Kategoria
    /// FROM Przedmioty
    /// ORDER BY Nazwa
    /// OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY;
    /// </summary>
    public IEnumerable<string> Zadanie10_DrugaStronaPrzedmiotow()
    {
        return DaneUczelni.Przedmioty
            .OrderBy(x => x.Nazwa)
            .Select(x => $"{x.Nazwa}, {x.Kategoria}")
            .Skip(2)
            .Take(2);
    }

    /// <summary>
    /// Zadanie:
    /// Połącz studentów z zapisami po StudentId.
    /// Zwróć pełne imię i nazwisko studenta oraz datę zapisu.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, z.DataZapisu
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId;
    /// </summary>
    public IEnumerable<string> Zadanie11_PolaczStudentowIZapisy()
    {
        return DaneUczelni.Studenci
            .Join(
                DaneUczelni.Zapisy, 
                s => s.Id, 
                z => z.StudentId,
                (s, z) => $"{s.Imie}, {s.Nazwisko}, {z.DataZapisu}");
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj wszystkie pary student-przedmiot na podstawie zapisów.
    /// Użyj podejścia, które pozwoli spłaszczyć dane do jednej sekwencji wyników.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, p.Nazwa
    /// FROM Zapisy z
    /// JOIN Studenci s ON s.Id = z.StudentId
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId;
    /// </summary>
    public IEnumerable<string> Zadanie12_ParyStudentPrzedmiot()
    {
        return DaneUczelni.Zapisy
            .Join(
                DaneUczelni.Studenci, 
                z => z.StudentId, 
                s => s.Id, 
                (z, s) => new {z, s})
            .Join(
                DaneUczelni.Przedmioty, 
                zp => zp.z.PrzedmiotId, 
                p => p.Id,
                (zp, p) => $"{zp.s.Imie}, {zp.s.Nazwisko}, {p.Nazwa}");
    }

    /// <summary>
    /// Zadanie:
    /// Pogrupuj zapisy według przedmiotu i zwróć nazwę przedmiotu oraz liczbę zapisów.
    ///
    /// SQL:
    /// SELECT p.Nazwa, COUNT(*)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie13_GrupowanieZapisowWedlugPrzedmiotu()
    {
        return DaneUczelni.Zapisy
            .Join(
                DaneUczelni.Przedmioty,
                z => z.PrzedmiotId,
                p => p.Id,
                (z, p) => new { z, p })
            .GroupBy(x => x.p.Nazwa)
            .Select(x => $"{x.Key}, {x.Count()}");
    }

    /// <summary>
    /// Zadanie:
    /// Oblicz średnią ocenę końcową dla każdego przedmiotu.
    /// Pomiń rekordy, w których ocena końcowa ma wartość null.
    ///
    /// SQL:
    /// SELECT p.Nazwa, AVG(z.OcenaKoncowa)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie14_SredniaOcenaNaPrzedmiot()
    {
        return DaneUczelni.Zapisy
            .Join(
                DaneUczelni.Przedmioty,
                z => z.PrzedmiotId,
                p => p.Id,
                (z, p) => new { z, p })
            .Where(x => x.z.OcenaKoncowa != null)
            .GroupBy(x => x.p.Nazwa)
            .Select(x => $"{x.Key}, {x.Average(arg => arg.z.OcenaKoncowa.Value)}");
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego prowadzącego policz liczbę przypisanych przedmiotów.
    /// W wyniku zwróć pełne imię i nazwisko oraz liczbę przedmiotów.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, COUNT(p.Id)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie15_ProwadzacyILiczbaPrzedmiotow()
    {
        return DaneUczelni.Prowadzacy
            .GroupJoin(
                DaneUczelni.Przedmioty,
                pr => pr.Id,
                p => p.ProwadzacyId,
                (pr, p) => new { pr.Imie, pr.Nazwisko, Count = p.Count() })
            .Select(x => $"{x.Imie}, {x.Nazwisko},  {x.Count}");
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego studenta znajdź jego najwyższą ocenę końcową.
    /// Pomiń studentów, którzy nie mają jeszcze żadnej oceny.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, MAX(z.OcenaKoncowa)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY s.Imie, s.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie16_NajwyzszaOcenaKazdegoStudenta()
    {
        return DaneUczelni.Studenci
            .Join(
                DaneUczelni.Zapisy,
                s => s.Id,
                z => z.StudentId,
                (s, z) => new { s, z })
            .Where(arg => arg.z.OcenaKoncowa != null)
            .GroupBy(arg => new { arg.s.Imie, arg.s.Nazwisko })
            .Select(x => $"{x.Key.Imie}, {x.Key.Nazwisko}, {x.Max(arg => arg.z.OcenaKoncowa.Value)}");
    }

    /// <summary>
    /// Wyzwanie:
    /// Znajdź studentów, którzy mają więcej niż jeden aktywny zapis.
    /// Zwróć pełne imię i nazwisko oraz liczbę aktywnych przedmiotów.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Imie, s.Nazwisko
    /// HAVING COUNT(*) > 1;
    /// </summary>
    public IEnumerable<string> Wyzwanie01_StudenciZWiecejNizJednymAktywnymPrzedmiotem()
    {
        return DaneUczelni.Studenci
            .Join(
                DaneUczelni.Zapisy,
                s => s.Id,
                z => z.StudentId,
                (s, z) => new { s, z })
            .Where(arg => arg.z.CzyAktywny == true)
            .GroupBy(arg => new { arg.s.Imie, arg.s.Nazwisko })
            .Where(arg => arg.Count() > 1)
            .Select(arg => $"{arg.Key.Imie}, {arg.Key.Nazwisko}, {arg.Count()}");
    }

    /// <summary>
    /// Wyzwanie:
    /// Wypisz przedmioty startujące w kwietniu 2026, dla których żaden zapis nie ma jeszcze oceny końcowej.
    ///
    /// SQL:
    /// SELECT p.Nazwa
    /// FROM Przedmioty p
    /// JOIN Zapisy z ON p.Id = z.PrzedmiotId
    /// WHERE MONTH(p.DataStartu) = 4 AND YEAR(p.DataStartu) = 2026
    /// GROUP BY p.Nazwa
    /// HAVING SUM(CASE WHEN z.OcenaKoncowa IS NOT NULL THEN 1 ELSE 0 END) = 0;
    /// </summary>
    public IEnumerable<string> Wyzwanie02_PrzedmiotyStartujaceWKwietniuBezOcenKoncowych()
    {
        return DaneUczelni.Przedmioty
            .Where(arg => arg.DataStartu is { Month: 4, Year: 2026 })
            .Join(
                DaneUczelni.Zapisy,
                p => p.Id,
                z => z.PrzedmiotId,
                (p, z) => new { p, z })
            .GroupBy(arg => arg.p.Nazwa)
            .Where(arg => arg.Count(x => x.z.OcenaKoncowa != null) == 0)
            .Select(arg => $"{arg.Key}");
    }

    /// <summary>
    /// Wyzwanie:
    /// Oblicz średnią ocen końcowych dla każdego prowadzącego na podstawie wszystkich jego przedmiotów.
    /// Pomiń brakujące oceny, ale pozostaw samych prowadzących w wyniku.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, AVG(z.OcenaKoncowa)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// LEFT JOIN Zapisy z ON z.PrzedmiotId = p.Id
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Wyzwanie03_ProwadzacyISredniaOcenNaIchPrzedmiotach()
    {
        return DaneUczelni.Prowadzacy
            .GroupJoin(
                DaneUczelni.Przedmioty,
                pr => pr.Id,
                p => p.ProwadzacyId,
                (pr, przedmioty) => new { pr, przedmioty })
            .SelectMany(
                x => x.przedmioty.DefaultIfEmpty(),
                (x, p) => new { x.pr, p })
            .GroupJoin(
                DaneUczelni.Zapisy,
                x => x.p == null ? -1 : x.p.Id, 
                z => z.PrzedmiotId,
                (x, zapisy) => new { x.pr, x.p, zapisy })
            .SelectMany(
                x => x.zapisy.DefaultIfEmpty(),
                (x, z) => new { x.pr, z })
            .Where(x => x.z != null && x.z.OcenaKoncowa != null)
            .GroupBy(x => new { x.pr.Imie, x.pr.Nazwisko })
            .Select(g => new
            {
                g.Key.Imie,
                g.Key.Nazwisko,
                Average = g.Average(x => (decimal?)x.z.OcenaKoncowa)
            })
            .Select(arg => $"{arg.Imie},  {arg.Nazwisko}, {arg.Average}");
    }

    /// <summary>
    /// Wyzwanie:
    /// Pokaż miasta studentów oraz liczbę aktywnych zapisów wykonanych przez studentów z danego miasta.
    /// Posortuj wynik malejąco po liczbie aktywnych zapisów.
    ///
    /// SQL:
    /// SELECT s.Miasto, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Miasto
    /// ORDER BY COUNT(*) DESC;
    /// </summary>
    public IEnumerable<string> Wyzwanie04_MiastaILiczbaAktywnychZapisow()
    {
        return DaneUczelni.Studenci
            .Join(
                DaneUczelni.Zapisy,
                s => s.Id,
                z => z.StudentId,
                (s, z) => new { s, z })
            .Where(arg => arg.z.CzyAktywny == true)
            .GroupBy(arg => arg.s.Miasto)
            .OrderByDescending(arg => arg.Count())
            .Select(arg => $"{arg.Key}: {arg.Count()}");
    }

    private static NotImplementedException Niezaimplementowano(string nazwaMetody)
    {
        return new NotImplementedException(
            $"Uzupełnij metodę {nazwaMetody} w pliku Exercises/ZadaniaLinq.cs i uruchom polecenie ponownie.");
    }
}
