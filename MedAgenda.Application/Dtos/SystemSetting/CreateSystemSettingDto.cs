using System;

namespace MedAgenda.Application.Dtos.SystemSetting
{
    public class CreateSystemSettingDto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}