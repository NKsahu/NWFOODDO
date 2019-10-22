function FoodDoApi(){ self = this; }
FoodDoApi.prototype = {
    self: null,
    urlString: "http://DOTNET/FoodDoApi.ashx",
    Signup_Customer:function(Name,Birthday,Email,Mobile,Password,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'Signup_Customer', 'parameters': {'Name':Name,'Birthday':Birthday,'Email':Email,'Mobile':Mobile,'Password':Password}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    Login_Customer:function(Mobile,Password,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'Login_Customer', 'parameters': {'Mobile':Mobile,'Password':Password}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    PostAddress:function(CID,Address1,Landmark,Hub,Type,Latitude,Longitude,Description,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'PostAddress', 'parameters': {'CID':CID,'Address1':Address1,'Landmark':Landmark,'Hub':Hub,'Type':Type,'Latitude':Latitude,'Longitude':Longitude,'Description':Description}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    GetAddress:function(CID,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'GetAddress', 'parameters': {'CID':CID}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    GetCategory:function(successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'GetCategory', 'parameters': {}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    GetMessOfFoodAndCategories:function(SearchTerm,CID,FoodType,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'GetMessOfFoodAndCategories', 'parameters': {'SearchTerm':SearchTerm,'CID':CID,'FoodType':FoodType}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    GetMessItem:function(SearchTerm,CID,FoodType,UID,MealsType,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'GetMessItem', 'parameters': {'SearchTerm':SearchTerm,'CID':CID,'FoodType':FoodType,'UID':UID,'MealsType':MealsType}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    SearchMessFood:function(SearchTerm,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'SearchMessFood', 'parameters': {'SearchTerm':SearchTerm}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    AddCart:function(CID,FID,Cnt,MessID,MealType,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'AddCart', 'parameters': {'CID':CID,'FID':FID,'Cnt':Cnt,'MessID':MessID,'MealType':MealType}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    GetCart:function(CID,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'GetCart', 'parameters': {'CID':CID}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    PostOrder:function(CID,CSVMessId,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'PostOrder', 'parameters': {'CID':CID,'CSVMessId':CSVMessId}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    GetOrder:function(CID,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'GetOrder', 'parameters': {'CID':CID}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    HaveItemInCart:function(CID,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'HaveItemInCart', 'parameters': {'CID':CID}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    ChangePass:function(CID,OldPass,NewPass,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'ChangePass', 'parameters': {'CID':CID,'OldPass':OldPass,'NewPass':NewPass}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    ReOrder:function(OrderID,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'ReOrder', 'parameters': {'OrderID':OrderID}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    CheckBalance:function(CID,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'CheckBalance', 'parameters': {'CID':CID}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    AddBalance:function(CID,AMT,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'AddBalance', 'parameters': {'CID':CID,'AMT':AMT}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    PostReview:function(FID,CID,Rating,comment,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'PostReview', 'parameters': {'FID':FID,'CID':CID,'Rating':Rating,'comment':comment}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    GetReview:function(FID,CID,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'GetReview', 'parameters': {'FID':FID,'CID':CID}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    ShowHideRating:function(Fid,CustID,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'ShowHideRating', 'parameters': {'Fid':Fid,'CustID':CustID}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    GmailLogin_Customer:function(Name,Gmail,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'GmailLogin_Customer', 'parameters': {'Name':Name,'Gmail':Gmail}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    DBLogin:function(Mobile,Password,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'DBLogin', 'parameters': {'Mobile':Mobile,'Password':Password}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    CommonLogin:function(MobileNo,Password,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'CommonLogin', 'parameters': {'MobileNo':MobileNo,'Password':Password}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    MessOrder:function(MessId,Status,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'MessOrder', 'parameters': {'MessId':MessId,'Status':Status}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    MessOrderCount:function(Messid,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'MessOrderCount', 'parameters': {'Messid':Messid}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    MarkOrderItemPacked:function(OrderItemID,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'MarkOrderItemPacked', 'parameters': {'OrderItemID':OrderItemID}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    AdminLogin:function(UserName,Password,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'AdminLogin', 'parameters': {'UserName':UserName,'Password':Password}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    CollectionFilter:function(successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'CollectionFilter', 'parameters': {}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    ListOfOredItemCollect:function(MessId,FID,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'ListOfOredItemCollect', 'parameters': {'MessId':MessId,'FID':FID}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    HubList:function(successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'HubList', 'parameters': {}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    CusQrInfo:function(CustId,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'CusQrInfo', 'parameters': {'CustId':CustId}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    WalletOffers:function(successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'WalletOffers', 'parameters': {}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    HubWiseTifinList:function(successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'HubWiseTifinList', 'parameters': {}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    CreateMessFood:function(food,imgbytes,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'CreateMessFood', 'parameters': {'food':food,'imgbytes':imgbytes}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    },
    MessFoodList:function(MID,successFunction,failFunction,token) {
        var data = { 'interface': 'FoodDoApi', 'method': 'MessFoodList', 'parameters': {'MID':MID}, 'token': token };
        
        var jsonData = dojo.toJson(data);
        var xhrArgs = {
            url: self.urlString,
            handleAs: 'json',
            postData: jsonData,
            load: successFunction,
            error: failFunction };
        var deferred = dojo.xhrPost(xhrArgs);
    }
};