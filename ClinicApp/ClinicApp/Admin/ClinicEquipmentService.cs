using ClinicApp.Admin;
using System.Collections.Generic;
using System.Linq;

public static class ClinicEquipmentService 
{
    static List<ClinicEquipment> HospitalEquipmentList { get; set;}

    static ClinicEquipmentService()
    {
        HospitalEquipmentList = new List<ClinicEquipment>
                {
                    new ClinicEquipment { Id = 1, Name = "Syrgery Knife", RoomId = 1, Amount = 6, Type = EquipmentType.Operations},
                    new ClinicEquipment { Id = 2, Name = "Operating Table", RoomId = 1, Amount = 2, Type = EquipmentType.RoomFurniture},
                    new ClinicEquipment { Id = 3, Name = "Bench", RoomId = 2, Amount = 4, Type = EquipmentType.Hallway},
                    new ClinicEquipment { Id = 4, Name = "Dental Mirror", RoomId = 4, Amount = 10, Type = EquipmentType.Examinations},
                    new ClinicEquipment { Id = 5, Name = "Sickle Probe", RoomId = 4, Amount = 10, Type = EquipmentType.Examinations}
                };
    }
    public static List<ClinicEquipment> GetAll() => HospitalEquipmentList;

    public static ClinicEquipment? Get(int id) => HospitalEquipmentList.FirstOrDefault(p => p.Id == id);

    public static void Add(ClinicEquipment heq)
    {
        heq.Id = HospitalEquipmentList.Last().Id + 1; 
        HospitalEquipmentList.Add(heq);
    }
    public static void Delete(int id)
    {
        var heq = Get(id);
        if (heq is null)
            return;
        HospitalEquipmentList.Remove(heq);
    }
    public static void AddToRoom(int eqId, int roomId)
    {
        var heq = Get(eqId);
        if (heq is null)
            return;
        heq.RoomId = roomId;
    }
    public static List<ClinicEquipment> Search(string searchTerm)
    {
        searchTerm = searchTerm.ToLower();
        var results = new List<ClinicEquipment>();
        foreach(var item in HospitalEquipmentList)
        {
            if(item.Name.ToLower().Contains(searchTerm) || item.Type.ToString().ToLower().Contains(searchTerm) || ClinicEquipmentService.Get(item.RoomId).Name.ToLower().Contains(searchTerm))
            {
                results.Add(item);
            }
        }
        return results;
    }
    public static List<ClinicEquipment> FilterByEqType(List<ClinicEquipment> inputList, EquipmentType type)
    {
        var results = new List<ClinicEquipment>();
        foreach(var item in inputList)
        {
            if(item.Type == type)
            {
                results.Add(item);
            }
        }
        return results;
    }
    public static List<ClinicEquipment> FilterByRoomType(List<ClinicEquipment> inputList, RoomType type)
    {
        var results = new List<ClinicEquipment>();
        foreach(var item in inputList)
        {
            if(ClinicRoomService.Get(item.RoomId).Type == type)
            {
                results.Add(item);
            }
        }
        return results;
    }
    public static List<ClinicEquipment> FilterByNumbers(List<ClinicEquipment> inputList, int lowerBound, int upperBound)
    {
        var results = new List<ClinicEquipment>();
        foreach(var item in inputList)
        {
            if(item.Amount >= lowerBound && item.Amount <= upperBound)
            {
                results.Add(item);
            }
        }
        return results;
    }
}