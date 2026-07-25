using System.Collections.Generic;
using System;

namespace FoodFlow.Application.DTOModels;

public class OperatingHoursDto
{
    public Dictionary<DayOfWeek, List<TimeSlotDto>> Schedule { get; set; } = new();
}