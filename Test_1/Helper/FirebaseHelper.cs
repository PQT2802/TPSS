using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using TPSS.Business.SettingObject;

namespace TPSS.API.Helper
{
    public static class FirebaseHelper
    {
        public static IServiceCollection AddFirebaseSDK(this IServiceCollection services, IConfiguration configuration)
        {
            ///Firebase storage
            var firebaseSettingSection = configuration.GetSection("FirebaseSettings");
            services.Configure<FirebaseSetting>(firebaseSettingSection);
            var firebaseSettings = firebaseSettingSection.Get<FirebaseSetting>();
            string firebaseDir = System.IO.Directory.GetCurrentDirectory();

            firebaseDir += "/Firebase/timeshareprojectsalessystem-firebase-adminsdk-fahiy-c3501b6652.json";

            //Firebase SDKs
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(firebaseDir),
                ProjectId = firebaseSettings.ProjectId,
                ServiceAccountId = firebaseSettings.ServiceAccountId
            });

            return services;
        }

    }
}
