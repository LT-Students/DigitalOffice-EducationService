﻿using LT.DigitalOffice.Kernel.BrokerSupport.Attributes;
using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;
using LT.DigitalOffice.Models.Broker.Common;
using LT.DigitalOffice.Models.Broker.Requests.Image;

namespace LT.DigitalOffice.EducationService.Models.Dto.Configurations
{
  public class RabbitMqConfig : BaseRabbitMqConfig
  {
    // image

    [AutoInjectRequest(typeof(ICreateImagesRequest))]
    public string CreateImagesEndpoint { get; set; }

    [AutoInjectRequest(typeof(IGetImagesRequest))]
    public string GetImagesEndpoint { get; set; }

    [AutoInjectRequest(typeof(IRemoveImagesRequest))]
    public string RemoveImagesEndpoint { get; set; }

    [AutoInjectRequest(typeof(ICheckUsersExistence))]
    public string CheckUsersExistenceEndpoint { get; set; }
  }
}
