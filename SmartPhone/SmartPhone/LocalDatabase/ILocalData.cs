using System.Collections.Generic;

namespace SmartPhone
{
    public interface ILocalData
    {
        int DeleteAd(int id);
        Advertisement GetAd(int id);
        IEnumerable<Advertisement> GetAds();
        UserInfo GetUserInfo();
        int SaveAd(Advertisement ad);
        int SaveUserInfo(UserInfo userInfo);
    }
}