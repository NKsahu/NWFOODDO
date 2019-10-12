<%@ WebHandler Language="C#" Class="FoodDoApi" %>
public class FoodDoApi : JsonServices.Web.JsonHandler
{
    public FoodDoApi()
    {
        JsonServices.InterfaceConfiguration IConfig = new JsonServices.InterfaceConfiguration("FoodDoApi", typeof(FOODDO.Models.IFOOD), typeof(FOODDO.Models.IIFOOD));
        this.service.Interfaces.Add(IConfig);
    }
}



 