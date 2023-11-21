using DB;

/*using (EpaContext db = new())
{
    KKS newArmature = new() { Id = "10AAA10AA101", EquipmentName = "Запорный клапан на линии парогенератора", SafetyClass = "3" };

    db.KKSes.Add(newArmature);
    db.SaveChanges();
}*/

using (EpaContext epaDB = new())
{
   /* KKS? kks = epaDB.KKSes.;

    if (kks != null)
    {
        kks.SafetyClass = "3Н";

        epaDB.SaveChanges();
    }*/

    List<KKS> KKSes = epaDB.KKSes.ToList();

    Console.WriteLine("KKSes list:");

    foreach (KKS k in KKSes)
    {
        Console.WriteLine($"{k.Id}    {k.EquipmentName} {k.SafetyClass}");
    }
}

/*using (EpaContext epaDB = new())
{
    KKS? kks = epaDB.KKSes.LastOrDefault();

    if (kks != null)
    {
        epaDB.KKSes.Remove(kks);

        epaDB.SaveChanges();
    }

    Console.ReadKey();
    Console.Clear();

    List<KKS> KKSes = epaDB.KKSes.ToList();

    Console.WriteLine("KKSes list:");

    foreach (KKS k in KKSes)
    {
        Console.WriteLine($"{k.Id}    {k.EquipmentName} {k.SafetyClass}");
    }
}*/

